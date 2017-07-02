namespace Globo.Teams.WebAPI.Handlers
{
	using System.Linq;
	using Microsoft.AspNetCore.Http;
	using Microsoft.AspNetCore.Builder;
	using System.Threading.Tasks;
	using Newtonsoft.Json;
	using Microsoft.Extensions.Primitives;
	using System.Text.RegularExpressions;
	using System.Threading;
	using System;
	using Globo.Authentication;

	public class AuthorizationHanler
	{
		private static Regex bearerRegex = new Regex("^bearer (?<token>[A-Za-z0-9]+)");
		internal async static Task AuthorizationHandlerMethod(HttpContext context, Func<Task> next)
		{
			StringValues values;
			string path = context.Request.Path.Value;
			if (path.Contains("api/teams"))
			{
				using (CancellationTokenSource tokenSource = new CancellationTokenSource(TimeSpan.FromSeconds(1)))
				{
					if (context.Request.Headers.TryGetValue("Authorization", out values))
					{
						string bearer = values.First();
						Match match = bearerRegex.Match(bearer);
						if (match.Success)
						{
							string token = match.Groups["token"].Value;
							TokenData tokenData = await AuthenticationManager.Instance.GetTokenDataAsync(token, tokenSource.Token);
							if (!Authorization.AccessControl.Instance.HasAccess(tokenData.User, "Administrator"))
							{
								context.Response.StatusCode = 403;
								await context.Response.WriteAsync("Acesso negado.");
							}
						}
					}
					else
					{
						context.Response.StatusCode = 401;
						await context.Response.WriteAsync("Acesso negado");
					}
				}
			}

			await next.Invoke();
		}
	}
}
