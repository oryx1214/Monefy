using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Messaging;
using Monefy.Services.Classes;
using Monefy.Services.Interfaces;
using Monefy.View;
using Monefy.ViewModel;
using SimpleInjector;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Monefy
{
    public partial class App : Application
    {
        public static Container Container { get; set; }

        public void Register()
        {
            Container = new Container();


            Container.RegisterSingleton<INavigationService, NavigationService>();
            Container.RegisterSingleton<IMessenger, Messenger>();
            Container.RegisterSingleton<IDataService, DataService>();
            Container.RegisterSingleton<ISerializationService, SerializationService>();

            Container.RegisterSingleton<MainWindowViewModel>();
            Container.RegisterSingleton<MainViewModel>();
            Container.RegisterSingleton<CalculatorViewModel>();

            Container.Verify();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            Register();

            var window = new MainWindow();

            window.DataContext = Container.GetInstance<MainWindowViewModel>();

            window.ShowDialog();
        }
    }
}