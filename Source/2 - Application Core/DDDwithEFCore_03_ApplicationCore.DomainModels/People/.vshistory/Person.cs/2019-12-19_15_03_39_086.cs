using DDDwithEFCore_01_Common.Core.Models;

using DDDwithEFCore_03_ApplicationCore.DomainModels.Exceptions;

namespace DDDwithEFCore_03_ApplicationCore.DomainModels.People
{
	public class Person : AggregateRoot
	{

		public PersonId PersonId => (PersonId)Id;
		public string Name { get; private set; }


		private Person(PersonId personId, string personName) : base(personId)
		{
			if (string.IsNullOrWhiteSpace(personName))
				throw new DomainException($"{nameof(personName)} is empty ..");

			Name = personName;
		}

		private Person(string personName) : this(IdentityFactory.Create<PersonId>(), personName)
		{ }

		private Person() { }
	}
}
