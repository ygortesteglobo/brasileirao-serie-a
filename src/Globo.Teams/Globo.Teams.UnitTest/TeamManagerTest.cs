namespace Globo.Teams.UnitTest
{
	using Globo.Teams.UnitTest.Mock;
	using Microsoft.VisualStudio.TestTools.UnitTesting;
	using System;
	using System.Collections.Generic;
	using System.Threading;
	using System.Threading.Tasks;

	[TestClass]
	public class TeamManagerTest
	{
		private static TimeSpan Timeout = TimeSpan.FromSeconds(120);

		[ClassInitialize]
		public static void Setup(TestContext context)
		{
			TeamManager.Instance = new TeamManager(new TeamPersistenceMock());
		}

		[TestMethod]
		public async Task Should_create_team()
		{
			// Arrange
			CancellationTokenSource ts = new CancellationTokenSource(Timeout);
			Exception exception = null;
			Team team = new Team()
			{
				Name = "Clube de Regatas Flamengo",
				ShortName = "Flamengo",
				Image = "http://flamengo.com.br/image.jpg"
			};

			// Act
			try
			{
				await TeamManager.Instance.SaveAsync(team, ts.Token);
			}
			catch (Exception e)
			{
				exception = e;
			}

			// Assert
			Assert.IsNull(exception);
			Assert.IsNotNull(team.Id);
		}

		[TestMethod]
		public async Task Should_get_team()
		{
			// Arrange
			CancellationTokenSource ts = new CancellationTokenSource(Timeout);
			Exception exception = null;
			string teamId = TeamPersistenceMockHelper.FirstTeam.Id;
			Team teamPersisted = null;

			// Act
			try
			{
				teamPersisted = await TeamManager.Instance.GetAsync(teamId, ts.Token);
			}
			catch (Exception e)
			{
				exception = e;
			}

			// Assert
			Assert.IsNull(exception);
			Assert.IsNotNull(teamPersisted);
			Assert.IsTrue(teamPersisted.IsPersisted);
		}

		[TestMethod]
		public async Task Should_update_team()
		{
			// Arrange
			CancellationTokenSource ts = new CancellationTokenSource(Timeout);
			Exception exception = null;
			Team teamPersisted = TeamPersistenceMockHelper.SecondTeam;
			string ticks = DateTime.Now.Ticks.ToString();
			teamPersisted.Name = ticks;
			Team teamPersistedToValidate = null;

			// Act
			try
			{
				teamPersistedToValidate = await TeamManager.Instance.GetAsync(teamPersisted.Id, ts.Token);
				if (teamPersistedToValidate == null) throw new Exception("TeamPersisted must be persisted.");
				await TeamManager.Instance.SaveAsync(teamPersisted, ts.Token);
				teamPersistedToValidate = await TeamManager.Instance.GetAsync(teamPersisted.Id, ts.Token);
			}
			catch (Exception e)
			{
				exception = e;
			}

			// Assert
			Assert.IsNull(exception);
			Assert.IsNotNull(teamPersistedToValidate);
			Assert.AreEqual(teamPersistedToValidate.Name, ticks);
		}

		[TestMethod]
		public async Task Should_delete_team()
		{
			// Arrange
			CancellationTokenSource ts = new CancellationTokenSource(Timeout);
			Exception exception = null;
			Team teamPersisted = TeamPersistenceMockHelper.ThirdTeam;
			Team teamPersistedToValidate = null;

			// Act
			try
			{
				teamPersistedToValidate = await TeamManager.Instance.GetAsync(teamPersisted.Id, ts.Token);
				if (teamPersistedToValidate == null) throw new Exception("TeamPersisted must be persisted.");
				await TeamManager.Instance.DeleteAsync(teamPersisted.Id, ts.Token);
				teamPersistedToValidate = await TeamManager.Instance.GetAsync(teamPersisted.Id, ts.Token);
			}
			catch (Exception e)
			{
				exception = e;
			}

			// Assert
			Assert.IsNull(exception);
			Assert.IsNull(teamPersistedToValidate);
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
