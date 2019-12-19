using System;

namespace DDDwithEFCore_01_Common.Core.Models
{
	public abstract class EntityBase : IEquatable<EntityBase>
	{
		protected EntityBase(IdentityBase id) => Id = id;
		protected EntityBase() { }

		protected IdentityBase Id;


		public bool Equals(EntityBase other)
		{
			if (ReferenceEquals(null, other)) return false;
			if (other is null) return false;

			if (ReferenceEquals(this, other)) return true;

			return Equals(Id, other.Id);
		}

		public override bool Equals(object obj)
		{
			if (ReferenceEquals(null, obj)) return false;

			if (ReferenceEquals(this, obj)) return true;

			if (obj.GetType() != this.GetType()) return false;

			return Equals((EntityBase)obj);
		}


		public override int GetHashCode() => (GetType().GetHashCode() * 907) + Id.GetHashCode();

		public override string ToString() => $"{GetType().Name}#[Identity={Id}]";


		public static bool operator ==(EntityBase a, EntityBase b)
		{
			if (ReferenceEquals(a, null) && ReferenceEquals(b, null))
				return true;

			if (ReferenceEquals(a, null) || ReferenceEquals(b, null))
				return false;

			return a.Equals(b);
		}

		public static bool operator !=(EntityBase a, EntityBase b) => !(a == b);
	}
}
