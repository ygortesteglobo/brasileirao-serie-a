namespace Globo.Teams.WebAPI.Controllers
{
	using Globo.Authentication;
	using Microsoft.AspNetCore.Mvc;
	using System.Collections.Generic;
	using System.Threading;
	using System.Threading.Tasks;

	[Route("api/users")]
	public class UserController : Controller
	{
		[HttpGet]
		public async Task<List<User>> GetAllAsync(string email, CancellationToken cancellationToken)
		{
			return await UserManager.Instance.GetAllAsync(cancellationToken);
		}

		[HttpGet("{email}")]
		public async Task<User> GetAsync(string email, CancellationToken cancellationToken)
		{
			return await UserManager.Instance.GetAsync(email, cancellationToken);
		}

		[HttpPatch]
		public async Task SaveAsync([FromBody]User user, CancellationToken cancellationToken)
		{
			await UserManager.Instance.SaveAsync(user, cancellationToken);
		}

		[HttpPost("authenticate")]
		public async Task<string> AuthenticateAsync(string email, string password, CancellationToken cancellationToken)
		{
			return await AuthenticationManager.Instance.GetUserTokenAsync(email, password, cancellationToken);
		}
	}

	[Route("home")]
	public class HomeController : Controller
	{
		[Route("error")]
		public void Error()
		{

		}
	}
}
