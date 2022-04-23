using System;
using System.Collections.Generic;
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

namespace ShopManagementApp
{
    /// <summary>
    /// Interaction logic for AddNewCategoryDialogWindow.xaml
    /// </summary>
    public partial class AddNewCategoryDialogWindow : Window
    {
        public Category NewCategory { get; set; }
        public AddNewCategoryDialogWindow()
        {
            InitializeComponent();
            NewCategory = new Category();
            DataContext=NewCategory;
        }
        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult=true;
        }
    }
}
