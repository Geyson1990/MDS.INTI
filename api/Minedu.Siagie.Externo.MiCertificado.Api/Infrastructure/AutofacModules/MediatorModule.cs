using Autofac;
using MediatR;
using Microsoft.Extensions.Configuration;
using Minedu.Siagie.Externo.MiCertificado.Api.Behaviors;
using Minedu.Siagie.Externo.MiCertificado.Domain.Commands;
using System.Reflection;

namespace Minedu.Siagie.Externo.MiCertificado.Api.Infrastructure.AutofacModules
{
    public class MediatorModule : Autofac.Module
    {
        public static IConfiguration _configuration;
        public MediatorModule(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(typeof(IMediator).GetTypeInfo().Assembly)
                .AsImplementedInterfaces();

            builder.RegisterAssemblyTypes(typeof(CreateTokenCommand).GetTypeInfo().Assembly)
                .AsClosedTypesOf(typeof(IRequestHandler<,>));

            builder.Register<ServiceFactory>(context =>
            {
                var componentContext = context.Resolve<IComponentContext>();
                return t => { object o; return componentContext.TryResolve(t, out o) ? o : null; };
            });

            builder.RegisterGeneric(typeof(LoggingBehavior<,>)).As(typeof(IPipelineBehavior<,>));
            builder.RegisterGeneric(typeof(ValidatorBehavior<,>)).As(typeof(IPipelineBehavior<,>));
        }
    }
}