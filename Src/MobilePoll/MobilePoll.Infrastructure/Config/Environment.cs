using System;
using System.Collections.Generic;
using System.Configuration;
using MobilePoll.Infrastructure.Bus;
using MobilePoll.Infrastructure.Ioc;

namespace MobilePoll.Infrastructure.Config
{
    public static class Configuration
    {
        private static readonly Dictionary<string, string> Settings = new Dictionary<string, string>();

        private static IServiceContainer container;

        public static void Initialize(IContainerBuilder containerBuilder)
        {
            if (container != null)
                return;

            ComponentScanner.Scan(containerBuilder);

            containerBuilder.RegisterSingleton(containerBuilder);
            containerBuilder.RegisterModule(new DefaultDependencyRegistrar());
            container = containerBuilder.BuildContainer();
        }

        public static IServiceContainer RootContainer
        {
            get
            {
                if (container == null)
                    throw new InvalidOperationException("IoC container has not been built.");

                return container;
            }
        }

        public static ILocalBus Bus
        {
            get
            {
                if (container == null)
                    throw new InvalidOperationException("IoC container has not been built.");

                return container.GetInstance<ILocalBus>();
            }
        }

        public static string GetSetting(string key)
        {
            try
            {
                return ConfigurationManager.AppSettings[key] ?? Settings[key];
            }
            catch (Exception)
            {
                throw new ConfigurationSettingNotFoundException(key);    
            }
        }

        public static void AddSetting(string key, string value)
        {
            Settings[key] = value;
        }
    }
}