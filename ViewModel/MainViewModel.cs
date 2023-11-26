using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.RightsManagement;
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
        private readonly ISerializationService _serializationService;
        private SpandingModel _balance;

        private string _bg = "#7ac795";
        public string Background
        {
            get => _bg;
            set => Set(ref _bg, value);
        }

        private string _brush = "#5aa377";

        public string Brush
        {
            get => _brush;
            set => Set(ref _brush, value);
        }



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
        public List<SpandingModel> Spendings { get; set; } 

        public MainViewModel(INavigationService navigationService, IMessenger messenger, IDataService dataService, ISerializationService serializationService)
        {
            _navigationService = navigationService;
            _messenger = messenger;
            _dataService = dataService;
            _serializationService = serializationService;
            Balance = new SpandingModel();
            Spendings = _serializationService.Deserialize() ?? new();

            foreach (var item in Spendings)
            {
                Chart.Series.Add(new PieSeries()
                {
                    Fill = item.Color,
                    Values = new ChartValues<double> { item.Value}
                });
            }

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
                    Spendings.Add(tmp);
                    if ((check.Foreground as SolidColorBrush)?.Color == Colors.LimeGreen)
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

        public RelayCommand Serialize
        {
            get => new(() =>
            {
                _serializationService.Serialize(Spendings);
            });
        }

        public RelayCommand<Button> DarkMode    
        {
            get => new((btn) =>
            {
                if (btn.Name == "DarkMode")
                {
                    Background = (Background == "#7ac795") ? "fc8181" : "#7ac795";
                    Brush = (Brush == "#5aa377") ? "#a24445" : "#5aa377";
                }
            });
        }
    }
}
