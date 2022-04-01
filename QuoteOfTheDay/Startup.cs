using GraphQL.Server;
using GraphQL.Server.Ui.Playground;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using QuoteOfTheDay.Data.Context;
using QuoteOfTheDay.Data.Repositories;
using QuoteOfTheDay.Data.Repositories.Interfaces;
using QuoteOfTheDay.GraphQL;

namespace QuoteOfTheDay
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IWebHostEnvironment environment)
        {
            Configuration = configuration;
            Environment = environment;
        }


        public IConfiguration Configuration { get; }
        public IWebHostEnvironment Environment { get; }


        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<AppDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            services.AddScoped<IQuoteRepository, QuoteRepository>();

            services.AddDefer();
            services.AddHttpScope();

            services
                .AddSingleton<QuoteOfTheDaySchema>()
                .AddSingleton<QuoteQuery>()
                .AddGraphQL(opt => opt.EnableMetrics = false)
                .AddSystemTextJson()
                .AddErrorInfoProvider(opt => opt.ExposeExceptionStackTrace = Environment.IsDevelopment())
                .AddGraphTypes(typeof(QuoteOfTheDaySchema));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseGraphQL<QuoteOfTheDaySchema>();
            app.UseGraphQLPlayground(new GraphQLPlaygroundOptions());
        }
    }
}
