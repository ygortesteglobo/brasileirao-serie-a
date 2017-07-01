namespace Globo.Teams.DynamoDb
{
	using System;
	using System.Collections.Generic;
	using System.Threading;
	using System.Threading.Tasks;
	using Globo.Teams.Storage;
	using Amazon.DynamoDBv2;
	using Amazon;
	using Amazon.DynamoDBv2.Model;

	public class DynamoDbPersistence : ITeamPersistence
	{
		private static string tableName = "globo_teams";
		private AmazonDynamoDBClient client;
		public DynamoDbPersistence()
		{
			AmazonDynamoDBConfig clientConfig = new AmazonDynamoDBConfig();
			clientConfig.RegionEndpoint = RegionEndpoint.USEast1;
			AmazonDynamoDBClient client = new AmazonDynamoDBClient(clientConfig);
		}

		public Task DeleteAsync(string id, CancellationToken cancellationToken)
		{
			throw new NotImplementedException();
		}

		public Task<List<Team>> GetAllAsync(CancellationToken cancellationToken)
		{
			throw new NotImplementedException();
		}

		public Task<Team> GetAsync(string id, CancellationToken cancellationToken)
		{
			throw new NotImplementedException();
		}

		public async Task SaveAsync(Team team, CancellationToken cancellationToken)
		{
			PutItemRequest putItem = new PutItemRequest(tableName, new Dictionary<string, AttributeValue>()
			{
				{ "id", new AttributeValue(){ S = team.Id } },
				{ "name", new AttributeValue(){ S = team.Name } },
				{ "shortName", new AttributeValue(){ S = team.ShortName } },
				{ "image", new AttributeValue(){ S = team.Image } }
			});
			await client.PutItemAsync(putItem, cancellationToken);
		}
	}
}
