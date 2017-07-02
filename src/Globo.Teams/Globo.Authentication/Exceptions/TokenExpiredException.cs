namespace Globo.Authentication.Exceptions
{
	using System;

	public class TokenExpiredException : Exception
    {
		public TokenExpiredException() :
			base("Token expired")
		{ }
	}
}
