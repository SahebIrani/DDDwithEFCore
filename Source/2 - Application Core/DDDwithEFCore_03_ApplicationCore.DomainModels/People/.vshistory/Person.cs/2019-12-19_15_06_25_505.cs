using DDDwithEFCore_01_Common.Core.Models;

using DDDwithEFCore_03_ApplicationCore.DomainModels.Exceptions;

namespace DDDwithEFCore_03_ApplicationCore.DomainModels.People
{
	public class Person : AggregateRoot
	{
		private Person() { }

		private Person(PersonId personId, string personName) : base(personId)
		{
			if (string.IsNullOrWhiteSpace(personName))
				throw new DomainException($"{nameof(personName)} is empty ..");

			Name = personName;
		}

		private Person(string personName) :
			this(IdentityFactory.Create<PersonId>(), personName)
		{ }

		public string Name { get; private set; }

		public PersonId PersonId => (PersonId)Id;


		/// <summary>
		/// Creations
		/// </summary>
		/// <param name="personName"></param>
		/// <returns>Person</returns>
		public static Person Create(string personName) => new Person(personName);


	}
}

