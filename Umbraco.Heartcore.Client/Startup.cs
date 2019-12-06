using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Umbraco.Heartcore.Client.Core;
using Umbraco.Heartcore.Client.Features.Shared;

namespace Umbraco.Heartcore.Client
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			services
				.AddControllers()
				.AddNewtonsoftJson();

			var umbracoConfig = Configuration.GetSection("umbraco");
			var projectAlias = umbracoConfig.GetValue<string>("projectAlias");
			var apiKey = umbracoConfig.GetValue<string>("apiKey");

			services.AddUmbracoHeadlessContentDelivery(projectAlias, apiKey);
			services.AddSingleton<IUmbracoContextHelper, UmbracoContextHelper>();

		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public static void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if(env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}

			app.UseHttpsRedirection();

			app.UseRouting();

			app.UseAuthorization();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllers();
			});
		}
	}
}
