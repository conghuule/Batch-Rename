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
    /// Interaction logic for EditReplaceWindow.xaml
    /// </summary>
    public partial class EditReplaceWindow : Window
    {
        public string newRule { get; set; }

        public EditReplaceWindow(string oldRule)
        {
            InitializeComponent();
        }

        private void editRuleButton_Click(object sender, RoutedEventArgs e)
        {
            string findExtension = FindTextBox.Text;
            string replaceExtension = ReplaceTextBox.Text;
            newRule = $"ReplaceCharacter {findExtension} {replaceExtension}";

            DialogResult = true;
        }
    }
}
