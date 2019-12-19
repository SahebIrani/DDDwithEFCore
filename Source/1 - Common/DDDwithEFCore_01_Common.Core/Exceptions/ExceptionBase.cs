using System;

namespace DDDwithEFCore_01_Common.Core.Exceptions
{
	public abstract class ExceptionBase : Exception
	{
		protected ExceptionBase(string message) : base(message: message) { }
		protected ExceptionBase(
			string message,
			Exception innerException)
			: base(
				  message: message,
				  innerException: innerException)
		{ }
	}
}
