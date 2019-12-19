using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using Dapper;

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
			await this._validator.ValidateAndThrowAsync(request, null, cancellationToken);

			var sqlClauses = new List<string>
			{
				this.SqlClauseForQueryingProducts(request),
				this.SqlClauseForCountProducts(request)
			};

			var combinedSqlClauses = string.Join("; ", sqlClauses);
			var parameters = new
			{
				Offset = (request.PageIndex - 1) * request.PageSize,
				PageSize = request.PageSize,
				SearchTerm = $"%{request.SearchTerm}%"
			};

			using var connection = await this._connectionFactory.GetConnection(cancellationToken);

			var multiQueries = await connection.QueryMultipleAsync(combinedSqlClauses, parameters);

			var products = await multiQueries.ReadAsync<GetProductCollectionResult.ProductCollectionItem>();
			var totalProducts = await multiQueries.ReadFirstOrDefaultAsync<int>();

			var result = new GetProductCollectionResult
			{
				Products = products ?? Enumerable.Empty<GetProductCollectionResult.ProductCollectionItem>(),
				TotalProducts = totalProducts
			};

			return result;
		}

		private string SqlClauseForQueryingProducts(GetProductCollectionRequest request)
		{
			var fields = new Dictionary<string, string>
			{
				{ nameof(GetProductCollectionResult.ProductCollectionItem.Id), $"{nameof(Product)}.Id" },
				{ nameof(GetProductCollectionResult.ProductCollectionItem.DisplayName), $"{nameof(Product)}.{nameof(Product.Name)}" }
			};

			var groupByFields = string.Join(",", fields.Select(x => x.Value));
			var selectedFields = string.Join(", ", fields.Select(x => $"{x.Key}={x.Value}"));

			var sqlClauseBuilder = new StringBuilder($"SELECT {selectedFields}")
				.Append($" FROM {nameof(Product)} AS {nameof(Product)}");

			if (!string.IsNullOrWhiteSpace(request.SearchTerm))
			{
				sqlClauseBuilder = sqlClauseBuilder
					.Append($" WHERE {nameof(Product)}.{nameof(Product.Name)} LIKE @SearchTerm");
			}

			sqlClauseBuilder = sqlClauseBuilder
				.Append($" GROUP BY {groupByFields}")
				.Append($" ORDER BY {nameof(Product)}.{nameof(Product.Name)} ")
				.Append(" OFFSET @Offset ROWS ")
				.Append(" FETCH NEXT @PageSize ROWS ONLY; ");

			return sqlClauseBuilder.ToString();
		}

		private string SqlClauseForCountProducts(GetProductCollectionRequest request)
		{
			var sqlClauseBuilder = new StringBuilder($"SELECT COUNT(*)")
				.Append($" FROM {nameof(Product)}");

			if (!string.IsNullOrWhiteSpace(request.SearchTerm))
			{
				sqlClauseBuilder = sqlClauseBuilder
					.Append($" WHERE {nameof(Product.Name)} LIKE @SearchTerm");
			}

			return sqlClauseBuilder.ToString();
		}
	}
}
