using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Monefy.Services.Interfaces;

namespace Monefy.ViewModel
{
    class MainViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        public MainViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }
        public RelayCommand Plus
        {
            get => new(() =>
            {
                _navigationService.NavigateTo<CalculatorViewModel>();
            });
        }
    }
}
