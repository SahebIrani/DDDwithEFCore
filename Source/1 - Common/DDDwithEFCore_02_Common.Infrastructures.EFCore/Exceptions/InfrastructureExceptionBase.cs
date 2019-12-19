using System;

namespace DDDwithEFCore_02_Common.Infrastructures.EFCore.Exceptions
{
	public abstract class InfrastructureExceptionBase : Exception
	{
		protected InfrastructureExceptionBase(string message) : base(message: message)
		{
			if (string.IsNullOrEmpty(message))
				throw new ArgumentException("message IsNullOrEmpty", nameof(message));
		}

		protected InfrastructureExceptionBase(
			string message,
			Exception innerException) : base(
				message: message,
				innerException: innerException)
		{ }
	}
}
