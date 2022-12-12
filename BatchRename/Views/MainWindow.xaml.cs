using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Diagnostics;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using System.IO;
using Contract;
using System.Reflection;
using System.Collections.ObjectModel;
using System.Diagnostics.Metrics;
using BatchRename.Views;

namespace BatchRename
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            
        }
        static void Swap<T>(ref T lhs, ref T rhs)
        {
            T temp;
            temp = lhs;
            lhs = rhs;
            rhs = temp;
        }
        public class PickedRule
        {
            public string Number { get; set; } = "";
            public string Name { get; set; } = "";
            public string Description { get; set; } = "";
        }


        public class PickedFile
        {
            public string Filename { get; set; } = "";
            public string Newname { get; set; } = "";
            public string Path { get; set; } = "";
            public string Error { get; set; } = "";
            public string Number { get; set; } = "";
        }
        ObservableCollection<PickedRule> pickedRules;
        ObservableCollection<PickedFile> pickedFiles;
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            pickedRules = new ObservableCollection<PickedRule>();
            pickedRules.Add(new PickedRule { Number = "1", Name = "Add counter", Description = "Add counter" });
            pickedRules.Add(new PickedRule { Number = "2", Name = "Add counter", Description = "Add counter" });
            pickedRules.Add(new PickedRule { Number = "3", Name = "Add counter", Description = "Add counter" });
            pickedRules.Add(new PickedRule { Number = "3", Name = "Add counter", Description = "Add counter" });
            pickedRules.Add(new PickedRule { Number = "3", Name = "Add counter", Description = "Add counter" });
            pickedRules.Add(new PickedRule { Number = "3", Name = "Add counter", Description = "Add counter" });
            pickedRules.Add(new PickedRule { Number = "3", Name = "Add counter", Description = "Add counter" });
            pickedRules.Add(new PickedRule { Number = "3", Name = "Add counter", Description = "Add counter" });
            pickedRules.Add(new PickedRule { Number = "3", Name = "Add counter", Description = "Add counter" });
            pickedRules.Add(new PickedRule { Number = "3", Name = "Add counter", Description = "Add counter" });
            pickedRules.Add(new PickedRule { Number = "3", Name = "Add counter", Description = "Add counter" });
            pickedRules.Add(new PickedRule { Number = "3", Name = "Add counter", Description = "Add counter" });
            pickedRules.Add(new PickedRule { Number = "3", Name = "Add counter", Description = "Add counter" });
            pickedRules.Add(new PickedRule { Number = "3", Name = "Add counter", Description = "Add counter" });
            pickedRules.Add(new PickedRule { Number = "3", Name = "Add counter", Description = "Add counter" });
            pickedRules.Add(new PickedRule { Number = "3", Name = "Add counter", Description = "Add counter" });
            pickedRules.Add(new PickedRule { Number = "3", Name = "Add counter", Description = "Add counter" });
            pickedRules.Add(new PickedRule { Number = "3", Name = "Add counter", Description = "Add counter" });
            pickedRules.Add(new PickedRule { Number = "3", Name = "Add counter", Description = "Add counter" });
            pickedRules.Add(new PickedRule { Number = "3", Name = "Add counter", Description = "Add counter" });
            pickedRules.Add(new PickedRule { Number = "3", Name = "Add counter", Description = "Add counter" });
            pickedRules.Add(new PickedRule { Number = "3", Name = "Add counter", Description = "Add counter" });
            pickedRules.Add(new PickedRule { Number = "3", Name = "Add counter", Description = "Add counter" });
            pickedRules.Add(new PickedRule { Number = "3", Name = "Add counter", Description = "Add counter" });
            pickedRules.Add(new PickedRule { Number = "3", Name = "Add counter", Description = "Add counter" });
            pickedRules.Add(new PickedRule { Number = "3", Name = "Add counter", Description = "Add counter" });
            pickedRules.Add(new PickedRule { Number = "3", Name = "Add counter", Description = "Add counter" });
            pickedRules.Add(new PickedRule { Number = "3", Name = "Add counter", Description = "Add counter" });
            pickedRulesDataGrid.ItemsSource = pickedRules;

            pickedFiles = new ObservableCollection<PickedFile>();
            pickedFiles.Add(new PickedFile { Number = "1", Filename = "abc.txt", Newname = "123.txt", Path = "C:\\downloads", Error = "No error" });
            pickedFiles.Add(new PickedFile { Number = "1", Filename = "abc.txt", Newname = "123.txt", Path = "C:\\downloads", Error = "No error" });
            pickedFiles.Add(new PickedFile { Number = "1", Filename = "abc.txt", Newname = "123.txt", Path = "C:\\downloads", Error = "No error" });
            pickedFiles.Add(new PickedFile { Number = "1", Filename = "abc.txt", Newname = "123.txt", Path = "C:\\downloads", Error = "No error" });
            pickedFiles.Add(new PickedFile { Number = "1", Filename = "abc.txt", Newname = "123.txt", Path = "C:\\downloads", Error = "No error" });
            pickedFiles.Add(new PickedFile { Number = "1", Filename = "abc.txt", Newname = "123.txt", Path = "C:\\downloads", Error = "No error" });
            pickedFiles.Add(new PickedFile { Number = "1", Filename = "abc.txt", Newname = "123.txt", Path = "C:\\downloads", Error = "No error" });
            pickedFiles.Add(new PickedFile { Number = "1", Filename = "abc.txt", Newname = "123.txt", Path = "C:\\downloads", Error = "No error" });
            pickedFiles.Add(new PickedFile { Number = "1", Filename = "abc.txt", Newname = "123.txt", Path = "C:\\downloads", Error = "No error" });
            pickedFiles.Add(new PickedFile { Number = "1", Filename = "abc.txt", Newname = "123.txt", Path = "C:\\downloads", Error = "No error" });
            pickedFiles.Add(new PickedFile { Number = "1", Filename = "abc.txt", Newname = "123.txt", Path = "C:\\downloads", Error = "No error" });
            pickedFiles.Add(new PickedFile { Number = "1", Filename = "abc.txt", Newname = "123.txt", Path = "C:\\downloads", Error = "No error" });
            pickedFiles.Add(new PickedFile { Number = "1", Filename = "abc.txt", Newname = "123.txt", Path = "C:\\downloads", Error = "No error" });
            pickedFiles.Add(new PickedFile { Number = "1", Filename = "abc.txt", Newname = "123.txt", Path = "C:\\downloads", Error = "No error" });
            pickedFiles.Add(new PickedFile { Number = "1", Filename = "abc.txt", Newname = "123.txt", Path = "C:\\downloads", Error = "No error" });
            pickedFiles.Add(new PickedFile { Number = "1", Filename = "abc.txt", Newname = "123.txt", Path = "C:\\downloads", Error = "No error" });
            pickedFiles.Add(new PickedFile { Number = "1", Filename = "abc.txt", Newname = "123.txt", Path = "C:\\downloads", Error = "No error" });
            pickedFiles.Add(new PickedFile { Number = "1", Filename = "abc.txt", Newname = "123.txt", Path = "C:\\downloads", Error = "No error" });
            pickedFiles.Add(new PickedFile { Number = "1", Filename = "abc.txt", Newname = "123.txt", Path = "C:\\downloads", Error = "No error" });
            pickedFiles.Add(new PickedFile { Number = "1", Filename = "abc.txt", Newname = "123.txt", Path = "C:\\downloads", Error = "No error" });
            pickedFiles.Add(new PickedFile { Number = "1", Filename = "abc.txt", Newname = "123.txt", Path = "C:\\downloads", Error = "No error" });
            pickedFiles.Add(new PickedFile { Number = "1", Filename = "abc.txt", Newname = "123.txt", Path = "C:\\downloads", Error = "No error" });
            pickedFiles.Add(new PickedFile { Number = "1", Filename = "abc.txt", Newname = "123.txt", Path = "C:\\downloads", Error = "No error" });
            pickedFiles.Add(new PickedFile { Number = "1", Filename = "abc.txt", Newname = "123.txt", Path = "C:\\downloads", Error = "No error" });
            pickedFiles.Add(new PickedFile { Number = "1", Filename = "abc.txt", Newname = "123.txt", Path = "C:\\downloads", Error = "No error" });
            pickedFiles.Add(new PickedFile { Number = "1", Filename = "abc.txt", Newname = "123.txt", Path = "C:\\downloads", Error = "No error" });
            pickedFiles.Add(new PickedFile { Number = "1", Filename = "abc.txt", Newname = "123.txt", Path = "C:\\downloads", Error = "No error" });
            pickedFiles.Add(new PickedFile { Number = "1", Filename = "abc.txt", Newname = "123.txt", Path = "C:\\downloads", Error = "No error" });
            pickedFiles.Add(new PickedFile { Number = "1", Filename = "abc.txt", Newname = "123.txt", Path = "C:\\downloads", Error = "No error" });

            pickedFilesDataGrid.ItemsSource = pickedFiles;



        }
       

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            var screen = new RulesWindow();

            if (screen.ShowDialog() == true)
            {
                RulesWindow.Rule data = (RulesWindow.Rule)screen.newRule.Clone();
                PickedRule insertRule = new PickedRule();
                insertRule.Name = data.Name;
                pickedRules.Add(insertRule);
            }
            else
            {

            }
        }

        private void RemoveButton_Clicked(object sender, RoutedEventArgs e)
        {
            var selectedItems = pickedRulesDataGrid.SelectedItems;
            if (selectedItems != null)
            {
                int id = pickedRulesDataGrid.Items.IndexOf(selectedItems[0]);
                pickedRules.RemoveAt(id);
            }
        }

        private void UpButton_Clicked(object sender, RoutedEventArgs e)
        {
            var selectedItems = pickedRulesDataGrid.SelectedItems;
            if (selectedItems != null)
            {
                int id = pickedRulesDataGrid.Items.IndexOf(selectedItems[0]);
                if(id != 0)
                {
                    pickedRules.Move(id, id - 1);
                }
                
            }
        }

        private void DownButton_Clicked(object sender, RoutedEventArgs e)
        {
            var selectedItems = pickedRulesDataGrid.SelectedItems;
            if (selectedItems != null)
            {
                int id = pickedRulesDataGrid.Items.IndexOf(selectedItems[0]);
                if(id!= pickedRules.Count - 1)
                {
                    pickedRules.Move(id, id + 1);
                }
                
            }
        }




        //public string First { get; set; } = "     abc txt google.txt";
        //public string Second { get; set; } = "123 giant.pdf";
        //public string Third { get; set; } = "   batch UltraMegaCop.txt      ";

        //private void Window_Loaded(object sender, RoutedEventArgs e)
        //{
        //    var exeFolder = AppDomain.CurrentDomain.BaseDirectory;
        //    var folderInfo = new DirectoryInfo(exeFolder);
        //    var dllFiles = folderInfo.GetFiles("*.dll");

        //    foreach (var file in dllFiles)
        //    {
        //        var assembly = Assembly.LoadFrom(file.FullName);
        //        var types = assembly.GetTypes();

        //        foreach (var type in types)
        //        {
        //            if (type.IsClass && typeof(IRule).IsAssignableFrom(type))
        //            {
        //                IRule rule = (IRule)Activator.CreateInstance(type)!;
        //                RuleFactory.Register(rule);
        //            }
        //        }
        //    }

        //    string presetPath = "Rules.txt";
        //    var rulesData = File.ReadAllLines(presetPath);
        //    var rules = new List<IRule>();

        //    foreach(var line in rulesData)
        //    {
        //        var rule = RuleFactory.Instance().Parse(line);

        //        if(rule != null)
        //        {
        //            rules.Add(rule);
        //        }
        //    }

        //    foreach (var rule in rules)
        //    {
        //        First = rule?.Rename(First)!;
        //    }
        //    foreach (var rule in rules)
        //    {
        //        Second = rule?.Rename(Second)!;
        //    }
        //    foreach (var rule in rules)
        //    {
        //        Third = rule?.Rename(Third)!;
        //    }

        //    this.DataContext = this;
        //}

    }
}
