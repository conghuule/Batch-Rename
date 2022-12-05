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
    /// Interaction logic for RulesWindow.xaml
    /// </summary>
    public partial class RulesWindow : Window
    {
        public RulesWindow()
        {
            InitializeComponent();
        }

        public class Rule
        {
            public string Name { get; set; }   
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            List<Rule> rules = new List<Rule>();
            rules.Add(new Rule { Name = "Add counter" });
            rules.Add(new Rule { Name = "Add prefix" });
            rules.Add(new Rule { Name = "Add suffix" });
            rules.Add(new Rule { Name = "Remove all space" });
            rules.Add(new Rule { Name = "All character to lowercase" });

            rulesListView.ItemsSource = rules;
        }

        private void addRuleButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }

    }
}
