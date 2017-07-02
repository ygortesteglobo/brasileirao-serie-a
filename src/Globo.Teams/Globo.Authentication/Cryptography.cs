namespace Globo.Authentication
{
	using Microsoft.AspNetCore.Cryptography.KeyDerivation;
	using System;
	using System.Security.Cryptography;

	public class Cryptography
	{
		private static char[] splitter = new char[] { ':' };
		private const string hashformat = "{0}{1}{2}";

		public static string GenerateHash(string password)
		{
			return GenerateHash(password, null);
		}

		public static bool ValidateHash(string hash, string word)
		{
			string[] hashSplitted = hash.Split(splitter);
			string wordHashed = GenerateHash(word, hashSplitted[0]);
			return string.Equals(hash, wordHashed);
		}

		private static string GenerateHash(string password, string saltHashed = null)
		{
			byte[] salt = new byte[128 / 8];
			if (string.IsNullOrWhiteSpace(saltHashed))
				using (var rng = RandomNumberGenerator.Create())
					rng.GetBytes(salt);
			else
				salt = Convert.FromBase64String(saltHashed);

			return MountHash(salt, KeyDerivation.Pbkdf2(
				password: password,
				salt: salt,
				prf: KeyDerivationPrf.HMACSHA1,
				iterationCount: 10000,
				numBytesRequested: 256 / 8));
		}

		private static string MountHash(byte[] salt, byte[] keyDerivation)
		{
			return string.Format(hashformat, Convert.ToBase64String(salt), splitter[0], Convert.ToBase64String(keyDerivation));
		}
	}
}
