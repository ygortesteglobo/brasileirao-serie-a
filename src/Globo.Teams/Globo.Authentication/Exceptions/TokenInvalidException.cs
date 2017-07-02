namespace Globo.Authentication.Exceptions
{
	using System;

	public class TokenInvalidException : Exception
	{
		public TokenInvalidException() :
			base("The token is invalid.")
		{ }
	}
}
