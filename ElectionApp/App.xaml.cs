using DAL.RepositoryWrapper;
using ElectionApp.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Windows;
using DAL.Data;
using DAL.Repositories.Interfaces;
using DAL.Repositories;
using AutoMapper;
using System.Reflection;
using System.Linq;
using ElectionApp.WindowManagers;

namespace ElectionApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private static IServiceProvider m_serviceProvider;

        public static IServiceProvider ServiceProvider
        {
            get 
            {
                if (m_serviceProvider == null)
                    m_serviceProvider = Init().BuildServiceProvider();

                return m_serviceProvider;
            }
        }

        private static IServiceCollection Init()
        { 
            var services = new ServiceCollection();

            #region Do Initial Setup Here
            services.AddSingleton<IDataProvider, JsonDataProvider>();
            services.AddSingleton<IVoterRepository, VoterRepository>();
            services.AddSingleton<IRepositoryWrapper, RepositoryWrapper>();

            //services.AddSingleton<IWindowManager, WindowManager>();

            services.AddSingleton<MainWindow>();
            services.AddSingleton<MainWindowViewModel>();
            
            //Mapper configuration
            var mapperConfig = new MapperConfiguration(mc =>
            {
                var assembly = Assembly.GetExecutingAssembly();

                var profiles = assembly.DefinedTypes.Where(t => t.BaseType != null && t.BaseType.Name.Equals(nameof(Profile))).Select(t => t); 
                
                foreach (var p in profiles)
                {
                    mc.AddProfile((Profile)Activator.CreateInstance(p));
                }
            });

            IMapper mapper = mapperConfig.CreateMapper();

            services.AddSingleton(mapper);
            #endregion

            return services;
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e); 

            var mainWindow = ServiceProvider.GetRequiredService<MainWindow>();

            var mainWindowViewModel = ServiceProvider.GetRequiredService<MainWindowViewModel>();

            mainWindow.DataContext = mainWindowViewModel;

            mainWindowViewModel.Dispatcher = mainWindow.Dispatcher;

            mainWindow.Show();
        }

        protected override void OnExit(ExitEventArgs e)
        {
            IRepositoryWrapper repositoryWrapper = ServiceProvider.GetRequiredService<IRepositoryWrapper>();

            repositoryWrapper.SaveData();

            base.OnExit(e);
        }
    }
}
