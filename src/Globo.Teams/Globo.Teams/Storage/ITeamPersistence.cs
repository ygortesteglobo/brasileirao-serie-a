namespace Globo.Teams.Storage
{
	using System.Collections.Generic;
	using System.Threading;
	using System.Threading.Tasks;

	public interface ITeamPersistence
    {
		Task SaveAsync(Team team, CancellationToken cancellationToken);
		Task<Team> GetAsync(string id, CancellationToken cancellationToken);
		Task<List<Team>> GetAllAsync(CancellationToken cancellationToken);
		Task DeleteAsync(string id, CancellationToken cancellationToken);
	}
}
