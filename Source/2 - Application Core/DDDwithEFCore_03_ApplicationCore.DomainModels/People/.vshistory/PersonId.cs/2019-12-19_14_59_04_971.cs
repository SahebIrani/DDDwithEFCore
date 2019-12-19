using System;

using DDDwithEFCore_01_Common.Core.Models;

namespace DDDwithEFCore_03_ApplicationCore.DomainModels.People
{
	public class PersonId : IdentityBase
	{
		public PersonId(Guid id) : base(id) { }

		public static explicit operator PersonId(Guid id) => id == Guid.Empty ? null : new PersonId(id);
	}
}
