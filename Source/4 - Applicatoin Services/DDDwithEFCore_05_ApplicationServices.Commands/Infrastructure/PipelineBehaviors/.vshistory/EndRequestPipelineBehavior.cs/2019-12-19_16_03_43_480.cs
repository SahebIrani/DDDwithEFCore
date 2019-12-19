using System.Threading;
using System.Threading.Tasks;

using MediatR;

namespace DDDwithEFCore_05_ApplicationServices.Commands.Infrastructure.PipelineBehaviors
{
	public sealed class EndRequestPipelineBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
	{
		private readonly IRepositoryFactory _repositoryFactory;

		public EndRequestPipelineBehavior(IRepositoryFactory repositoryFactory)
			=> this._repositoryFactory = repositoryFactory;

		#region Implementation of IPipelineBehavior<in TRequest,TResponse>

		public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
		{
			using (this._repositoryFactory)
			{
				var response = await next();
				return response;
			}
		}

		#endregion
	}
}
