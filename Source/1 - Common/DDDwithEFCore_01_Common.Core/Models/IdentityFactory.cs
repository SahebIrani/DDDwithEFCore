using System;
using System.Linq;
using System.Reflection;

namespace DDDwithEFCore_01_Common.Core.Models
{
	public static class IdentityFactory
	{
		public static TIdentity Create<TIdentity>() where TIdentity : IdentityBase
			=> Create<TIdentity>(id: Guid.NewGuid());

		public static TIdentity Create<TIdentity>(object id) where TIdentity : IdentityBase
		{
			if (id == null || (Guid)id == Guid.Empty) return null;

			ConstructorInfo identityConstructor = typeof(TIdentity)
					.GetConstructors(bindingAttr: BindingFlags.NonPublic | BindingFlags.Instance)
					.FirstOrDefault(x => x.GetParameters().Length > 0);

			object instance = identityConstructor?.Invoke(new object[] { id });

			return (TIdentity)instance;
		}
	}
}
