using AT.Common.Constants;
using AT.Data;
using AthrillAccount.Project.Configuration;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace AthrillAccount.Project
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
            //cors
            services.AddCors();

            services.DependencyInjectionConfig(); // custom

            // Register the Swagger generator, defining 1 or more Swagger documents
            services.AddSwaggerGen(c => c.SwaggerGenOptionsConfiguration()); // custom

            #region Detection 

            // used for client side tracking
            services.AddDetection();
            services.AddDetectionCore().AddBrowser();

            #endregion
            

            services.AddControllers(opt => opt.GlobalFiltersConfiguration()) // custom
                .AddFluentValidation(opt => opt.RegisterValidators()) // custom
                .ConfigureApiBehaviorOptions(opt => opt.CustomValidationConfig()) // custom
                .AddNewtonsoftJson(opt => opt.JsonConfig()); // custom

            #region Authentication

            services.AddAuthentication(x => x.AuthenticationOptionsConfiguration()).AddJwtBearer(x => x.JwtBearerOptionsConfiguration(Configuration)); // custom

            #endregion

            // setting up db connection using entity framework core 
            services.AddDbContext<AthrillTechDbContext>(item => item.UseSqlServer(Configuration.GetConnectionString("AthrillTechDbCon")));

            AppSettings.Features = Configuration.GetSection("ATFeatures").Get<ATFeatureAppSettings>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseStaticFiles();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            #region CORS

            app.UseCors(builder => builder.WithOrigins(CorsConfiguration.GetAllowedOrigins(env, Configuration))  // custom
                                        .AllowAnyMethod()
                                        .AllowAnyHeader()
                                        .WithExposedHeaders(CorsConfiguration.GetExposedHeaders())  // custom
            );

            #endregion

            #region Swagger

            if (Configuration.GetValue<bool>("ATAppSettings:IsShowSwaggerDocs") || env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerUIOptionsConfiguration(Configuration)); // custom
            }

            #endregion

            app.UseHttpsRedirection();

            app.UseRouting();

            app.MiddlewareConfiguration();  // custom


            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
