using Autofac;
using Minedu.Siagie.Externo.MiCertificado.Data.Queries;
using Minedu.Siagie.Externo.MiCertificado.Domain.Queries;

namespace Minedu.Siagie.Externo.MiCertificado.Api.Infrastructure.AutofacModules
{
    public class ApplicationModule : Autofac.Module
    {
        public string QueriesConnectionString { get; }
        public ApplicationModule(string qconstr)
        {
            QueriesConnectionString = qconstr;

        }
        protected override void Load(ContainerBuilder builder)
        {
            #region Queries Register
            builder.Register(c => new AnioInstitucionUoW(QueriesConnectionString))
                .As<IAnioInstitucionQuery>()
                .InstancePerLifetimeScope();
            builder.Register(c => new ApoderadoUoW(QueriesConnectionString))
                .As<IApoderadoQuery>()
                .InstancePerLifetimeScope();
            builder.Register(c => new DepartamentoUoW(QueriesConnectionString))
                .As<IDepartamentoQuery>()
                .InstancePerLifetimeScope();
            builder.Register(c => new DistritoUoW(QueriesConnectionString))
                .As<IDistritoQuery>()
                .InstancePerLifetimeScope();
            builder.Register(c => new DreUoW(QueriesConnectionString))
                .As<IDreQuery>()
                .InstancePerLifetimeScope();
            builder.Register(c => new EstudianteUoW(QueriesConnectionString))
                .As<IEstudianteQuery>()
                .InstancePerLifetimeScope();
            builder.Register(c => new GradoUoW(QueriesConnectionString))
                .As<IGradoQuery>()
                .InstancePerLifetimeScope();
            builder.Register(c => new PeriodoPromocionalUoW(QueriesConnectionString))
                .As<IPeriodoPromocionalQuery>()
                .InstancePerLifetimeScope();
            builder.Register(c => new InstitucionEducativaUoW(QueriesConnectionString))
                .As<IInstitucionEducativaQuery>()
                .InstancePerLifetimeScope();
            builder.Register(c => new ModalidadUoW(QueriesConnectionString))
                .As<IModalidadQuery>()
                .InstancePerLifetimeScope();
            builder.Register(c => new NivelUoW(QueriesConnectionString))
                .As<INivelQuery>()
                .InstancePerLifetimeScope();
            builder.Register(c => new ProvinciaUoW(QueriesConnectionString))
                .As<IProvinciaQuery>()
                .InstancePerLifetimeScope();
            builder.Register(c => new TipoAreaUoW(QueriesConnectionString))
                .As<ITipoAreaQuery>()
                .InstancePerLifetimeScope();
            builder.Register(c => new TokenUoW(QueriesConnectionString))
                .As<ITokenQuery>()
                .InstancePerLifetimeScope();
            builder.Register(c => new UgelUoW(QueriesConnectionString))
                .As<IUgelQuery>()
                .InstancePerLifetimeScope();
            #endregion

            #region Application

            #endregion
            #region Repository

            #endregion
        }
    }
}
