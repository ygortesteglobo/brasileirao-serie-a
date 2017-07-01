namespace Globo.Teams.IntegratedTest
{
	using Globo.Teams.DynamoDb;
	using Microsoft.VisualStudio.TestTools.UnitTesting;
	using System;
	using System.Collections.Generic;
	using System.Threading;
	using System.Threading.Tasks;

	[TestClass]
	public class TeamManagerTest
	{
		private static TimeSpan Timeout = TimeSpan.FromSeconds(1200);

		[ClassInitialize]
		public static void Setup(TestContext context)
		{
			TeamManager.Instance = new TeamManager(new DynamoDbPersistence());
		}

		[TestMethod]
		public async Task Should_create_update_delete_team()
		{
			// Arrange
			CancellationTokenSource ts = new CancellationTokenSource(Timeout);
			Exception exception = null;
			Team team = new Team()
			{
				Name = "Sample club",
				ShortName = null,
				Image = "http://test.com.br/image.jpg"
			};

			// Act
			try
			{
				team = await TeamManager.Instance.SaveAsync(team, ts.Token);

				Team teampersisted = await TeamManager.Instance.GetAsync(team.Id, ts.Token);
				if (teampersisted == null) throw new Exception("Team was not persisted.");

				string newName = DateTime.Now.Ticks.ToString();
				teampersisted.Name = newName;
				team = await TeamManager.Instance.SaveAsync(teampersisted, ts.Token);

				Team teampersisted2 = await TeamManager.Instance.GetAsync(team.Id, ts.Token);
				if (teampersisted2 == null || !teampersisted2.Name.Equals(newName)) throw new Exception("Team was not updated.");

				await TeamManager.Instance.DeleteAsync(teampersisted.Id, ts.Token);
			}
			catch (Exception e)
			{
				exception = e;
			}
			finally
			{
			}

			// Assert
			Assert.IsNull(exception);
			Assert.IsNotNull(team.Id);
		}

		[TestMethod]
		public async Task Should_get_all_teams()
		{
			// Arrange
			CancellationTokenSource ts = new CancellationTokenSource(Timeout);
			Exception exception = null;
			List<Team> teams = null;

			// Act
			try
			{
				teams = await TeamManager.Instance.GetAllAsync(ts.Token);
			}
			catch (Exception e)
			{
				exception = e;
			}

			// Assert
			Assert.IsNull(exception);
			Assert.IsNotNull(teams);
			Assert.IsTrue(teams.Count > 0);
		}
	}
}
