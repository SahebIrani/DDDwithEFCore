using System;

namespace DDDwithEFCore_02_Common.Infrastructures.EFCore.Exceptions
{
	public class DatabaseCannotConnectException : InfrastructureExceptionBase
	{
		public DatabaseCannotConnectException() : base(message: "Database cannot connect.")
		{
		}

		public DatabaseCannotConnectException(string message) : base(message: message)
		{
		}

		public DatabaseCannotConnectException(
			string message,
			Exception innerException) : base(
			message: message,
			innerException: innerException)
		{
		}
	}
}
