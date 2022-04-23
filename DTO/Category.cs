using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopManagementApp
{
    public partial class Category : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public override string ToString()
        {
            return this.Name;
        }
    }
}
