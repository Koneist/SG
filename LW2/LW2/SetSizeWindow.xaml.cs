using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace LW2
{
    /// <summary>
    /// Interaction logic for SetSizeWindow.xaml
    /// </summary>
    public partial class SetSizeWindow : Window
    {
        public SetSizeWindow()
        {
            InitializeComponent();
        }

        double _canvasWidth = 0;
        double _canvasHeight = 0;

        private void Accept_Click(object sender, RoutedEventArgs e)
        { 

            var IsFieldsCorrect = ValidateField(WidthBox, out _canvasWidth);
                IsFieldsCorrect = ValidateField(HeightBox, out _canvasHeight) && IsFieldsCorrect;
            
            if(!IsFieldsCorrect)
                return;

            this.DialogResult = true;
        }

        private bool ValidateField(TextBox textBox, out double value)
        {
            if (!double.TryParse(textBox.Text, out value))
            {
                value = 0;
                textBox.BorderBrush = Brushes.Red;
                return false;
            }

            textBox.BorderBrush = Brushes.Gray;
            return true;
        }

        public double CanvasWidth
        {
            get { return _canvasWidth; }
        }
        public double CanvasHeight
        {
            get { return _canvasHeight; }
        }

        private static readonly Regex _regex = new Regex("[^0-9,]+"); 
        private static bool IsTextAllowed(string text)
        {
            return !_regex.IsMatch(text);
        }

        private void IsNumber_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !IsTextAllowed(e.Text);
        }
    }
}
