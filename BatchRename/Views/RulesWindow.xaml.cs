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
    /// 
    public partial class AddCounterWindow
    {
        public string nameCounter { get; set; }
        public int StartFrom { get; set; }
        public int Step { get; set; }
        public int Count { get; set; }
        public char PadChar { get; set; }

    }

    public partial class AddPreFixWindow
    {
        public string namePreFix { get; set; }
        public string PreFix { get; set; }

    }

    public partial class AddSuFixWindow
    {
        public string nameSuFix { get; set; }
        public string SuFix { get; set; }

    }

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

        }

        private void addRuleButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }

        private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (AddCounter.IsSelected)
                AddCounterWindow(sender, e);
            else if(AddPrefix.IsSelected)
                AddPreFixWindow(sender, e);
            else if (AddSufix.IsSelected)
                AddSuFixWindow(sender, e);
        }

        private void AddCounterWindow(object sender, RoutedEventArgs e)
        {
            List<AddCounterWindow> component = new List<AddCounterWindow>();
            component.Add(new AddCounterWindow { nameCounter = "Start From"});
            component.Add(new AddCounterWindow { nameCounter = "Step"});
            component.Add(new AddCounterWindow { nameCounter = "Count"});
            component.Add(new AddCounterWindow { nameCounter = "Pad Char"});

            CounterList.ItemsSource = component;
        }

        private void AddPreFixWindow(object sender, RoutedEventArgs e)
        {
            List<AddPreFixWindow> component = new List<AddPreFixWindow>();
            component.Add(new AddPreFixWindow { namePreFix = "Add prefix"});
            PreFixList.ItemsSource = component;
        }
        private void AddSuFixWindow(object sender, RoutedEventArgs e)
        {
            List<AddSuFixWindow> component = new List<AddSuFixWindow>();
            component.Add(new AddSuFixWindow { nameSuFix = "Add sufix", SuFix = "" });
            SuFixList.ItemsSource = component;
        }
        
    }
}
