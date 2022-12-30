using Contract;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    /// Interaction logic for RulesWindow.xaml
    /// </summary>
    public partial class RulesWindow : Window
    {
        public string newRule { get; set; }

        public RulesWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void addRuleButton_Click(object sender, RoutedEventArgs e)
        {
            if (AddCounterTabItem.IsSelected)
            {
                string startFrom = StartValueTextBox.Text;
                string step = StepTextBox.Text;
                string numberOfDigits = NumberOfDigitsTextBox.Text;

                newRule = "AddCounter "+  startFrom + " " + step + " " + numberOfDigits;
            }
            else if (AddPrefixTabItem.IsSelected)
            {
                string prefix = AddPrefixTextBox.Text;

                newRule = "AddPrefix " + prefix;
            }
            else if (AddSuffixTabItem.IsSelected)
            {
                string suffix = AddSuffixTextBox.Text;

                newRule = "AddSuffix " + suffix;
            }
            else if (ChangeExtensionTabItem.IsSelected) {
                string oldExtension = OldExtensionTextBox.Text;
                string newExtension = NewExtensionTextBox.Text;

                newRule = "ChangeExtension " + oldExtension + " " + newExtension;
            }
            else if (ReplaceTabItem.IsSelected)
            {
                string findCharacter = FindCharacterTextBox.Text;
                string replaceCharacter = ReplaceCharacterTextBox.Text;

                newRule = "ReplaceCharacter " + findCharacter + " " + replaceCharacter;
            }
            else if (ToPascalCaseTabItem.IsSelected)
            {
                newRule = "PascalCase";
            }
            else if (ToLowerCaseTabItem.IsSelected)
            {
                newRule = "LowerCaseNoSpace";
            }
            else if (RemoveAllSpaceTabItem.IsSelected)
            {
                newRule = "Trim";
            }

            DialogResult = true;
        }
    }
}
