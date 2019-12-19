using System;
using System.Collections.Generic;

namespace DDDwithEFCore_01_Common.Core.Models.vshistory.IdentityBase.cs
{
	public abstract class IdentityBase : ValueObjectBase
	{
		protected IdentityBase(Guid id) => Id = id;

		public Guid Id { get; protected set; }


		protected override IEnumerable<object> GetEqualityComponents()
		{
			yield return Id;
		}


		public override string ToString() => $"{GetType().Name}:{Id}";


		public static implicit operator Guid(IdentityBase id) => id.Id;
	}
}
