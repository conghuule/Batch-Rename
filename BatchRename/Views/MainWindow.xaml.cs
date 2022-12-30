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

using System.IO;
using Contract;
using System.Reflection;
using System.Collections.ObjectModel;
using System.Diagnostics.Metrics;
using BatchRename.Views;
using Microsoft.Win32;
using Microsoft.WindowsAPICodePack.Dialogs;
using static BatchRename.MainWindow;
using AddCounterRule;
using System.ComponentModel;
using ChangeExtensionRule;
using TrimRule;

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

        

        public class PickedRule : INotifyPropertyChanged
        {
            public string Number { get; set; } = "";
            public string Name { get; set; } = "";
            public string Description { get; set; } = "";

            public event PropertyChangedEventHandler? PropertyChanged;
        }


        public class PickedFile : INotifyPropertyChanged
        {
            public event PropertyChangedEventHandler? PropertyChanged;

            public string Filename { get; set; } = "";
            public string Newname { get; set; } = "";
            public string Path { get; set; } = "";
            public string Error { get; set; } = "";
            public string Number { get; set; } = "";
        }

        private List<string> _fileAddedList;
        ObservableCollection<PickedRule> pickedRules;
        ObservableCollection<PickedFile> pickedFiles;

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            _fileAddedList = new List<string>();
            pickedRules = new ObservableCollection<PickedRule>();
            pickedRulesDataGrid.ItemsSource = pickedRules;

            pickedFiles = new ObservableCollection<PickedFile>();
            pickedFilesDataGrid.ItemsSource = pickedFiles;
        }


        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            var screen = new RulesWindow();

            if (screen.ShowDialog() == true)
            {
                //RulesWindow.Rule data = (RulesWindow.Rule)screen.newRule.Clone();
                //PickedRule insertRule = new PickedRule();
                //insertRule.Name = data.Name;
                //pickedRules.Add(insertRule);
                string newRule = screen.newRule;
                //MessageBox.Show(newRule);
                pickedRules.Add(new PickedRule { Name = newRule, Description = newRule });

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
                if (id != 0)
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
                if (id != pickedRules.Count - 1)
                {
                    pickedRules.Move(id, id + 1);
                }

            }
        }

        private void AddFiles_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Multiselect = true;

            if (openFileDialog.ShowDialog() == true)
            {

                foreach (var path in openFileDialog.FileNames)
                {
                    if (!_fileAddedList.Contains(path))
                    {
                        _fileAddedList.Add(path);
                        string filename = Path.GetFileName(path);

                        PickedFile file = new PickedFile()
                        {
                            Filename = filename,
                            Path = path,
                        };

                        pickedFiles.Add(file);
                    }
                }
            }
        }

        private void AddFolder_Click(object sender, RoutedEventArgs e)
        {
            CommonOpenFileDialog dialog = new CommonOpenFileDialog();
            dialog.Multiselect = true;
            dialog.IsFolderPicker = true;
            if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
            {
                HandleImportFiles(dialog.FileNames.ToList());
            }
        }
        private void handleFolder(string path)
        {
            string[] fileEntries = Directory.GetFiles(path);
            foreach (string fileName in fileEntries)
            {
                if (!_fileAddedList.Contains(fileName))
                    _fileAddedList.Add(fileName);
            }
            // Recurse into subdirectories of this directory.
            string[] subdirectoryEntries = Directory.GetDirectories(path);
            foreach (string subdirectory in subdirectoryEntries)
                handleFolder(subdirectory);
        }
        private void HandleImportFiles(List<string> FileNames)
        {
            Dispatcher.BeginInvoke(() =>
            {
                //Save old files
                List<string> lastFileList = new List<string>(_fileAddedList);
                //New files
                List<string> arrAllFiles = new List<string>(FileNames);
                foreach (var file in arrAllFiles)
                {
                    if (!_fileAddedList.Contains(file))
                    {
                        if (File.Exists(file))
                        {
                            // This path is a file
                            if (!_fileAddedList.Contains(file))
                                _fileAddedList.Add(file);
                        }
                        else if (Directory.Exists(file))
                        {
                            // This path is a directory
                            handleFolder(file);
                        }
                    }
                }

                // To store
                foreach (var path in _fileAddedList)
                {
                    if (!lastFileList.Contains(path))
                    {
                        string filename = Path.GetFileName(path);

                        PickedFile file = new PickedFile()
                        {
                            Filename = filename,
                            Path = path,
                        };

                        pickedFiles.Add(file);
                    }
                }
            });
        }

        private void RemoveFiles_Click(object sender, RoutedEventArgs e)
        {
            while (pickedFilesDataGrid.SelectedItems.Count > 0)
            {
                int index = pickedFilesDataGrid.SelectedIndex;
                pickedFiles.RemoveAt(index);
                _fileAddedList.RemoveAt(index);
            }
        }

        private void renameFileList()
        {
            RuleFactory.Register(new Trim());
            RuleFactory.Register(new AddCounter());
            RuleFactory.Register(new ChangeExtension());

            RuleFactory factory = new RuleFactory();
            List<IRule> ruleList = new List<IRule>();
            List<string> pickedRuleList = new List<string>() { "AddCounter 1 2 3", "Trim", "ChangeExtension txt md" };
            foreach (var pickedRule in pickedRuleList)
            {
                IRule rule = factory.Parse(pickedRule);
                ruleList.Add(rule);
            }
            foreach (var file in pickedFiles)
            {
                string Filename = file.Filename;
                foreach (var rule in ruleList)
                {
                    Filename = rule.Rename(Filename);
                }
                file.Newname = Filename;
            }
        }

        private void Preview_Click(object sender, RoutedEventArgs e)
        {
            renameFileList();

        }

        private void Start_Click(object sender, RoutedEventArgs e)
        {
            renameFileList();

            foreach (var file in pickedFiles)
            {
                File.Move(file.Path, file.Path.Replace(file.Filename, file.Newname));
            }
        }

        private void Handle_DropFile(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                List<string> lastFileList = new List<string>(_fileAddedList);

                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);

                foreach (var file in files)
                {
                    if (!_fileAddedList.Contains(file))
                    {
                        if (File.Exists(file))
                        {
                            // This path is a file
                            if (!_fileAddedList.Contains(file))
                                _fileAddedList.Add(file);
                        }
                        else if (Directory.Exists(file))
                        {
                            // This path is a directory
                            handleFolder(file);
                        }
                    }
                }

                foreach (var path in _fileAddedList)
                {
                    if (!lastFileList.Contains(path))
                    {
                        string filename = Path.GetFileNameWithoutExtension(path);
                        string extension = Path.GetExtension(path);

                        PickedFile file = new PickedFile()
                        {
                            Filename = filename,
                            Path = path,
                        };

                        pickedFiles.Add(file);
                    }
                }
            }

            filesPanel.Visibility = Visibility.Visible;
            dragdropPanel.Visibility = Visibility.Hidden;
        }

        private void Handle_DragEnter(object sender, DragEventArgs e)
        {
            filesPanel.Visibility = Visibility.Collapsed;
            dragdropPanel.Visibility = Visibility.Visible;
        }

        private void Handle_DragLeave(object sender, DragEventArgs e)
        {
            filesPanel.Visibility = Visibility.Visible;
            dragdropPanel.Visibility = Visibility.Hidden;
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