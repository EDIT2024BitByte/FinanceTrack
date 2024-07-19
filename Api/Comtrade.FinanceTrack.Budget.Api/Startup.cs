using Comtrade.FinanceTrack.Mapper.Budget;
using Comtrade.FinanceTrack.Budget.Api.Initialization;
using NSwag;
using NSwag.Generation.Processors.Security;

namespace Comtrade.FinanceTrack.Budget.Api
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {

            //services.AddHttpContextAccessor();
            //services.AddCors();
            services.AddControllers();

            // Register Swagger Services.
            services.AddSwaggerDocument(settings =>
            {
                settings.Title = "Comtrade.FinanceTrack.Budget.Api";
                settings.Version = "v1";

                //settings.OperationProcessors.Add(new OperationSecurityScopeProcessor("JWT Token"));
                //settings.AddSecurity("JWT Token", Enumerable.Empty<string>(),
                //    new OpenApiSecurityScheme()
                //    {
                //        Type = OpenApiSecuritySchemeType.ApiKey,
                //        Name = "Authorization",
                //        In = OpenApiSecurityApiKeyLocation.Header,
                //        Description = "Copy this into the value field: Bearer {token}"
                //    }
                //);
            });

            // Initialize Services.
            services.Initialize(Configuration);

            // Register AutoMappers here.
            services.AddAutoMapper(typeof(Startup));
            services.AddAutoMapper(c => c.AddProfile<BudgetMappingProfile>(), typeof(Startup));
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //if (env.IsDevelopment())
            //{
            app.UseDeveloperExceptionPage();
            app.UseOpenApi();
            app.UseSwaggerUi();
            //}

            app.UseRouting();

            //app.UseAuthentication();
            //app.UseAuthorization();

            //app.UseCors(builder =>
            //    builder.WithOrigins(Configuration["CORS:Url"], "null")
            //        .AllowAnyMethod()
            //        .AllowAnyHeader()
            //        .AllowCredentials()
            //);

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
