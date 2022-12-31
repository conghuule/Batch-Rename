using AddCounterRule;
using ChangeExtensionRule;
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
    /// Interaction logic for EditChangeExtensionWindow.xaml
    /// </summary>
    public partial class EditChangeExtensionWindow : Window
    {
        public string newRule { get; set; }
        public ChangeExtension _oldRule = new ChangeExtension();
        public EditChangeExtensionWindow(string oldRule)
        {
            InitializeComponent();

            _oldRule = (ChangeExtension)_oldRule.Parse(oldRule);
            DataContext = _oldRule;
        }

        private void editRuleButton_Click(object sender, RoutedEventArgs e)
        {
            string oldExtension = OldExtensionTextBox.Text;
            string newExtension = NewExtensionTextBox.Text;
            newRule = $"ChangeExtension {oldExtension} {newExtension}";

            DialogResult = true;
        }
    }
}
