using System.ComponentModel;
using System.Reflection;

using DDDwithEFCore_04_Infrastructures.EFCore.Registrations;

using DDDwithEFCore_05_ApplicationServices.Commands.Infrastructure.PipelineBehaviors;

using FluentValidation;

using MediatR;

using Microsoft.Extensions.DependencyInjection;

namespace DDDwithEFCore_05_ApplicationServices.Commands.Infrastructure
{
	public static class ApplicationServiceCommandsRegistration
	{
		public static IServiceCollection AddApplicationCommands(this IServiceCollection services)
		{
			Assembly assembly = Assembly.GetExecutingAssembly();
			services.AddEfCoreSqlServerDb();
			services.AddMediatR(assembly);
			services.AddValidatorsFromAssembly(assembly);
			services.AddScoped(typeof(IPipelineBehavior<,>), typeof(EndRequestPipelineBehavior<,>));

			StronglyTypedIdTypeDescriptor.AddStronglyTypedIdConverter((idType) =>
			{
				var typeOfIdentity = typeof(StronglyTypedIdConverter<>).MakeGenericType(idType);
				TypeDescriptor.AddAttributes(idType, new TypeConverterAttribute(typeOfIdentity));
			});

			return services;
		}
	}
}
