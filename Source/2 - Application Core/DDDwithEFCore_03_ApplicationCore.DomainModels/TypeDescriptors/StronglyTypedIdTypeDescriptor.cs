using System;
using System.Linq;
using System.Reflection;

using DDDwithEFCore_01_Common.Core.Models;

namespace DDDwithEFCore_03_ApplicationCore.DomainModels.TypeDescriptors
{
	public static class StronglyTypedIdTypeDescriptor
	{
		public static void AddStronglyTypedIdConverter(Action<Type> additionalAction) => Assembly.GetExecutingAssembly()
				.ExportedTypes
					.Where(x => !x.IsGenericTypeDefinition &&
								!x.IsAbstract &&
								x.BaseType == typeof(IdentityBase)
					)
					.ToList()
					.ForEach(idType =>
					{
						additionalAction?.Invoke(idType);
					})
		;
	}
}
