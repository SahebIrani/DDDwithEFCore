using System.Threading;
using System.Threading.Tasks;

using DDDwithEFCore_01_Common.Core.Contracts;

using MediatR;

namespace DDDwithEFCore_05_ApplicationServices.Commands.Infrastructure.PipelineBehaviors
{
	public sealed class EndRequestPipelineBehavior<TRequest, TResponse>
		: IPipelineBehavior<TRequest, TResponse>
	{
		public EndRequestPipelineBehavior(IRepositoryFactory repositoryFactory)
			=> _repositoryFactory = repositoryFactory;

		private readonly IRepositoryFactory _repositoryFactory;

		public async Task<TResponse> Handle(
			TRequest request,
			CancellationToken cancellationToken,
			RequestHandlerDelegate<TResponse> next)
		{
			using (_repositoryFactory)
			{
				TResponse response = await next();
				return response;
			}
		}
	}
}
