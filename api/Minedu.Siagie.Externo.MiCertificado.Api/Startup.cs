using System;
using System.Linq;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Minedu.Core.EventBus.Util;
using Minedu.Siagie.Externo.MiCertificado.Map;
using Minedu.Siagie.Externo.MiCertificado.Api.Extensions;
using Minedu.Siagie.Externo.MiCertificado.Api.Infrastructure.AutofacModules;
using Minedu.Siagie.Externo.MiCertificado.Domain.Model;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Minedu.Siagie.Externo.MiCertificado.Api.Middleware;
using Minedu.Core.General.Communication;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Http;

namespace Minedu.Siagie.Externo.MiCertificado.Api
{
    public class Startup
    {
        public TokenConfigurations TokenConfigurations { get; }
        public IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            TokenConfigurations = Configuration.GetSection(nameof(TokenConfigurations)).Get<TokenConfigurations>();
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.Configure<TokenConfigurations>(Configuration.GetSection(nameof(TokenConfigurations)));

            var valuesSection = Configuration
                    .GetSection("Cors:AllowSpecificOrigins")
                    .GetChildren()
                    .ToList()
                    .Select(x => (x.GetValue<string>("origin"))).ToList();
            services.AddCors(options =>
            {
                options.AddPolicy("_AllowSpecificOrigins",
                    builder => builder.WithOrigins(valuesSection.ToArray())
                                        .AllowAnyHeader()
                                        .AllowAnyMethod()
                                        );
                options.AddPolicy("_AllowAllOrigins",
                    builder => builder.AllowAnyOrigin()
                                        .AllowAnyHeader()
                                        .AllowAnyMethod());
            });

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    var secretkey = Encoding.UTF8.GetBytes(TokenConfigurations.SecretKey);
                    var encryptionkey = Encoding.UTF8.GetBytes(TokenConfigurations.Encryptkey);

                    var validationParameters = new TokenValidationParameters
                    {
                        ClockSkew = TimeSpan.Zero, // default: 5 min
                        RequireSignedTokens = true,

                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(secretkey),

                        RequireExpirationTime = true,
                        ValidateLifetime = true,

                        ValidateAudience = true, //default : false
                        ValidAudience = TokenConfigurations.Audience,

                        ValidateIssuer = true, //default : false
                        ValidIssuer = TokenConfigurations.Issuer
                    };

                    options.RequireHttpsMetadata = false;
                    options.SaveToken = true;
                    options.TokenValidationParameters = validationParameters;

                    options.Events = new JwtBearerEvents
                    {
                        OnChallenge = async context =>
                        {
                            // Call this to skip the default logic and avoid using the default response
                            context.HandleResponse();

                            // Write to the response in any way you wish
                            context.Response.StatusCode = 401;
                            context.Response.ContentType = "application/json";
                            context.Response.Headers.Append("WWW-Authenticate", "Bearer error=\"invalid_token\"");
                            
                            string respuesta = "Usted no está autorizado (token inválido).";
                            var response = new StatusResponse<string>();
                            response.Success = false;
                            response.Data = null;

                            response.Validations.Add(new MessageStatusResponse("Usted no está autorizado (token inválido).", "00"));
                            JObject jObject = new JObject();
                            jObject["_param"] = HttpMethodEncryptationSecurity.TryEncrypt(JsonConvert.SerializeObject(response));
                            respuesta = jObject.ToString();

                            await context.Response.WriteAsync(respuesta, Encoding.UTF8);
                        }
                    };
                });

            services.AddAppInsight(Configuration)
                .AddCustomMVC(Configuration)
                .AddIntegrationServices(Configuration)
                .AddEventBus(Configuration)
                .AddCustomIntegrations(Configuration);

            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new AnioInstitucionMap());
                mc.AddProfile(new ApoderadoMap());
                mc.AddProfile(new DepartamentoMap());
                mc.AddProfile(new DistritoMap());
                mc.AddProfile(new DreMap());
                mc.AddProfile(new EstudianteMap());
                mc.AddProfile(new GradoNivelMap());
                mc.AddProfile(new GradoSeccionMap());
                mc.AddProfile(new PeriodoPromocionalMap());
                mc.AddProfile(new AlumnoMap());
                mc.AddProfile(new ColegioMap());
                mc.AddProfile(new InstitucionEducativaMap());
                mc.AddProfile(new ModalidadMap());
                mc.AddProfile(new NivelMap());
                mc.AddProfile(new ProvinciaMap());
                mc.AddProfile(new TipoAreaMap());
                mc.AddProfile(new UgelMap());
            });
            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);

            var container = new ContainerBuilder();
            container.Populate(services);
            container.RegisterModule(new MediatorModule(Configuration));
            container.RegisterModule(new ApplicationModule(Configuration.GetSection("ConnectionStrings:SiagieQuery").Value));

            return new AutofacServiceProvider(container.Build());
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            var pathBase = Configuration["PATH_BASE"];
            if (!string.IsNullOrEmpty(pathBase))
            {
                loggerFactory.CreateLogger<Startup>().LogDebug("Using PATH BASE '{pathBase}'", pathBase);
                app.UsePathBase(pathBase);
            }
            app.UseRouting();
            ConfigureAuth(app);
            app.UseCors("_AllowSpecificOrigins");

            //if (env.IsProduction())
            //{
              app.UseMiddleware<HttpMethodSecurityMiddleware>();
            //}

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
        protected virtual void ConfigureAuth(IApplicationBuilder app)
        {
            app.UseAuthentication();
            app.UseAuthorization();
        }

    }
}