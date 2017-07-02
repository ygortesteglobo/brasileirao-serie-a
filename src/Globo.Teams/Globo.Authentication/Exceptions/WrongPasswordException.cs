namespace Globo.Authentication.Exceptions
{
	using System;

	public class WrongPasswordException : Exception
	{
		public WrongPasswordException() :
			base("Wrong password.")
		{ }
	}
}
