using FluentValidation.AspNetCore;
using Greenflux.Data;
using Greenflux.Exceptions;
using Greenflux.Filters;
using Greenflux.Mappers;
using Greenflux.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System.Data;
using System.Text.Json.Serialization;

namespace Greenflux
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
            services.Configure<GzipCompressionProviderOptions>(o =>
            {
                o.Level = System.IO.Compression.CompressionLevel.Optimal;
            });
            services.AddResponseCompression(options =>
            {
                options.Providers.Add<GzipCompressionProvider>();
            });
            services
                .AddControllers(opts =>
                {
                    opts.Filters.Add<GlobalExceptionFilter>();
                    opts.Filters.Add<ResponseFilterAttribute>();
                })
                .AddJsonOptions(opts =>
                {
                    opts.JsonSerializerOptions.Converters.Add(new JsonStringEnumMemberConverter());
                });
            services.AddMvcCore()
                .AddFluentValidation(f => f.RegisterValidatorsFromAssembly(typeof(Startup).Assembly))
                .ConfigureApiBehaviorOptions(opts =>
                {
                    opts.InvalidModelStateResponseFactory = BindValidationException.Handle;
                })
                ;
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Greenflux", Version = "v1" });
            });

            services.AddResponseCaching();

            services.AddAutoMapper(typeof(GroupProfile), typeof(ChargeStationProfile), typeof(ConnectorProfile));

            //Injections
            services.AddSingleton<IDatabaseConnectionFactory>(new SQLiteConnectionFactory(Configuration.GetConnectionString("Default")));
            services.AddScoped<IDbConnection>(s => s.GetService<IDatabaseConnectionFactory>().CreateConnection());

            services.AddScoped<GroupService>();
            services.AddScoped<GroupRepository>();

            services.AddScoped<ChargeStationService>();
            services.AddScoped<ChargeStationRepository>();

            services.AddScoped<ConnectorService>();
            services.AddScoped<ConnectorRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseResponseCompression();
            app.UseResponseCaching();
            if (env.IsDevelopment())
            {
                //app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Greenflux v1"));
            }

            app.UseHsts();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}