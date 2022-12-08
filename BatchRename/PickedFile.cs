using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BatchRename
{
    internal class PickedFile : INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler? PropertyChanged;

        public string Filename { get; set; } = "";
        public string Extension { get; set; } = "";
        public string Newname { get; set; } = "";
        public string NewExtension { get; set; } = "";
        public string Path { get; set; } = "";
        public string Error { get; set; } = "";
    }
}
