using DDDwithEFCore_01_Common.Core.Contracts;

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
				.Must(x => this.ProductMustExist(repositoryFactory, x))
				.WithMessage(x => $"Person#{x.PersonId} could not be found ..");

			RuleFor(x => x.PersonName)
				.Cascade(CascadeMode.StopOnFirstFailure)
				.NotNull()
				.NotEmpty();
		}

		private bool ProductMustExist(IRepositoryFactory repositoryFactory, ProductId productId)
		{
			var repository = repositoryFactory.CreateRepository<Product>();
			var product = repository.FindOneAsync(x => x.ProductId == productId).Result;
			return product != null;
		}
	}
}
