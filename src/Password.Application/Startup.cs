using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Password.Domain.Interfaces;
using Password.Infra.Services;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;
using Microsoft.Extensions.Configuration;

namespace Password.Application
{
    public class Startup
    {
        private readonly IConfigurationRoot m_configuration;

        public Startup(IHostingEnvironment env)
        {
            m_configuration = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional : false, reloadOnChange : true)
                .AddJsonFile($"appsettings.{env.EnvironmentName.ToLower()}.json", optional : true, reloadOnChange : true)
                .AddEnvironmentVariables()
                .Build();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddLogging(loggingBuilder =>
            {
                loggingBuilder.AddConfiguration(m_configuration.GetSection("Logging"));
            });

            services.AddCors(options =>
            {
                options.AddPolicy("Development", builder => builder
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowAnyOrigin());

                options.AddPolicy("Production", builder => builder
                    .WithOrigins("*")
                    .WithMethods("POST", "OPTIONS")
                    .WithHeaders("Content-Type"));
            });

            services
                .AddMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
                .AddJsonOptions(options =>
                {
                    options.SerializerSettings.ContractResolver = new DefaultContractResolver();
                    options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
                    options.SerializerSettings.DateTimeZoneHandling = DateTimeZoneHandling.Utc;
                });

            services.AddTransient<IPassword, PasswordValidationService>();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseCors("Development");
            }
            else
            {
                app.UseHsts();
                app.UseHttpsRedirection();
                app.UseCors("Production");
            }

            app.UseExceptionHandler("/errors/500");
            app.UseStatusCodePagesWithReExecute("/errors/{0}");
            app.UseMvc();
        }
    }
}
