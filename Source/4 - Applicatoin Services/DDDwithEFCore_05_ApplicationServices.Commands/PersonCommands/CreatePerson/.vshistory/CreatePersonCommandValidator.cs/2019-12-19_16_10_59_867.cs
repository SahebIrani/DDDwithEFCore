using FluentValidation;

namespace DDDwithEFCore_05_ApplicationServices.Commands.PersonCommands.CreatePerson
{
	public class CreatePersonCommandValidator : AbstractValidator<CreatePersonCommand>
	{
		public CreateProductCommandValidator()
		{
			RuleFor(x => x.ProductName)
				.Cascade(CascadeMode.StopOnFirstFailure)
				.NotNull()
				.NotEmpty();
		}
	}
}
