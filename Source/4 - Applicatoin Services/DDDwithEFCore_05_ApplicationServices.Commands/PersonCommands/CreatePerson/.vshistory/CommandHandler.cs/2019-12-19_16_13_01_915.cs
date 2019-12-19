using System;
using System.Threading;
using System.Threading.Tasks;

using DDDwithEFCore_01_Common.Core.Contracts;

using DDDwithEFCore_03_ApplicationCore.DomainModels.People;

using FluentValidation;

using MediatR;

namespace DDDwithEFCore_05_ApplicationServices.Commands.PersonCommands.CreatePerson
{
	public class CommandHandler : AsyncRequestHandler<CreatePersonCommand>
	{
		public CommandHandler(IRepositoryFactory repositoryFactory, IValidator<CreatePersonCommand> validator)
		{
			_repositoryFactory = repositoryFactory ?? throw new ArgumentNullException(nameof(repositoryFactory));
			_repository = _repositoryFactory.CreateRepository<Person>();
			_validator = validator ?? throw new ArgumentNullException(nameof(validator));
		}

		private readonly IRepositoryFactory _repositoryFactory;
		private readonly IRepository<Person> _repository;
		private readonly IValidator<CreatePersonCommand> _validator;


		protected override async Task Handle(CreatePersonCommand request, CancellationToken cancellationToken)
		{
			await _validator.ValidateAndThrowAsync(request, null, cancellationToken);

			Person product = Person.Create(request.PersonName);

			await _repository.AddAsync(product);
		}
	}
}