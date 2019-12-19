using System;

namespace DDDwithEFCore_05_ApplicationServices.Commands.Exceptions
{
    public class NotFoundEntityException : ApplicationServiceCommandsException
    {
        public NotFoundEntityException(string message) : base(message)
        {
        }

        public NotFoundEntityException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
