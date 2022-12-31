using AddCounterRule;
using AddPrefixRule;
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
    /// Interaction logic for EditAddPrefixWindow.xaml
    /// </summary>
    public partial class EditAddPrefixWindow : Window
    {
        public string newRule { get; set; }
        public AddPrefix _oldRule = new AddPrefix();
        public EditAddPrefixWindow(string oldRule)
        {
            InitializeComponent();

            _oldRule = (AddPrefix)_oldRule.Parse(oldRule);
            DataContext = _oldRule;
        }

        private void editRuleButton_Click(object sender, RoutedEventArgs e)
        {
            string prefix = AddPrefixTextBox.Text;
            newRule = $"AddPrefix {prefix}";

            DialogResult = true;
        }
    }
}
