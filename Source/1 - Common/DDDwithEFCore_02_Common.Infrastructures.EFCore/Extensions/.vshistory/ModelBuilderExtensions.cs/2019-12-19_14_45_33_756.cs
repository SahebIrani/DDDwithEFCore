using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

using DDDEfCore.Core.Common.Models;

using Microsoft.EntityFrameworkCore;

namespace DDDwithEFCore_02_Common.Infrastructures.EFCore.Extensions
{
	public static class ModelBuilderExtensions
	{
		public static void Register(
			this ModelBuilder builder,
			IEnumerable<Assembly> fromAssemblies)
		{
			IEnumerable<TypeInfo> types = fromAssemblies.SelectMany(x => x.DefinedTypes);

			if (types?.Any() == true)
			{
				builder.RegisterEntities(types);
				builder.RegisterCustomMappings(types);
			}
		}

		private static void RegisterEntities(
			this ModelBuilder builder,
			IEnumerable<Type> fromTypes)
		{
			IEnumerable<Type> concreteTypes = fromTypes
				.Where(x => !x.GetTypeInfo().IsAbstract
							&& !x.GetTypeInfo().IsInterface
							&& x.BaseType != null
							&& x.IsConcreteOfAggregateRoot())
				;

			foreach (Type concreteType in concreteTypes)
				builder.Entity(concreteType);
		}

		private static void RegisterCustomMappings(
			this ModelBuilder builder,
			IEnumerable<Type> fromTypes)
		{
			IEnumerable<Type> customModelBuilderTypes =
				fromTypes.Where(predicate: x => x != null
				&& typeof(ICustomModelBuilder).IsAssignableFrom(x)
				&& x != typeof(ICustomModelBuilder))
			;

			foreach (Type builderType in customModelBuilderTypes)
			{
				ICustomModelBuilder customModelBuilder =
					(ICustomModelBuilder)Activator.CreateInstance(builderType);

				customModelBuilder.Build(builder);
			}
		}

		private static bool IsConcreteOfAggregateRoot(this Type type) =>
			typeof(AggregateRoot).IsAssignableFrom(type)
				   || (type.GetTypeInfo().BaseType.IsGenericType &&
					   typeof(AggregateRoot).IsAssignableFrom(type.GetTypeInfo().BaseType.GetGenericTypeDefinition()))
			;
	}
}
