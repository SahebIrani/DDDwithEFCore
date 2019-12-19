using MediatR;

namespace DDDwithEFCore_05_ApplicationServices.Commands.PersonCommands.CreatePerson
{
    public class CreateProductCommand : IRequest
    {
        public string ProductName { get; set; }
    }
}
