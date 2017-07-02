namespace Globo.Teams.WebAPI
{
	using Microsoft.AspNetCore.Http;
	using Microsoft.AspNetCore.Builder;
	using Microsoft.AspNetCore.Hosting;
	using Microsoft.Extensions.Configuration;
	using Microsoft.Extensions.DependencyInjection;
	using Microsoft.Extensions.Logging;
	using Globo.Authentication;
	using Globo.Teams.WebAPI.Handlers;

	public class Startup
	{
		public Startup(IHostingEnvironment env)
		{
			var builder = new ConfigurationBuilder()
				.SetBasePath(env.ContentRootPath)
				.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
				.AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
				.AddEnvironmentVariables();
			Configuration = builder.Build();

			TeamManager.Instance = new TeamManager(new DynamoDb.DynamoDbPersistence());
			UserManager.Instance = new UserManager(new Authentication.Storage.LocalRepository());
		}

		public IConfigurationRoot Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			// Add framework services.
			services.AddMvc();
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
		{
			app.UseExceptionHandler(ErrorHandler.HandlerMethod);
			app.Use(AuthorizationHanler.AuthorizationHandlerMethod);
			app.Use(async (context, next) =>
			{
				// Do work that doesn't write to the Response.
				await next.Invoke();
				// Do logging or other work that doesn't write to the Response.
			});

			loggerFactory.AddConsole(Configuration.GetSection("Logging"));
			loggerFactory.AddDebug();

			app.UseMvc();
		}
	}
}
