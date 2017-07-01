namespace Globo.Teams
{
	using Globo.Teams.Storage;
	using System;
	using System.Collections.Generic;
	using System.Threading;
	using System.Threading.Tasks;

	public class TeamManager
    {
		private ITeamPersistence persistence = null;

		private static TeamManager instance = null;
		public static TeamManager Instance
		{
			get
			{
				if (instance == null)
					throw new Exception("You must inject the persistance dependency");

				return instance;
			}
			set
			{
				instance = value;
			}
		}

		public TeamManager(ITeamPersistence persistence)
		{
			this.persistence = persistence ?? throw new ArgumentException("TeamPersistence cannot be null.", "persistence");
		}

		public async Task<Team> SaveAsync(Team team, CancellationToken cancellationToken)
		{
			cancellationToken.ThrowIfCancellationRequested();

			team.CreateNewId();
			team.Validate();

			await persistence.SaveAsync(team, cancellationToken);
			return team;
		}

		public async Task<Team> GetAsync(string id, CancellationToken cancellationToken)
		{
			cancellationToken.ThrowIfCancellationRequested();

			ValidateId(id);

			Team team = await persistence.GetAsync(id, cancellationToken);
			if (team != null) team.IsPersisted = true;
			return team;
		}

		public async Task DeleteAsync(string id, CancellationToken cancellationToken)
		{
			cancellationToken.ThrowIfCancellationRequested();

			ValidateId(id);

			await persistence.DeleteAsync(id, cancellationToken);
		}

		public async Task<List<Team>> GetAllAsync(CancellationToken cancellationToken)
		{
			cancellationToken.ThrowIfCancellationRequested();

			List<Team> teams = await persistence.GetAllAsync(cancellationToken);
			Parallel.ForEach(teams, item => item.IsPersisted = true);
			return teams;
		}


		private void ValidateId(string id)
		{
			if (string.IsNullOrWhiteSpace(id))
				throw new ArgumentNullException("The id cannot be null when get a team.");
		}
	}
}
