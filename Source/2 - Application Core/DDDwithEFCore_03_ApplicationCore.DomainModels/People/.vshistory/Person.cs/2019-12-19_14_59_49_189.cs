using DDDwithEFCore_01_Common.Core.Models;

namespace DDDwithEFCore_03_ApplicationCore.DomainModels.People
{
	public class Person : AggregateRoot
	{
		public Person() { }

		public Person(IdentityBase id) : base(id) { }

		public string Name { get; private set; }



	}
}
