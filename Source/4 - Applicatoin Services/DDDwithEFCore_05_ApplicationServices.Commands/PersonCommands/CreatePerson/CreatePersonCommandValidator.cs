using FluentValidation;

namespace DDDwithEFCore_05_ApplicationServices.Commands.PersonCommands.CreatePerson
{
	public class CreatePersonCommandValidator : AbstractValidator<CreatePersonCommand>
	{
		public CreatePersonCommandValidator()
		{
			RuleFor(x => x.PersonName)
				.Cascade(CascadeMode.StopOnFirstFailure)
				.NotNull()
				.NotEmpty();
		}
	}
}
