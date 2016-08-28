namespace CryptMaster
{
    using System.Linq;
    using System.Reflection;
    using Autofac;
    using CryptMaster.Services;

    /// <summary>
    /// Autofac module which loads application dependencies into DI container
    /// </summary>
    public class DefaultModule : Autofac.Module
    {
        /// <summary>
        /// Loads all application related dependencies into DI container
        /// This code is called at startup by Autofac
        /// </summary>
        /// <param name="builder">Autofac builder</param>
        protected override void Load(ContainerBuilder builder)
        {
            var currentAssembly = typeof(DefaultModule).GetTypeInfo().Assembly;
            builder.RegisterAssemblyTypes(currentAssembly)
               .Where(t => typeof(ICryptService).IsAssignableFrom(t))
               .As<ICryptService>()
               .SingleInstance();
        }
    }
}
