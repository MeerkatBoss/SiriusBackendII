using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SiriusBackendII.GraphQL.Mutations;
using SiriusBackendII.GraphQL.Queries;
using SiriusBackendII.Models;
using SiriusBackendII.Services;

namespace SiriusBackendII
{
	public sealed class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddControllers();
			services.AddDbContext<AppDbContext>(options =>
			{
				options.UseSqlServer(Configuration.GetConnectionString("DatabaseConnection"));
			});
			services.AddScoped<SellerService>();
			services.AddScoped<BouquetService>();
			services.AddScoped<CustomerService>();
			services.AddScoped<PurchaseService>();
			services
				.AddGraphQLServer()
				.AddSorting()
				.AddFiltering()

				.AddQueryType<Query>()
				.AddTypeExtension<SellerQuery>()
				.AddTypeExtension<BouquetQuery>()
				.AddTypeExtension<CustomerQuery>()
				.AddTypeExtension<PurchaseQuery>()

				.AddMutationType<Mutation>()
				.AddTypeExtension<PurchaseMutation>();
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}

			app.UseHttpsRedirection();

			app.UseRouting();

			app.UseAuthorization();

			app.UseEndpoints(endpoints =>
			{ 
				endpoints.MapGraphQL();
			});
		}
	}
}