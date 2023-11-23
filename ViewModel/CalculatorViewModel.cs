using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows;
using System.Windows.Media.Media3D;
using GalaSoft.MvvmLight.Command;
using System.Reflection;
using GalaSoft.MvvmLight.Messaging;
using Monefy.Services.Interfaces;

namespace Monefy.ViewModel
{
    public class CalculatorViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        private bool hasDecimalPoint = false;
        private string currentOperation = string.Empty;
        private string inputText = string.Empty;
        public string InputText
        {
            get { return inputText; }
            set { Set(ref inputText, value); }
        }

        public CalculatorViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;

        }

        public RelayCommand<string> DigitCommand
        {
            get => new(param =>
            {
                StringBuilder tmp = new();
                tmp.Append(InputText);
                tmp.Append(param);
                InputText = tmp.ToString();
            });
        }
        

        public RelayCommand<string> OperationCommand
        {
            get => new(param =>
            {
                if (!string.IsNullOrEmpty(param))
                {
                    string lastInput = InputText.TrimEnd();
                    char lastChar = lastInput[lastInput.Length - 1];

                    if (lastChar != '+' && lastChar != '-' && lastChar != '×' && lastChar != '÷')
                    {
                        StringBuilder tmp = new();
                        tmp.Append(InputText);
                        tmp.Append(" " + param + " ");
                        InputText = tmp.ToString();
                        hasDecimalPoint = false;
                    }
                }
            });
        }

        public RelayCommand DotCommand
        {
            get => new(() =>
            {
                if (!hasDecimalPoint)
                {
                    inputText += ".";
                    hasDecimalPoint = true;
                }
            });
        }

        public RelayCommand DeleteCommand
        {
            get => new(() =>
            {
                InputText = InputText.Substring(0, InputText.Length - 1);
            });
        }

        public RelayCommand Back
        {
            get => new(() =>
            {
                _navigationService.NavigateTo<MainViewModel>();
            });
        }

        public RelayCommand EqualCommand
        {
            get => new(() =>
            {
                    string[] parts = InputText.Split(' ');
                    string[] newParts;
                    double result = 0;

                    if (parts.Length % 2 == 1)
                    {
                        result = double.Parse(parts[0], CultureInfo.InvariantCulture);

                        if (parts.Contains<string>("×") || parts.Contains<string>("÷"))
                        {
                            for (int i = 1; i < parts.Length - 1; i++)
                            {
                                string operation = parts[i];
                                double operand;

                                if (double.TryParse(parts[i + 1], NumberStyles.Any, CultureInfo.InvariantCulture, out operand))
                                {
                                    switch (operation)
                                    {
                                        case "×":
                                            result *= operand;
                                            break;
                                        case "÷":
                                            if (operand != 0)
                                            {
                                                result /= operand;
                                            }
                                            else
                                            {
                                                InputText = "Error";
                                                return;
                                            }
                                            break;
                                    }
                                }
                            }
                        }

                        for (int i = 1; i < parts.Length - 1; i += 2)
                        {
                            string operation = parts[i];
                            double operand;

                            if (double.TryParse(parts[i + 1], NumberStyles.Any, CultureInfo.InvariantCulture, out operand))
                            {
                                switch (operation)
                                {
                                    case "+":
                                        result += operand;
                                        break;
                                    case "-":
                                        result -= operand;
                                        break;
                                }
                            }
                            else
                            {
                                InputText = "Error";
                                return;
                            }
                        }
                    }

                    InputText = result.ToString(CultureInfo.InvariantCulture);
                    hasDecimalPoint = false;
            });
        }
    }
}
