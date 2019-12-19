using System;

using Dapper;

using DDDwithEFCore_03_ApplicationCore.DomainModels.TypeDescriptors;

using DDDwithEFCore_06_ApplicationServices.Queries.DbConnectionFactories;

using FluentValidation;

using MediatR;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DDDwithEFCore_06_ApplicationServices.Queries.Registrations
{
	public static class ApplicationServiceQueriesRegistration
	{
		public static IServiceCollection AddApplicationQueries(this IServiceCollection services)
		{
			services.AddScoped<SqlServerDbConnectionFactory>(provider =>
			{
				IConfiguration configuration = provider.GetRequiredService<IConfiguration>();
				string connectionString = configuration.GetConnectionString("AppDbContext");
				return new SqlServerDbConnectionFactory(connectionString);
			});

			services.AddMediatR(typeof(SqlServerDbConnectionFactory).Assembly);
			services.AddValidatorsFromAssembly(typeof(SqlServerDbConnectionFactory).Assembly);

			StronglyTypedIdTypeDescriptor.AddStronglyTypedIdConverter((idType) =>
			{
				Type idTypeHandler = typeof(StronglyTypedIdMapper<>).MakeGenericType(idType);

				SqlMapper.ITypeHandler idTypeHandlerInstance =
					(SqlMapper.ITypeHandler)Activator.CreateInstance(idTypeHandler);

				SqlMapper.AddTypeHandler(idType, idTypeHandlerInstance);
			});

			return services;
		}
	}
}
