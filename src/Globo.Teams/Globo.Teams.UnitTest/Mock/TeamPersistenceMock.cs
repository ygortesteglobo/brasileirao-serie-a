namespace Globo.Teams.UnitTest.Mock
{
	using Storage;
	using System.Collections.Generic;
	using System.Threading;
	using System.Threading.Tasks;

	internal class TeamPersistenceMock : ITeamPersistence
	{
		private List<Team> teams = new List<Team>();

		public TeamPersistenceMock()
		{
			teams.Add(TeamPersistenceMockHelper.FirstTeam);
			teams.Add(TeamPersistenceMockHelper.SecondTeam);
			teams.Add(TeamPersistenceMockHelper.ThirdTeam);
		}

		public async Task<Team> GetAsync(string id, CancellationToken cancellationToken)
		{
			return teams.Find(item => item.Id.Equals(id));
		}

		public async Task SaveAsync(Team team, CancellationToken cancellationToken)
		{
			teams.Add(team);
		}

		public async Task<List<Team>> GetAllAsync(CancellationToken cancellationToken)
		{
			return teams;
		}

		public async Task DeleteAsync(string id, CancellationToken cancellationToken)
		{
			teams.RemoveAll(item => item.Id.Equals(id));
		}
	}

	internal class TeamPersistenceMockHelper
	{
		public static Team FirstTeam = new Team()
		{
			Id = "testId",
			Name = "test Name",
			ShortName = "shortName",
			Image = "http://url.com/image.jpg"
		};

		public static Team SecondTeam = new Team()
		{
			Id = "testId-two",
			Name = "test Name two",
			ShortName = "shortNameTwo",
			Image = "http://url.com/image.jpg"
		};

		public static Team ThirdTeam = new Team()
		{
			Id = "testId-three",
			Name = "test Name three",
			ShortName = "shortNameThree",
			Image = "http://url.com/image.jpg"
		};
	}
}
