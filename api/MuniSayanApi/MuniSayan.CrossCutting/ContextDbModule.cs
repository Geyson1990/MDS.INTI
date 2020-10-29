using Autofac;
using Microsoft.Extensions.Configuration;
using Minedu.Comun.Data;
using MuniSayan.DataAccess.Context;
using MuniSayan.DataAccess.Contracts.UnitOfWork;
using MuniSayan.DataAccess.UnitOfWork;
using System;
using System.Reflection;

namespace MuniSayan.CrossCutting
{
    public class ContextDbModule : Autofac.Module
    {

        public static IConfiguration Configuration;

        protected override void Load(ContainerBuilder builder)
        {
            string connectionString = Configuration.GetSection("ConnectionStrings:MiCertificadoDBContext").Value;

            //Context           
            builder.RegisterType<CertificadoDbContext>().Named<IDbContext>("context").WithParameter("connstr", connectionString).InstancePerLifetimeScope();
            //Resolver UnitOfWork
            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>().WithParameter((c, p) => true, (c, p) => p.ResolveNamed<IDbContext>("context"));

            //-> Aplicacion
            builder.RegisterAssemblyTypes(Assembly.Load(new AssemblyName("MuniSayan.Application")))
                .Where(t => t.Name.EndsWith("Service", StringComparison.Ordinal) && t.GetTypeInfo().IsClass)
                .AsImplementedInterfaces();

            builder.RegisterAssemblyTypes(Assembly.Load(new AssemblyName("MuniSayan.Application")))
                .Where(t => t.Name.EndsWith("Config", StringComparison.Ordinal) && t.GetTypeInfo().IsClass)
                .AsImplementedInterfaces();

            /*builder.RegisterAssemblyTypes(Assembly.Load(new AssemblyName("MuniSayan.Application")))
                .Where(t => t.Name.EndsWith("Caller", StringComparison.Ordinal) && t.GetTypeInfo().IsClass)
                .AsImplementedInterfaces();*/


            builder.RegisterAssemblyTypes(Assembly.Load(new AssemblyName("MuniSayan.Application")))
                .Where(t => t.Name.EndsWith("Security", StringComparison.Ordinal) && t.GetTypeInfo().IsClass)
                .AsImplementedInterfaces();
        }
    }
}
