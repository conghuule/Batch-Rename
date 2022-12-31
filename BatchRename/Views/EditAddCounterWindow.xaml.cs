using AddCounterRule;
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
using System.Windows.Shapes;

namespace BatchRename.Views
{
    /// <summary>
    /// Interaction logic for EditAddCounterWindow.xaml
    /// </summary>
    public partial class EditAddCounterWindow : Window
    {
        public string newRule { get; set; }
        public AddCounter _oldRule = new AddCounter();
        public EditAddCounterWindow(string oldRule)
        {
            InitializeComponent();

            _oldRule = (AddCounter)_oldRule.Parse(oldRule);
            DataContext= _oldRule;
        }

        private void editRuleButton_Click(object sender, RoutedEventArgs e)
        {
            string startValue = StartValueTextBox.Text;
            string step = StepTextBox.Text;
            string numberOfDigits = NumberOfDigitsTextBox.Text;
            newRule = $"AddCounter {startValue} {step} {numberOfDigits}";

            DialogResult = true;
        }
    }
}
