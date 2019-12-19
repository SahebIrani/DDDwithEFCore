using FluentValidation;

namespace DDDwithEFCore_06_ApplicationServices.Queries.PersonQueries.GetPersonCollection
{
	public class GetPersonCollectionRequestValidator : AbstractValidator<GetProductCollectionRequest>
	{
		public GetPersonCollectionRequestValidator()
		{
			RuleFor(x => x.PageIndex)
				.Cascade(CascadeMode.StopOnFirstFailure)
				.GreaterThan(0)
				.LessThan(int.MaxValue);

			RuleFor(x => x.PageSize)
				.Cascade(CascadeMode.StopOnFirstFailure)
				.GreaterThan(0)
				.LessThan(int.MaxValue);
		}
	}
}
