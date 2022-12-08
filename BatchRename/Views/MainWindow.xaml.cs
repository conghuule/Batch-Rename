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
using Microsoft.WindowsAPICodePack.Dialogs;
using System.IO;
using Contract;
using System.Reflection;
using System.Collections.ObjectModel;
using System.Diagnostics.Metrics;
using BatchRename.Views;
using Microsoft.Win32;
using System.Collections;
using System.Xml.Linq;
using static BatchRename.MainWindow;
using static Microsoft.WindowsAPICodePack.Shell.PropertySystem.SystemProperties.System;
using TrimRule;
using AddCounterRule;
using ChangeExtensionRule;
using System.ComponentModel;

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

        public class PickedRule
        {
            public string Name { get; set; } = "";
            public string Description { get; set; } = "";
        }


        public class PickedFile
        {
            public string Filename { get; set; } = "";
            public string Newname { get; set; } = "";
            public string Path { get; set; } = "";
            public string Error { get; set; } = "";
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            _pickedRules.Add(new PickedRule { Name = "Add Counter", Description = "Add counter", Rule = "AddCounter 1 2 3" });
            _pickedRules.Add(new PickedRule { Name = "Trim", Description = "Trim", Rule = "Trim" });
            _pickedRules.Add(new PickedRule { Name = "Change Extension", Description = "Change Extension", Rule = "ChangeExtension txt md" });

            RuleFactory.Register(new Trim());
            RuleFactory.Register(new AddCounter());
            RuleFactory.Register(new ChangeExtension());

            RuleFactory factory = new RuleFactory();

            foreach (var pickedRule in _pickedRules)
            {
                IRule rule = factory.Parse(pickedRule.Rule);
                _activeRules.Add(rule);
            }

      

        private void Button_OnClick(object sender, RoutedEventArgs e)
        {
            var screen = new RulesWindow();

                        PickedFile file = new PickedFile()
                        {
                            Filename = filename,
                            Extension = extension,
                            Path= path,
                        };

                        _pickedFiles.Add(file);
                    }
                }
            }
        }

        private void AddFolder_OnClick(object sender, RoutedEventArgs e)
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



        //public string First { get; set; } = "     abc txt google.txt";
        //public string Second { get; set; } = "123 giant.pdf";
        //public string Third { get; set; } = "   batch UltraMegaCop.txt      ";

                        PickedFile file = new PickedFile()
                        {
                            Filename = filename,
                            Extension = extension,
                            Path = path,
                        };

                        _pickedFiles.Add(file);
                    }
                }
            });
        }

        private void RemoveFiles_OnClick(object sender, RoutedEventArgs e)
        {
            while (pickedFilesDataGrid.SelectedItems.Count > 0)
            {
                int index = pickedFilesDataGrid.SelectedIndex;
                _pickedFiles.RemoveAt(index);
                _fileAddedList.RemoveAt(index);
            }
        }

        private void renameFileList()
        {
            foreach (var file in _pickedFiles)
            {
                file.Newname = file.Filename;
                file.NewExtension = file.Extension;
                foreach (var rule in _activeRules)
                {
                    FileInfor fileInfor = rule.Rename(new FileInfor() { Filename = file.Newname, Extension = file.NewExtension });
                    file.Newname = fileInfor.Filename;
                    file.NewExtension = fileInfor.Extension;
                }
            }
        }

        private void PreviewButton_OnClick(object sender, RoutedEventArgs e)
        {
            renameFileList();

        }

        private void StartButton_OnClick(object sender, RoutedEventArgs e)
        {
            renameFileList();

            foreach (var file in _pickedFiles)
            {
                File.Move(file.Path, file.Path.Replace(file.Filename + file.Extension, file.Newname+file.NewExtension));
            }
        }
    }
}
