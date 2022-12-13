using System.Collections.Generic;
using System.Linq;
using System.Windows;
using Microsoft.WindowsAPICodePack.Dialogs;
using System.IO;
using Contract;
using System.Collections.ObjectModel;
using Microsoft.Win32;
using TrimRule;
using AddCounterRule;
using ChangeExtensionRule;


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

        private List<string> _fileAddedList = new List<string>();
        private ObservableCollection<PickedFile> _pickedFiles = new ObservableCollection<PickedFile>();
        private ObservableCollection<PickedRule> _pickedRules = new ObservableCollection<PickedRule>();
        private List<IRule> _activeRules = new List<IRule>();
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

            pickedRulesDataGrid.ItemsSource = _pickedRules;
            pickedFilesDataGrid.ItemsSource = _pickedFiles;
        }

        public class PickedRule
        {
            public string Name { get; set; } = "";
            public string Description { get; set; } = "";
            public string Rule { get; set; } = "";
        }

        private void AddFiles_OnClick(object sender, RoutedEventArgs e)
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
                        string filename = Path.GetFileNameWithoutExtension(path);
                        string extension = Path.GetExtension(path);

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

                // To store
                foreach (var path in _fileAddedList)
                {
                    if (!lastFileList.Contains(path))
                    {
                        string filename = Path.GetFileNameWithoutExtension(path);
                        string extension = Path.GetExtension(path);

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
                            Extension = extension,
                            Path = path,
                        };

                        _pickedFiles.Add(file);
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
    }
}
