using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using Dapper;

using DDDwithEFCore_03_ApplicationCore.DomainModels.People;

using DDDwithEFCore_06_ApplicationServices.Queries.DbConnectionFactories;

using FluentValidation;

using MediatR;

namespace DDDwithEFCore_06_ApplicationServices.Queries.PersonQueries.GetPersonCollection
{
	public class RequestHandler : IRequestHandler<GetPersonCollectionRequest, GetPersonCollectionResult>
	{
		public RequestHandler(
			SqlServerDbConnectionFactory connectionFactory,
			IValidator<GetPersonCollectionResult> validator)
		{
			_connectionFactory = connectionFactory;
			_validator = validator;
		}

		private readonly SqlServerDbConnectionFactory _connectionFactory;
		private readonly IValidator<GetPersonCollectionResult> _validator;


		public async Task<GetPersonCollectionResult> Handle(
			GetPersonCollectionRequest request,
			CancellationToken cancellationToken)
		{
			await _validator.ValidateAndThrowAsync(request, null, cancellationToken);

			List<string> sqlClauses = new List<string>
			{
				SqlClauseForQueryingProducts(request),
				SqlClauseForCountProducts(request)
			};

			string combinedSqlClauses = string.Join("; ", sqlClauses);

			var parameters = new
			{
				Offset = (request.PageIndex - 1) * request.PageSize,
				request.PageSize,
				SearchTerm = $"%{request.SearchTerm}%"
			};

			using IDbConnection connection = await _connectionFactory.GetConnection(cancellationToken);

			SqlMapper.GridReader multiQueries = await connection.QueryMultipleAsync(combinedSqlClauses, parameters);

			IEnumerable<GetPersonCollectionResult.PersonCollectionItem> people =
				await multiQueries.ReadAsync<GetPersonCollectionResult.PersonCollectionItem>();

			int totalPerson = await multiQueries.ReadFirstOrDefaultAsync<int>();

			GetPersonCollectionResult result = new GetPersonCollectionResult
			{
				People = people ?? Enumerable.Empty<GetPersonCollectionResult.PersonCollectionItem>(),
				TotalPerson = totalPerson
			};

			return result;
		}

		private string SqlClauseForQueryingProducts(GetPersonCollectionRequest request)
		{
			Dictionary<string, string> fields = new Dictionary<string, string>
			{
				{ nameof(GetPersonCollectionResult.PersonCollectionItem.Id), $"{nameof(Person)}.Id" },
				{
					nameof(GetPersonCollectionResult.PersonCollectionItem.DisplayName),
					$"{nameof(Person)}.{nameof(Person.Name)}"
				}
			};

			string groupByFields = string.Join(",", fields.Select(x => x.Value));
			string selectedFields = string.Join(", ", fields.Select(x => $"{x.Key}={x.Value}"));

			StringBuilder sqlClauseBuilder = new StringBuilder($"SELECT {selectedFields}")
				.Append($" FROM {nameof(Person)} AS {nameof(Person)}");

			if (!string.IsNullOrWhiteSpace(request.SearchTerm))
				sqlClauseBuilder =
					sqlClauseBuilder
						.Append($" WHERE {nameof(Person)}.{nameof(Person.Name)} LIKE @SearchTerm");

			sqlClauseBuilder = sqlClauseBuilder
				.Append($" GROUP BY {groupByFields}")
				.Append($" ORDER BY {nameof(Person)}.{nameof(Person.Name)} ")
				.Append(" OFFSET @Offset ROWS ")
				.Append(" FETCH NEXT @PageSize ROWS ONLY; ");

			return sqlClauseBuilder.ToString();
		}

		private string SqlClauseForCountProducts(GetPersonCollectionRequest request)
		{
			StringBuilder sqlClauseBuilder =
				new StringBuilder($"SELECT COUNT(*)").Append($" FROM {nameof(Person)}");

			if (!string.IsNullOrWhiteSpace(request.SearchTerm))
				sqlClauseBuilder = sqlClauseBuilder
					.Append($" WHERE {nameof(Person.Name)} LIKE @SearchTerm");

			return sqlClauseBuilder.ToString();
		}
	}
}
