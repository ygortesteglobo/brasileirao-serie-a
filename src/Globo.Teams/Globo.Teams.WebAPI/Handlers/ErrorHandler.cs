namespace Globo.Teams.WebAPI.Handlers
{
	using Microsoft.AspNetCore.Builder;
	using System.Threading.Tasks;
	using Microsoft.AspNetCore.Http;
	using Globo.Authentication.Exceptions;
	using Newtonsoft.Json;
	using Microsoft.AspNetCore.Diagnostics;
	using System;

	public class ErrorHandler
	{
		public static void HandlerMethod(IApplicationBuilder app)
		{
			app.Run(CheckAccessAsync);
		}

		private async static Task CheckAccessAsync(HttpContext context)
		{
			context.Response.StatusCode = (int)System.Net.HttpStatusCode.InternalServerError;
			context.Response.ContentType = "text/html";
			IExceptionHandlerFeature exceptionHandler = context.Features.Get<IExceptionHandlerFeature>();
			if (exceptionHandler != null)
			{
				Exception ex = exceptionHandler.Error;
				if (ex is TokenExpiredException || ex is TokenInvalidException || ex is WrongPasswordException)
				{
					context.Response.StatusCode = 401;
					await context.Response.WriteAsync("Acesso negado.");
				}
				else if (ex is UnauthorizedException)
				{
					context.Response.StatusCode = 403;
					await context.Response.WriteAsync("Acesso negado.");
				}
				else
				{
					context.Response.StatusCode = 500;
					context.Response.ContentType = "application/json";
					string message = JsonConvert.SerializeObject(new
					{
						message = ex.Message,
						stack = ex.StackTrace
					});
					await context.Response.WriteAsync(message, System.Text.Encoding.UTF8);
				}
			}
		}
	}
}
