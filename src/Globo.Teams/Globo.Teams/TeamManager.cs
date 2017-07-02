namespace Globo.Teams
{
	using Globo.Teams.Storage;
	using System;
	using System.Collections.Generic;
	using System.Threading;
	using System.Threading.Tasks;

	public class TeamManager
    {
		private ITeamRepository repository = null;

		private static TeamManager instance = null;
		public static TeamManager Instance
		{
			get
			{
				if (instance == null)
					throw new Exception("You must inject the storage dependency");

				return instance;
			}
			set
			{
				instance = value;
			}
		}

		public TeamManager(ITeamRepository repository)
		{
			this.repository = repository ?? throw new ArgumentException("Cannot init TeamManager with null repository.", "repository");
		}

		public async Task<Team> SaveAsync(Team team, CancellationToken cancellationToken)
		{
			cancellationToken.ThrowIfCancellationRequested();

			team.CreateNewId();
			team.Validate();

			await repository.SaveAsync(team, cancellationToken);
			return team;
		}

		public async Task<Team> GetAsync(string id, CancellationToken cancellationToken)
		{
			cancellationToken.ThrowIfCancellationRequested();

			ValidateId(id);

			Team team = await repository.GetAsync(id, cancellationToken);
			if (team != null) team.IsPersisted = true;
			return team;
		}

		public async Task DeleteAsync(string id, CancellationToken cancellationToken)
		{
			cancellationToken.ThrowIfCancellationRequested();

			ValidateId(id);

			await repository.DeleteAsync(id, cancellationToken);
		}

		public async Task<List<Team>> GetAllAsync(CancellationToken cancellationToken)
		{
			cancellationToken.ThrowIfCancellationRequested();

			List<Team> teams = await repository.GetAllAsync(cancellationToken);
			Parallel.ForEach(teams, item => {
				if(item != null) item.IsPersisted = true;
			});
			return teams;
		}


		private void ValidateId(string id)
		{
			if (string.IsNullOrWhiteSpace(id))
				throw new ArgumentNullException("The id cannot be null when get a team.");
		}
	}
}
