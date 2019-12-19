using System;

using DDDwithEFCore_01_Common.Core.Models;

namespace DDDwithEFCore_03_ApplicationCore.DomainModels.People
{
	public class PersonId : IdentityBase
	{
		public PersonId(Guid id) : base(id) { }
	}
}
