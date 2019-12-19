using System;
using System.Collections.Generic;

namespace DDDwithEFCore_01_Common.Core.Models.vshistory.IdentityBase.cs
{
	public abstract class IdentityBase : ValueObjectBase
	{
		#region Constructors
		protected IdentityBase(Guid id) => Id = id;

		#endregion
		public Guid Id { get; protected set; }

		#region Overrides of ValueObjectBase

		protected override IEnumerable<object> GetEqualityComponents()
		{
			yield return Id;
		}

		#endregion

		#region Overrides of Object

		public override string ToString() => $"{GetType().Name}:{Id}";

		#endregion

		public static implicit operator Guid(IdentityBase id) => id.Id;
	}
}
