namespace Globo.Authentication.Exceptions
{
	using System;

	public class UnauthorizedException : Exception
    {
		public UnauthorizedException(string email) :
			base("Access denied.")
		{ }
	}
}
