using System;

using FluentValidation;

namespace DDDwithEFCore_06_ApplicationServices.Queries.PersonQueries.GetPersonDetail
{
	public class GetPersonDetailRequestValidator : AbstractValidator<GetProductDetailRequest>
	{
		public GetProductDetailRequestValidator()
		{
			RuleFor(x => x.ProductId)
				.Cascade(CascadeMode.StopOnFirstFailure)
				.NotNull()
				.NotEqual((ProductId)Guid.Empty);
		}
	}
}
