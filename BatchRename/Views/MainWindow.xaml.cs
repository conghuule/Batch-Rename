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
using BatchRename.Models;
using System.Configuration;
using System.IO.Packaging;

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

        private List<string> _fileAddedList;
        ObservableCollection<string> pickedRuleList;
        ObservableCollection<PickedRule> pickedRules;
        ObservableCollection<PickedFile> pickedFiles;

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            _fileAddedList = new List<string>();
            pickedRules = new ObservableCollection<PickedRule>();
            pickedRulesDataGrid.ItemsSource = pickedRules;

            pickedFiles = new ObservableCollection<PickedFile>();
            pickedFilesDataGrid.ItemsSource = pickedFiles;

            var exeFolder = AppDomain.CurrentDomain.BaseDirectory;
            var folderInfo = new DirectoryInfo(exeFolder);
            var dllFiles = folderInfo.GetFiles("*.dll");

            foreach (var file in dllFiles)
            {
                var assembly = Assembly.LoadFrom(file.FullName);
                var types = assembly.GetTypes();

                foreach (var type in types)
                {
                    if (type.IsClass && typeof(IRule).IsAssignableFrom(type))
                    {
                        IRule rule = (IRule)Activator.CreateInstance(type)!;
                        RuleFactory.Register(rule);
                    }
                }
            }
            pickedRuleList = new ObservableCollection<string>();

            string? preset = ConfigurationManager.AppSettings["Preset"];
            if(preset != null && preset != "")
            {
                var rules = preset.Split(", ", StringSplitOptions.None);
                foreach (var rule in rules)
                {
                    pickedRuleList.Add(rule);

                    RuleDetail detail = new RuleDetail() { Rule = rule };
                    string ruleName = detail.GetName();
                    string ruleDescription = detail.GetDescription();
                    pickedRules.Add(new PickedRule { Name = ruleName, Description = ruleDescription });
                }
            }
        }


        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            var screen = new RulesWindow();

            if (screen.ShowDialog() == true)
            {
                string newRule = screen.newRule;
                RuleDetail detail = new RuleDetail() { Rule = newRule };

                bool isExistRule = false;
                foreach (var pickedRule in pickedRuleList)
                {
                    string pickedRuleName = pickedRule.Split(" ", StringSplitOptions.None)[0];
                    if (pickedRuleName == detail.GetName())
                    {
                        isExistRule = true;
                    }
                }

                if (isExistRule)
                {
                    MessageBox.Show("Rule is exist", "", MessageBoxButton.OK, MessageBoxImage.Error);
                } else
                {
                    string ruleName = detail.GetName();
                    string ruleDescription = detail.GetDescription();
                    pickedRules.Add(new PickedRule { Name = ruleName, Description = ruleDescription });

                    pickedRuleList.Add(newRule);
                }
            }
        }

        private void RemoveButton_Clicked(object sender, RoutedEventArgs e)
        {
            var selectedItems = pickedRulesDataGrid.SelectedItems;
            if (selectedItems != null)
            {
                int id = pickedRulesDataGrid.Items.IndexOf(selectedItems[0]);
                pickedRules.RemoveAt(id);
                pickedRuleList.RemoveAt(id);
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
                    pickedRuleList.Move(id, id - 1);
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
                    pickedRuleList.Move(id, id + 1);
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
            List<IRule> ruleList = new List<IRule>();
            foreach (var pickedRule in pickedRuleList)
            {
                IRule rule = RuleFactory.Instance().Parse(pickedRule);
                if (rule != null)
                {
                    ruleList.Add(rule);
                }
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

            MessageBox.Show("Rename filenames successfully", "", MessageBoxButton.OK, MessageBoxImage.Information);
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

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            string preset = String.Join(", ", pickedRuleList.ToArray());

            var configFile = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            var setttings = configFile.AppSettings.Settings;
            setttings["Preset"].Value = preset;
            configFile.Save(ConfigurationSaveMode.Minimal);

            MessageBox.Show("Save preset successfully", "", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}