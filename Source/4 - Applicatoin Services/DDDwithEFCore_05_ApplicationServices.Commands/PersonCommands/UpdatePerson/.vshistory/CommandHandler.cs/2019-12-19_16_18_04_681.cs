using System;
using System.Threading;
using System.Threading.Tasks;

using DDDwithEFCore_01_Common.Core.Contracts;

using DDDwithEFCore_03_ApplicationCore.DomainModels.People;

using FluentValidation;

using MediatR;

namespace DDDwithEFCore_05_ApplicationServices.Commands.PersonCommands.UpdatePerson
{
	public class CommandHandler : AsyncRequestHandler<UpdatePersonCommand>
	{
		private readonly IRepositoryFactory _repositoryFactory;
		private readonly IRepository<Person> _repository;
		private readonly IValidator<UpdatePersonCommand> _validator;

		public CommandHandler(IRepositoryFactory repositoryFactory, IValidator<UpdatePersonCommand> validator)
		{
			_repositoryFactory =
				repositoryFactory ?? throw new ArgumentNullException(nameof(repositoryFactory));

			_repository = _repositoryFactory.CreateRepository<Person>();

			_validator = validator ?? throw new ArgumentNullException(nameof(validator));
		}

		protected override async Task Handle(UpdatePersonCommand request, CancellationToken cancellationToken)
		{
			await _validator.ValidateAndThrowAsync(request, null, cancellationToken);

			Person product = await _repository.FindOneAsync(x => x.PersonId == request.PersonId);

			product.ChangeName(request.PersonName);

			await _repository.UpdateAsync(product);
		}

	}
}
