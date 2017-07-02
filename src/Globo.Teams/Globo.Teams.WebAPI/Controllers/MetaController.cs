namespace Globo.Teams.WebAPI.Controllers
{
	using Microsoft.AspNetCore.Mvc;

	[Route("meta")]
	public class MetaController : Controller
    {
		[HttpGet("healthcheck")]
		public string HealthCheck()
        {
            return "OK";
        }
    }
}