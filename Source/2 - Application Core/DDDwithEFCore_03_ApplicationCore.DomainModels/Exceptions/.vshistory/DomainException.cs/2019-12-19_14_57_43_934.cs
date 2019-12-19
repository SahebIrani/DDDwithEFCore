using System;

namespace DDDwithEFCore_03_ApplicationCore.DomainModels.Exceptions
{
	public class DomainException : ExceptionBase
	{
		public DomainException(string message) : base(message: message)
		{
		}

		public DomainException(
			string message,
			Exception innerException) : base(
				message: message,
				innerException: innerException)
		{
		}
	}
}
