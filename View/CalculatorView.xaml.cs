using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Monefy.View
{
    /// <summary>
    /// Логика взаимодействия для CalculatorView.xaml
    /// </summary>
    public partial class CalculatorView : UserControl
    {
        public CalculatorView()
        {
            InitializeComponent();
        }
        private void inputTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            string regexPattern = @"[^0-9.]";
            if (System.Text.RegularExpressions.Regex.IsMatch(e.Text, regexPattern))
            {
                e.Handled = true;
            }

            if (e.Text == "." && ((TextBox)sender).Text.Contains("."))
            {
                e.Handled = true;
            }
        }
    }
}
