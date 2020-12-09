using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Autofac;
using AutoMapper;
using OsCoreApplication.DataLayer.DataAccess;
using OsCoreApplication.DataLayer.EFModel;
using OsCoreApplication.DataLayer.Infrastructure;
using OsCoreApplication.Libraries.Memcached;
using OsCoreApplication.Services;

namespace OsCoreApplication.Configuration
{
    public class DataAccessModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>().InstancePerRequest();
            builder.RegisterType<EFDBEntities>().AsSelf();
        }
    }

    public class ServicesModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(Assembly.Load("OsCoreApplication.Services"))
                   .Where(s => s.Name.EndsWith("Services"))
                   .AsImplementedInterfaces()
                   .InstancePerDependency();

            builder.RegisterType<TransactionProvider>().As<ITransactionProvider>().InstancePerRequest();
        }
    }

    public class MapperModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            var assemblies = AppDomain.CurrentDomain.GetAssemblies();
            builder.RegisterAssemblyTypes(assemblies)
                .Where(s => typeof(Profile).IsAssignableFrom(s) && !s.IsAbstract && s.IsPublic)
                .As<Profile>();

            builder.Register(s => new MapperConfiguration(m =>
            {
                foreach (var profile in s.Resolve<IEnumerable<Profile>>())
                {
                    m.AddProfile(profile);
                }
            }));

            builder.Register(s => s.Resolve<MapperConfiguration>().CreateMapper()).As<IMapper>().SingleInstance();
        }
    }

    public class LibModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<CachedAccess>().As<ICached>().InstancePerRequest();
        }
    }
}
