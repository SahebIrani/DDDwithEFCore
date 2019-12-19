using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using Dapper;

using DDDwithEFCore_06_ApplicationServices.Queries.DbConnectionFactories;

using FluentValidation;

using MediatR;

namespace DDDwithEFCore_06_ApplicationServices.Queries.PersonQueries.GetPersonDetail
{
	public class RequestHandler : IRequestHandler<GetPersonDetailRequest, GetPersonDetailResult>
	{
		public RequestHandler(
			SqlServerDbConnectionFactory connectionFactory,
			IValidator<GetPersonDetailRequest> validator)
		{
			_connectionFactory = connectionFactory;
			_validator = validator;
		}

		private readonly SqlServerDbConnectionFactory _connectionFactory;
		private readonly IValidator<GetPersonDetailRequest> _validator;

		public async Task<GetPersonDetailResult> Handle(
			GetPersonDetailRequest request,
			CancellationToken cancellationToken)
		{
			await _validator.ValidateAndThrowAsync(request, null, cancellationToken);

			List<string> sqlClauses = new List<string>
			{
				SqlClauseForQueryingPerson(),
				SqlClauseForQueryingCatalogCategory()
			};


			using var connection = await _connectionFactory.GetConnection(cancellationToken);

			SqlMapper.GridReader multiQueries =
				await connection.QueryMultipleAsync(string.Join("; ", sqlClauses),
				new { request.PersonId })
			;

			GetPersonDetailResult.PersonDetailResult people
				= await multiQueries.ReadFirstOrDefaultAsync<GetPersonDetailResult.PersonDetailResult>();

			GetPersonDetailResult result = new GetPersonDetailResult
			{
				People = people ?? new GetPersonDetailResult.PersonDetailResult()
			};

			return result;
		}


		private string SqlClauseForQueryinPerson()
		{
			var fields = new Dictionary<string, string>
			{
				{ nameof(GetPersonDetailResult.Person.Id), $"{nameof(Person)}.Id" },
				{ nameof(GetPersonDetailResult.Person.Name), $"{nameof(Person)}.{nameof(Person.Name)}" }
			};

			var selectedFields = string.Join(", ", fields.Select(x => $"{x.Key}={x.Value}"));

			var sqlClauseBuilder = new StringBuilder($"SELECT {selectedFields}")
				.Append($" FROM {nameof(Person)} AS {nameof(Person)}")
				.Append($" WHERE {nameof(Person)}.Id = @PersonId");

			return sqlClauseBuilder.ToString();
		}

		private string SqlClauseForQueryingCatalogCategory()
		{
			var fields = new Dictionary<string, string>
			{
				{ nameof(GetProductDetailResult.CatalogCategoryResult.CatalogCategoryId), $"{nameof(CatalogCategory)}.{nameof(CatalogCategory.CatalogCategoryId)}" },
				{ nameof(GetProductDetailResult.CatalogCategoryResult.CatalogCategoryName), $"{nameof(CatalogCategory)}.{nameof(CatalogCategory.DisplayName)}" },
				{ nameof(GetProductDetailResult.CatalogCategoryResult.CatalogId), $"{nameof(CatalogCategory)}.{nameof(CatalogCategory.CatalogId)}" },
				{ nameof(GetProductDetailResult.CatalogCategoryResult.CatalogName), $"{nameof(Catalog)}.{nameof(Catalog.DisplayName)}" },
				{ nameof(GetProductDetailResult.CatalogCategoryResult.ProductDisplayName), $"{nameof(CatalogProduct)}.{nameof(CatalogProduct.DisplayName)}" },
				{ nameof(GetProductDetailResult.CatalogCategoryResult.CatalogProductId), $"{nameof(CatalogProduct)}.{nameof(CatalogProduct.CatalogProductId)}" }
			};

			var selectedFields = string.Join(", ", fields.Select(x => $"{x.Key}={x.Value}"));

			var sqlClauseBuilder = new StringBuilder($"SELECT {selectedFields}")
				.Append($" FROM {nameof(CatalogProduct)} AS {nameof(CatalogProduct)}")
				.Append($" INNER JOIN {nameof(CatalogCategory)} AS {nameof(CatalogCategory)}")
				.Append($" ON {nameof(CatalogCategory)}.{nameof(CatalogCategory.CatalogCategoryId)} = {nameof(CatalogProduct)}.{nameof(CatalogCategory)}Id")
				.Append($" INNER JOIN {nameof(Catalog)} AS {nameof(Catalog)}")
				.Append($" ON {nameof(Catalog)}.Id = {nameof(CatalogCategory)}.{nameof(CatalogCategory.CatalogId)}")
				.Append($" WHERE {nameof(CatalogProduct)}.{nameof(CatalogProduct.ProductId)} = @ProductId");

			return sqlClauseBuilder.ToString();
		}
	}
}
