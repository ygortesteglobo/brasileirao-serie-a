namespace Globo.Authentication
{
	using Newtonsoft.Json;
	using System;
	using System.Collections.Generic;

	public class User
    {
		[JsonProperty("email")]
		public string Email { get; set; }

		[JsonProperty("password")]
		public string Password { get; set; }

		[JsonProperty("tokenId")]
		public string TokenId { get; set; }

		[JsonIgnore]
		public bool IsPersisted { get; set; }

		internal void Validate()
		{
			List<ArgumentNullException> errors = new List<ArgumentNullException>(3);
			if (string.IsNullOrWhiteSpace(Email))
				errors.Add(new ArgumentNullException("email"));
			if (string.IsNullOrWhiteSpace(Password))
				errors.Add(new ArgumentNullException("password"));
		}
	}
}
