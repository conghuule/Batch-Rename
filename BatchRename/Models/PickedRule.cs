using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BatchRename.Models
{
    public class PickedRule : INotifyPropertyChanged
    {
        public string Number { get; set; } = "";
        public string Name { get; set; } = "";
        public string Description { get; set; } = "";

        public event PropertyChangedEventHandler? PropertyChanged;
    }
}
