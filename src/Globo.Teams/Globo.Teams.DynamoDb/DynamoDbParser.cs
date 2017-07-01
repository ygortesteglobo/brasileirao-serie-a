namespace Globo.Teams.DynamoDb
{
	using Amazon.DynamoDBv2.Model;
	using System.Collections.Generic;
	using System;
	using System.Threading.Tasks;

	internal class DynamoDbParser
	{
		private const string TABLE_NAME = "globo_teams";

		private const string FIELD_NAME_ID = "id";
		private const string FIELD_NAME_NAME = "name";
		private const string FIELD_NAME_SHORTNAME = "shortName";
		private const string FIELD_NAME_IMAGE = "image";

		public static PutItemRequest GetPutItemRequest(Team team)
		{
			Dictionary<string, AttributeValue> attributes = new Dictionary<string, AttributeValue>();

			attributes[FIELD_NAME_ID] = new AttributeValue() { S = team.Id };

			if (!string.IsNullOrWhiteSpace(team.Name))
				attributes[FIELD_NAME_NAME] = new AttributeValue() { S = team.Name };

			if (!string.IsNullOrWhiteSpace(team.ShortName))
				attributes[FIELD_NAME_SHORTNAME] = new AttributeValue() { S = team.ShortName };

			if (!string.IsNullOrWhiteSpace(team.Image))
				attributes[FIELD_NAME_IMAGE] = new AttributeValue() { S = team.Image };

			return new PutItemRequest(TABLE_NAME, attributes);
		}

		public static GetItemRequest GetGetItemRequest(string id)
		{
			return new GetItemRequest(TABLE_NAME, new Dictionary<string, AttributeValue>()
			{
				{ FIELD_NAME_ID, new AttributeValue(){S = id} }
			});
		}

		internal static ScanRequest GetScanRequest()
		{
			return new ScanRequest(TABLE_NAME);
		}

		internal static List<Team> GetListTeam(ScanResponse scanResponse)
		{
			List<Team> teams = new List<Team>();
			
			Parallel.ForEach(scanResponse.Items, item =>
			{
				Team team = GetTeam(item);
				teams.Add(team);
			});

			return teams;
		}

		public static Team GetTeam(GetItemResponse responseItem)
		{
			return GetTeam(responseItem.Item);
		}

		private static Team GetTeam(Dictionary<string, AttributeValue> item)
		{
			Team team = new Team();
			team.Id = item[FIELD_NAME_ID].S;
			if (item.ContainsKey(FIELD_NAME_NAME))
				team.Name = item[FIELD_NAME_NAME].S;
			if (item.ContainsKey(FIELD_NAME_SHORTNAME))
				team.ShortName = item[FIELD_NAME_SHORTNAME].S;
			if (item.ContainsKey(FIELD_NAME_IMAGE))
				team.Image = item[FIELD_NAME_IMAGE].S;

			return team;
		}

		public static DeleteItemRequest GetDeleteItemRequest(string id)
		{
			return new DeleteItemRequest(TABLE_NAME, new Dictionary<string, AttributeValue>()
			{
				{ FIELD_NAME_ID, new AttributeValue(){S = id} }
			});
		}
	}
}
