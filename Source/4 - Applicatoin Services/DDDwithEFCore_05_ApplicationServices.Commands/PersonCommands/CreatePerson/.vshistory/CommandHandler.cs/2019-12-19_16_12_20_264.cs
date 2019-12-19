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
		private readonly IRepositoryFactory _repositoryFactory;
		private readonly IRepository<Person> _repository;
		private readonly IValidator<CreatePersonCommand> _validator;

		public CommandHandler(IRepositoryFactory repositoryFactory, IValidator<CreatePersonCommand> validator)
		{
			_repositoryFactory = repositoryFactory ?? throw new ArgumentNullException(nameof(repositoryFactory));
			_repository = _repositoryFactory.CreateRepository<Person>();
			_validator = validator ?? throw new ArgumentNullException(nameof(validator));
		}

		#region Overrides of AsyncRequestHandler<CreateProductCommand>

		protected override async Task Handle(CreateProductCommand request, CancellationToken cancellationToken)
		{
			await this._validator.ValidateAndThrowAsync(request, null, cancellationToken);

			var product = Product.Create(request.ProductName);

			await this._repository.AddAsync(product);
		}

		#endregion
	}
}
//TODO: Missing UnitTest