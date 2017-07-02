namespace Globo.Authentication
{
	using Newtonsoft.Json;
	using System;

	internal class TokenHelper
	{
		private const string tokenFormat = "{0}-{1}-{2}";

		public static TokenData Generate(string email)
		{
			return new TokenData(Guid.NewGuid().ToString(), email);
		}

		public static TokenData GetByToken(string token)
		{
			
			try
			{
				byte[] bytes = Convert.FromBase64String(token);
				string json = System.Text.Encoding.UTF8.GetString(bytes);
				return JsonConvert.DeserializeObject<TokenData>(json);
			}
			catch (Exception)
			{
				return null;
			}
		}
	}
}
