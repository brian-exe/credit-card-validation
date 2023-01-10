using CreditCardValidation.Abstractions.Repositories;
using CreditCardValidation.Abstractions.Services;
using CreditCardValidation.Logging.Logger;
using CreditCardValidation.API.LoggerCustomizations;
using CreditCardValidation.API.Middlewares;
using CreditCardValidation.Logging;
using CreditCardValidation.Repositories;
using CreditCardValidation.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Serilog;
using Serilog.Core;
using Serilog.Debugging;
using Serilog.Enrichers.Sensitive;
using Serilog.Sinks.SystemConsole.Themes;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace CreditCardValidation.API
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
            AddLogging(services);

            services.AddTransient<ICreditCardValidationService, CreditCardValidationService>();
            services.AddTransient<ICardTypeRepository, CardTypeRepository>();

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "CreditCardValidation.API", Version = "v1" });
            });
        }

        private void AddLogging(IServiceCollection services)
        {
            var serilog = new LoggerConfiguration()
                .MinimumLevel.Information()
                .Enrich.FromLogContext()
                //.Enrich.When((logEvent,condition) => { logEvent})
                //.Enrich.WithSensitiveDataMasking(options =>
                .Enrich.With(new CustomSensitiveDataEnricher(options =>
                {
                    options.MaskingOperators = new List<IMaskingOperator>()
                    {
                        new CustomCCMaskingOperator(fullMask: false),
                        new IbanMaskingOperator(),
                        new EmailAddressMaskingOperator(),
                    };
                    options.MaskValue = "*";
                    options.MaskProperties.Add("CVV");

                }))
                .WriteTo.Console(theme: AnsiConsoleTheme.Code)
                .CreateLogger();

            ILoggerFactory loggerFactory = new LoggerFactory().AddSerilog(serilog);
            services.AddSingleton(loggerFactory);
            services.AddSingleton<ILoggerProxyFactory, LoggerProxyFactory>();
        }
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "CreditCardValidation.API v1"));
            }

            app.UseHttpsRedirection();
            app.UseMiddleware<ExceptionHandlingMiddleware>();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
