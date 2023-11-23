using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Messaging;
using Monefy.Messages;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monefy.ViewModel
{
    class MainWindowViewModel : ViewModelBase
    {
        private readonly IMessenger _messenger;
        private ViewModelBase currentView = App.Container.GetInstance<MainViewModel>();
        public ViewModelBase CurrentView 
        { 
            get { return currentView; } 
            set { Set(ref currentView, value); }
        }
        public MainWindowViewModel(IMessenger messenger)
        {
            _messenger = messenger;
            _messenger.Register<NavigationMessage>(this, message =>
            {
                CurrentView = App.Container.GetInstance(message.ViewModelType) as ViewModelBase; ;
            });
        }
    }
}
