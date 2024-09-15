using Autofac;
using Pizh.NET8.IService;
using Pizh.NET8.Repository.Base;
using Pizh.NET8.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Pizh.NET8.Extension
{
    public class AutofacModuleRegister : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            var basePath = AppContext .BaseDirectory;
            var servicesDllDile = Path.Combine(basePath, "Pizh.NET8.Service.dll");
            var repositoryDllDile = Path.Combine(basePath, "Pizh.NET8.Repository.dll");

            // 注册仓储
            builder.RegisterGeneric(typeof(BaseRepo<>)).As(typeof(IBaseRepo<>)).InstancePerDependency();
            // 注册服务
            builder.RegisterGeneric(typeof(BaseService<,>)).As(typeof(IBseService<,>)).InstancePerDependency();

            // 获取 Service.dll 程序集服务，并注册
            var assemblyServices = Assembly.LoadFrom(servicesDllDile);
            builder.RegisterAssemblyTypes(assemblyServices)
                .AsImplementedInterfaces()
                .InstancePerDependency()
                .PropertiesAutowired();

            // 获取 Repository.dll 程序集服务，并注册
            var assemblyRepository = Assembly.LoadFrom(repositoryDllDile);
            builder.RegisterAssemblyTypes(assemblyRepository)
                .AsImplementedInterfaces()
                .InstancePerDependency()
                .PropertiesAutowired();
        }
    }
}
