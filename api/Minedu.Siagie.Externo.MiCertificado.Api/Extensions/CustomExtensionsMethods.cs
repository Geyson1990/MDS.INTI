using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.OpenApi.Models;
using Minedu.Core.Api.Infrastructure.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Minedu.Siagie.Externo.MiCertificado.Application.Contract;
using Minedu.Siagie.Externo.MiCertificado.Application.Services;
using Minedu.Siagie.Identity.Application.Contract;
using Minedu.Siagie.Identity.Application.Services;
using System.Collections;
using Minedu.Siagie.Externo.MiCertificado.Api.Utils;

namespace Minedu.Siagie.Externo.MiCertificado.Api.Extensions
{
    public static class CustomExtensionsMethods
    {
        public static IServiceCollection AddAppInsight(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddApplicationInsightsTelemetry(configuration);
            services.AddApplicationInsightsKubernetesEnricher();

            return services;
        }
        public static IServiceCollection AddCustomMVC(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddControllers(options =>
            {
                options.Filters.Add(typeof(HttpGlobalExceptionFilter));
                options.ModelBindingMessageProvider.SetAttemptedValueIsInvalidAccessor((x, y) => $"El campo '{y}' no es válido.");
                options.ModelBindingMessageProvider.SetValueMustNotBeNullAccessor((x) => $"El valor '{x}' es inválido.");
                options.ModelBindingMessageProvider.SetValueIsInvalidAccessor((x) => $"El valor '{x}' es inválido.");
                options.ModelBindingMessageProvider.SetValueMustBeANumberAccessor((x) => $"El campo {x} debe ser un número.");
            });

            services.AddMvc().ConfigureApiBehaviorOptions(options =>
            {
                options.InvalidModelStateResponseFactory = c =>
                {
                    var errorsInModelState = c.ModelState
                    .OrderBy(y=>y.Key)
                        .Where(x => x.Value.Errors.Count > 0)
                        .ToDictionary(kvp => kvp.Key, kvp => kvp.Value.Errors.Select(x => x.ErrorMessage).ToArray());

                    Dictionary<string, ArrayList> tempError = new Dictionary<string, ArrayList>();
                    string tempErrorName;
                    ArrayList tempErrorValues = new ArrayList();

                    var errorResponse = new ErrorResponse();

                    foreach (var error in errorsInModelState)
                    {
                        tempErrorName = error.Key;
                        foreach (var subError in error.Value)
                        {
                            tempErrorValues.Add(subError);
                        }

                        tempError.Add(tempErrorName, (ArrayList)tempErrorValues.Clone());

                        tempErrorValues.Clear();
                    }

                    errorResponse.Errors = tempError;
                    errorResponse.Success = false;

                    return new BadRequestObjectResult(errorResponse);
                };
            });

            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder
                    .SetIsOriginAllowed((host) => true)
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials());
            });

            return services;
        }
        public static IServiceCollection AddCustomSwagger(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Siagie Externo Mi Certificado API",
                    Version = "v1",
                    Description = "Microservice Architecture",
                    Contact = new OpenApiContact
                    {
                        Name = "OTIC @ MINEDU",
                        Email = "desarrollador52_usi@minedu.gob.pe"
                    },
                    License = new OpenApiLicense
                    {
                        Name = "Derechos Reservados"
                    },
                });
            });

            return services;
        }
        public static IServiceCollection AddCustomSecuritySwagger(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSwaggerGen(options =>
            {
                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "JWT Authorization header using the Bearer scheme."
                });
                options.AddSecurityRequirement(new OpenApiSecurityRequirement{
                {
                    new OpenApiSecurityScheme{
                        Reference = new OpenApiReference{
                            Id = "Bearer",
                            Type = ReferenceType.SecurityScheme
                        }
                    },new List<string>()
                }
                });
            });
            return services;
        }
        public static IApplicationBuilder UseCustomHealthChecks(this IApplicationBuilder app)
        {
            app.UseHealthChecks("/api/status", new HealthCheckOptions()
            {
                Predicate = r => r.Name.Contains("self")
            });
            app.UseHealthChecks("/api/check", new HealthCheckOptions()
            {
                Predicate = _ => true,
                ResponseWriter = new Func<HttpContext, HealthReport, Task>(UIResponseWriter.WriteHealthCheckUIResponse)
            });
            return app;
        }
        public static IServiceCollection AddCustomHealthChecks(this IServiceCollection services, IConfiguration configuration)
        {
            var hcBuilder = services.AddHealthChecks();

            hcBuilder.AddCheck("self", () => HealthCheckResult.Healthy());

            //fix_jgranados : add sql, rabitmq

            return services;
        }
        public static IServiceCollection AddCustomIntegrations(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddTransient<IAnioInstitucionService, AnioInstitucionService>();
            services.AddTransient<IApoderadoService, ApoderadoService>();
            services.AddTransient<IDepartamentoService, DepartamentoService>();
            services.AddTransient<IDistritoService, DistritoService>();
            services.AddTransient<IDreService, DreService>();
            services.AddTransient<IEstudianteService, EstudianteService>();
            services.AddTransient<IGradoService, GradoService>();
            services.AddTransient<IPeriodoPromocionalService, PeriodoPromocionalService>();
            services.AddTransient<IInstitucionEducativaService, InstitucionEducativaService>();
            services.AddTransient<IJsonWebTokenService, JsonWebTokenService>();
            services.AddTransient<IModalidadService, ModalidadService>();
            services.AddTransient<INivelService, NivelService>();
            services.AddTransient<IProvinciaService, ProvinciaService>();
            services.AddTransient<ITipoAreaService, TipoAreaService>();
            services.AddTransient<IUgelService, UgelService>();

            return services;
        }
    }
}
