using System;
using System.Threading;
using System.Threading.Tasks;

using DDDwithEFCore_01_Common.Core.Contracts;

using FluentValidation;

using MediatR;

namespace DDDwithEFCore_05_ApplicationServices.Commands.PersonCommands.UpdatePerson
{
	public class CommandHandler : AsyncRequestHandler<UpdatePersonCommand>
	{
		private readonly IRepositoryFactory _repositoryFactory;
		private readonly IRepository<Product> _repository;
		private readonly IValidator<UpdatePersonCommand> _validator;

		public CommandHandler(IRepositoryFactory repositoryFactory, IValidator<UpdatePersonCommand> validator)
		{
			this._repositoryFactory = repositoryFactory ?? throw new ArgumentNullException(nameof(repositoryFactory));
			this._repository = this._repositoryFactory.CreateRepository<Product>();
			this._validator = validator ?? throw new ArgumentNullException(nameof(validator));
		}

		protected override async Task Handle(UpdateProductCommand request, CancellationToken cancellationToken)
		{
			await this._validator.ValidateAndThrowAsync(request, null, cancellationToken);

			var product = await this._repository.FindOneAsync(x => x.ProductId == request.ProductId);
			product.ChangeName(request.ProductName);

			await this._repository.UpdateAsync(product);
		}

	}
}
