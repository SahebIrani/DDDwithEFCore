using MediatR;

namespace DDDwithEFCore_05_ApplicationServices.Commands.PersonCommands.CreatePerson
{
	public class CreatePersonCommand : IRequest
	{
		public string PersonName { get; set; }
	}
}
