using System;

using FluentValidation;

namespace DDDwithEFCore_06_ApplicationServices.Queries.PersonQueries.GetPersonDetail
{
	public class GetPersonDetailRequestValidator : AbstractValidator<GetPersonDetailRequest>
	{
		public GetPersonDetailRequestValidator()
		{
			RuleFor(x => x.PersonId)
				.Cascade(CascadeMode.StopOnFirstFailure)
				.NotNull()
				.NotEqual((PersonId)Guid.Empty);
		}
	}
}
