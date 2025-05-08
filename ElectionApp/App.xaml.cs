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
using BL.ReportGenerators;
using ElectionApp.Views;

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
            services.AddSingleton<IReportGenerator, ReportGenerator>();

            services.AddSingleton<IWindowManager, WindowManager>();

            
            services.AddSingleton<MainWindowViewModel>();
            services.AddScoped<ReporterWindowViewModel>();

            services.AddSingleton(c =>
            {
                var vm = c.GetRequiredService<MainWindowViewModel>();
                var mainwindow = new MainWindow();

                mainwindow.DataContext = vm;
                vm.Dispatcher = mainwindow.Dispatcher;

                return mainwindow;
            });

            services.AddTransient(c =>
            {
                var scope = c.CreateScope();

                var repWindow = new ReporterWindow();
                var vm = scope.ServiceProvider.GetRequiredService<ReporterWindowViewModel>();
                repWindow.DataContext = vm;
                vm.Dispatcher = repWindow.Dispatcher;
                repWindow.Closed += (object sender, EventArgs e) => 
                {
                    scope.Dispose(); 
                };

                return repWindow;
            });

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

            var windowManager = ServiceProvider.GetRequiredService<IWindowManager>();

            windowManager.OpenWindow(typeof(MainWindow));
        }

        protected override void OnExit(ExitEventArgs e)
        {
            IRepositoryWrapper repositoryWrapper = ServiceProvider.GetRequiredService<IRepositoryWrapper>();

            repositoryWrapper.SaveData();

            base.OnExit(e);
        }
    }
}
