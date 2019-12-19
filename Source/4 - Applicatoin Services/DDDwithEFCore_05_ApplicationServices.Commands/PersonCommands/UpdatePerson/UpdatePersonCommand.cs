using DDDwithEFCore_03_ApplicationCore.DomainModels.People;

using MediatR;

namespace DDDwithEFCore_05_ApplicationServices.Commands.PersonCommands.UpdatePerson
{
	public class UpdatePersonCommand : IRequest
	{
		public PersonId PersonId { get; set; }
		public string PersonName { get; set; }
	}
}
