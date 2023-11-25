using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using LiveCharts;
using LiveCharts.Wpf;
using Microsoft.VisualBasic;
using Monefy.Messages;
using Monefy.Model;
using Monefy.Services.Interfaces;

namespace Monefy.ViewModel
{
    class MainViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        private readonly IMessenger _messenger;
        private readonly IDataService _dataService;
        private SpandingModel _balance;

        Button check = new();
        private PieChart chart = new();

        public PieChart Chart
        {
            get => chart;
            set
            {
                Set(ref chart, value);
            }
        }

        public SpandingModel Balance { get => _balance; set => Set(ref _balance, value); }
       
        public MainViewModel(INavigationService navigationService, IMessenger messenger, IDataService dataService)
        {
            _navigationService = navigationService;
            _messenger = messenger;
            _dataService = dataService;
            Balance = new SpandingModel();


            _messenger.Register<GenericMessage>(this, message =>
            {
                if (message.Data as Button != null)
                {
                    check = message.Data as Button;
                }
            });

            _messenger.Register<DataMessage>(this, message =>
            {
                if (message.Data as SpandingModel != null)
                {
                    var tmp = message.Data as SpandingModel;
                    if((check.Foreground as SolidColorBrush)?.Color == Colors.LimeGreen)
                    {
                        Balance.Value += tmp.Value;
                    }
                    else
                    {
                        Balance.Value -= tmp.Value;
                    }
                }
            });
        }

        public RelayCommand<Button> Plus
        {
            get => new((button) =>
            {
                _dataService.SendData(button);
                _navigationService.NavigateTo<CalculatorViewModel>();
            });
        }

        public RelayCommand<Button> Add
        {
            get => new((button) =>
            {
                _dataService.SendData(button);
                _dataService.SendData(Chart);

                _navigationService.NavigateTo<CalculatorViewModel>();
            });
        }
    }
}
