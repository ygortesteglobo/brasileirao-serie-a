namespace Globo.Teams.WebAPI.Controllers
{
	using System.Collections.Generic;
	using Microsoft.AspNetCore.Mvc;
	using System.Threading.Tasks;
	using System.Threading;

	[Route("api/teams")]
    public class TeamsController : Controller
    {

        [HttpGet]
        public async Task<IEnumerable<Team>> GetAllAsync(CancellationToken cancellationToken)
        {
			return await TeamManager.Instance.GetAllAsync(cancellationToken);
        }

        [HttpGet("{id}")]
        public async Task<Team> GetAsync(string id, CancellationToken cancellationToken)
        {
			return await TeamManager.Instance.GetAsync(id, cancellationToken);
		}

        [HttpPatch]
        public async Task<Team> PatchAsync([FromBody]Team team, CancellationToken cancellationToken)
        {
			return await TeamManager.Instance.SaveAsync(team, cancellationToken);
        }
		
        [HttpDelete("{id}")]
        public async Task DeleteAsync(string id, CancellationToken cancellationToken)
        {
			await TeamManager.Instance.DeleteAsync(id, cancellationToken);
        }
    }
}
