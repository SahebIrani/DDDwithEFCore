using DDDwithEFCore_01_Common.Core.Contracts;

using DDDwithEFCore_03_ApplicationCore.DomainModels.People;

using FluentValidation;

namespace DDDwithEFCore_05_ApplicationServices.Commands.PersonCommands.UpdatePerson
{
	public class UpdatePersonCommandValidator : AbstractValidator<UpdatePersonCommand>
	{
		public UpdatePersonCommandValidator(IRepositoryFactory repositoryFactory)
		{
			RuleFor(x => x.PersonId)
				.Cascade(CascadeMode.StopOnFirstFailure)
				.NotNull()
				.Must(x => ProductMustExist(repositoryFactory, x))
				.WithMessage(x => $"Person#{x.PersonId} could not be found ..");

			RuleFor(x => x.PersonName)
				.Cascade(CascadeMode.StopOnFirstFailure)
				.NotNull()
				.NotEmpty();
		}

		private bool ProductMustExist(IRepositoryFactory repositoryFactory, PersonId personId)
		{
			IRepository<Person> repository = repositoryFactory.CreateRepository<Person>();
			Person product = repository.FindOneAsync(x => x.PersonId == personId).GetAwaiter().GetResult();
			return product != null;
		}
	}
}
