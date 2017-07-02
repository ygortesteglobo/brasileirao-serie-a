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

	public class DynamoDbPersistence : ITeamRepository
	{
		private AmazonDynamoDBClient client;
		public DynamoDbPersistence()
		{
			AmazonDynamoDBConfig clientConfig = new AmazonDynamoDBConfig();
			clientConfig.RegionEndpoint = RegionEndpoint.USEast1;
			client = new AmazonDynamoDBClient(clientConfig);
		}

		public async Task DeleteAsync(string id, CancellationToken cancellationToken)
		{
			cancellationToken.ThrowIfCancellationRequested();

			DeleteItemRequest deleteItem = DynamoDbParser.GetDeleteItemRequest(id);
			await client.DeleteItemAsync(deleteItem, cancellationToken);
		}

		public async Task<List<Team>> GetAllAsync(CancellationToken cancellationToken)
		{
			cancellationToken.ThrowIfCancellationRequested();

			ScanRequest scanRequest = DynamoDbParser.GetScanRequest();
			ScanResponse scanResponse = await client.ScanAsync(scanRequest, cancellationToken);
			return DynamoDbParser.GetListTeam(scanResponse);
		}

		public async Task<Team> GetAsync(string id, CancellationToken cancellationToken)
		{
			cancellationToken.ThrowIfCancellationRequested();

			GetItemRequest getItem = DynamoDbParser.GetGetItemRequest(id);
			GetItemResponse responseItem = await client.GetItemAsync(getItem, cancellationToken);
			return DynamoDbParser.GetTeam(responseItem);
		}

		public async Task SaveAsync(Team team, CancellationToken cancellationToken)
		{
			cancellationToken.ThrowIfCancellationRequested();
			PutItemRequest putItem = DynamoDbParser.GetPutItemRequest(team);
			await client.PutItemAsync(putItem, cancellationToken);
		}
	}
}
