namespace DDDwithEFCore_01_Common.Core.Models
{
	public abstract class AggregateRoot : EntityBase
	{
		protected AggregateRoot() { }
		protected AggregateRoot(IdentityBase id) : base(id: id) { }
	}
}
