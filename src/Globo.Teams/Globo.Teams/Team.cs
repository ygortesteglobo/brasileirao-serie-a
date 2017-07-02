namespace Globo.Teams
{
	using Newtonsoft.Json;
	using System;
	using System.Collections.Generic;

	public class Team
    {
		[JsonProperty("id")]
		public string Id { get; set; }

		[JsonProperty("name")]
		public string Name { get; set; }

		[JsonProperty("shortName")]
		public string ShortName { get; set; }

		[JsonProperty("image")]
		public string Image { get; set; }

		[JsonIgnore]
		public bool IsPersisted { get; set; }


		internal void Validate()
		{
			List<ArgumentNullException> errors = new List<ArgumentNullException>(3);
			if (string.IsNullOrWhiteSpace(Id))
				errors.Add(new ArgumentNullException("id"));
			if (string.IsNullOrWhiteSpace(Name))
				errors.Add(new ArgumentNullException("name"));
			if (string.IsNullOrWhiteSpace(ShortName))
				errors.Add(new ArgumentNullException("shortName"));
		}

		internal void CreateNewId()
		{
			if (string.IsNullOrWhiteSpace(Id))
				Id = Guid.NewGuid().ToString();
			else
				IsPersisted = true;
		}
	}
}
