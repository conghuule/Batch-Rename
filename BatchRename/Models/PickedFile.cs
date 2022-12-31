using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BatchRename.Models
{
    public class PickedFile : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        public string Filename { get; set; } = "";
        public string Newname { get; set; } = "";
        public string Path { get; set; } = "";
        public string Error { get; set; } = "";
        public string Number { get; set; } = "";
    }
}
