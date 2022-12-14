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
        public BindingList<Rule> rules;
        public Rule newRule;
        public RulesWindow()
        {
            InitializeComponent();
        }

        public class Rule : INotifyPropertyChanged, ICloneable
        {
            public string Name { get; set; }

            public event PropertyChangedEventHandler? PropertyChanged;

            public object Clone()
            {
                return MemberwiseClone();
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            rules = new BindingList<Rule>();
            rules.Add(new Rule { Name = "Add counter" });
            rules.Add(new Rule { Name = "Add prefix" });
            rules.Add(new Rule { Name = "Add suffix" });
            rules.Add(new Rule { Name = "Remove all space" });
            rules.Add(new Rule { Name = "All character to lowercase" });
            rulesListView.ItemsSource = rules;
        }

        private void addRuleButton_Click(object sender, RoutedEventArgs e)
        {
            int id = -1;
            id = rulesListView.SelectedIndex;
            if (id != -1)
            {
                newRule = new Rule();
                newRule.Name = rules[id].Name;
                DialogResult = true;
            }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            this.Close();
        }
    }
}