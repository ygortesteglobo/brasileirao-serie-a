namespace Globo.Authentication
{
	using Globo.Authentication.Exceptions;
	using System;
	using System.Threading;
	using System.Threading.Tasks;

	public class AuthenticationManager
	{
		public static AuthenticationManager Instance = new AuthenticationManager();

		public async Task<string> GetUserTokenAsync(string email, string password, CancellationToken cancellationToken)
		{
			User user = await UserManager.Instance.GetAsync(email, cancellationToken);
			if (user == null)
				throw new UnauthorizedException(email);

			bool passMatch = Cryptography.ValidateHash(user.Password, password);
			if (passMatch)
			{
				TokenData token = TokenHelper.Generate(email);
				string tokenString = token.GetTokenString();
				user.TokenId = token.Id.ToString();
				await UserManager.Instance.SaveAsync(user, cancellationToken);
				return tokenString;
			}
			else
				throw new WrongPasswordException();
		}


		/// <summary>
		/// Validate if token is expired or the tokenId is not equal that is persisted by client
		/// </summary>
		/// <param name="token"></param>
		public async Task ThrowIfTokenIsInvalidAsync(string token, CancellationToken cancellationToken)
		{
			TokenData tokenData = TokenHelper.GetByToken(token);
			if (DateTime.Now > tokenData.Expiration)
				throw new TokenExpiredException();
			User user = await UserManager.Instance.GetAsync(tokenData.User, cancellationToken);
			if(string.IsNullOrWhiteSpace(user.TokenId) || !tokenData.Id.Equals(user.TokenId))
			{
				throw new TokenInvalidException();
			}
		}
	}
}