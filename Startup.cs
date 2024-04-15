
using API.Filter;
using API.Interfaces.Customer;
using API.Interfaces.Logs;
using API.Interfaces.Post;
using API.Logic.Helpers;
using API.Repositories.Customer;
using API.Repositories.Logs;
using API.Repositories.Post;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System.Reflection;
using System.Text;

namespace API
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
            services.AddCors();
            services.AddControllers();
            services.AddEndpointsApiExplorer();

            var appSettingsSection = Configuration.GetSection("AppSettings");
            services.Configure<AppSettings>(appSettingsSection);
            var appSettings = appSettingsSection.Get<AppSettings>();

            services.AddSwaggerGen(c =>
            {
                c.OperationFilter<AcceptedLanguageHeader>();
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Api Juju", Version = "v1" });
                var XmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, XmlFile);
                c.IncludeXmlComments(xmlPath);

            });

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_3_0);

            services.AddTransient<ILogs, LogsRepository>();
            services.AddTransient<ICustomer, CustomerRepository>();
            services.AddTransient<IPost, PostRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseDefaultFiles();
            app.UsePathBase("/");
            app.UseStaticFiles();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Api Juju v1");
                c.RoutePrefix = string.Empty;
            });

            app.UseMiddleware<ExceptionMiddleware>();
            app.UseCors(options =>
            {
                options.WithOrigins("*");
                options.AllowAnyMethod();
                options.AllowAnyHeader();
            });
            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();
            app.UseAuthentication();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

    }
}
