using AddCounterRule;
using AddSuffixRule;
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
    /// Interaction logic for EditAddSuffixWindow.xaml
    /// </summary>
    public partial class EditAddSuffixWindow : Window
    {
        public string newRule { get; set; }
        public AddSuffix _oldRule = new AddSuffix();
        public EditAddSuffixWindow(string oldRule)
        {
            InitializeComponent();

            _oldRule = (AddSuffix)_oldRule.Parse(oldRule);
            DataContext = _oldRule;
        }

        private void editRuleButton_Click(object sender, RoutedEventArgs e)
        {
            string suffix = AddSuffixTextBox.Text;
            newRule = $"AddSuffix {suffix}";

            DialogResult = true;
        }
    }
}
