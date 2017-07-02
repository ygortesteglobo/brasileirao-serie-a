namespace Globo.Authentication
{
	using Newtonsoft.Json;
	using System;

	public class TokenData
    {
		public string Id { get; set; }
		public string User { get; set; }
		public DateTime Expiration { get; set; }

		public TokenData() { }
		public TokenData(string id, string user)
		{
			Id = id;
			User = user;
			Expiration = DateTime.Now.AddDays(1);
		}

		public string GetTokenString()
		{
			string json = JsonConvert.SerializeObject(this);
			byte[] bytes = System.Text.Encoding.UTF8.GetBytes(json);
			return Convert.ToBase64String(bytes);
		}
	}
}
