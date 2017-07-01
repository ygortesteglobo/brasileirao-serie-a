namespace Globo.Teams
{
	using Newtonsoft.Json;

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
    }
}
