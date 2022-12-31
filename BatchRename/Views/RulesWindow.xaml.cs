using Contract;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
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
using SaveWindowPosition;

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
            var userPrefs = new UserPreferences();

            this.Height = userPrefs.WindowHeight;
            this.Width = userPrefs.WindowWidth;
            this.Top = userPrefs.WindowTop;
            this.Left = userPrefs.WindowLeft;
            this.WindowState = userPrefs.WindowState;
        }
        protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
        {
            var userPrefs = new UserPreferences();

            userPrefs.WindowHeight = this.Height;
            userPrefs.WindowWidth = this.Width;
            userPrefs.WindowTop = this.Top;
            userPrefs.WindowLeft = this.Left;
            userPrefs.WindowState = this.WindowState;

            userPrefs.Save();
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
