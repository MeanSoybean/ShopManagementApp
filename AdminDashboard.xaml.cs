using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
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
using Aspose.Cells;
using Fluent;
using Microsoft.Win32;
using ShopManagementApp.UserControls;

namespace ShopManagementApp
{
    /// <summary>
    /// Interaction logic for AdminDashboard.xaml
    /// </summary>
    public partial class AdminDashboard : RibbonWindow
    {
        public AdminDashboard()
        {
            InitializeComponent();
        }

        private ObservableCollection<TabItem> screens;
        private MasterDataUserControl masterDataUserControl;
        private SalesUserControl salesUserControl;
        private ReportUserControl reportUserControl;
        private void RibbonWindow_Loaded(object sender, RoutedEventArgs e)
        {
            masterDataUserControl = new MasterDataUserControl();
            salesUserControl = new SalesUserControl();
            reportUserControl = new ReportUserControl();
            screens = new ObservableCollection<TabItem>()
            {
                new TabItem() { Name="MasterDataUserControl", Content = masterDataUserControl },
                new TabItem() { Name="SalesUserControl", Content = salesUserControl },
                new TabItem() { Name="ReportUserControl", Content = reportUserControl },
            };
            Tabs.ItemsSource=screens;
        }

        private void ExitMenu_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Close();
        }

        private void ExcelImportButton_Clicked(object sender, RoutedEventArgs e)
        {
            // Get file
            var filename = GetFileNameFromOpenFileDialog();
            if (filename == null) return;

            // Prepare to read Categories
            var workbook = new Workbook(filename);
            var sheet = workbook.Worksheets[0];
            var row = 2;
            var col = 'B';
            var cell = sheet.Cells[$"{col}{row}"];

            // Prepare database connection
            var db = new MyShopEntities();
            var categories = new List<Category>();
            // Iterate and save
            do
            {
                // Save to list in memory
                var name = cell.StringValue;
                categories.Add(new Category { Name = name });

                // Next row
                row++;
                cell = sheet.Cells[$"{col}{row}"];
            } while (cell.Value != null);
            // Save to db
            db.Categories.AddRange(categories);
            db.SaveChanges();

            // Get ID of Category based on name
            var nameToIdDictionary = new Dictionary<string, int>();
            foreach (var category in categories)
            {
                nameToIdDictionary.Add(category.Name, category.ID);
            }

            // Read Products
            sheet = workbook.Worksheets[1];
            row = 2;
            cell = sheet.Cells[$"B{row}"];
            var productCount = 0;
            var products = new List<Product>();
            do
            {
                var categoryName = cell.StringValue;
                var productName = sheet.Cells[$"C{row}"].StringValue;
                var price = sheet.Cells[$"D{row}"].FloatValue;
                var quantity = sheet.Cells[$"E{row}"].IntValue;
                var imagePath = sheet.Cells[$"F{row}"].StringValue;
                products.Add(new Product {
                    CatID = nameToIdDictionary[categoryName],
                    Name = productName,
                    Price = price.ToString(),
                    Quantity = quantity,
                    ImagePath = imagePath
                });
                row++;
                cell = sheet.Cells[$"B{row}"];
                productCount++;
            } while (cell.Value != null);
            db.Products.AddRange(products);
            db.SaveChanges();

            MessageBox.Show($"Added {categories.Count} categories and {productCount} products to the system.", "Import Success", MessageBoxButton.OK, MessageBoxImage.Information);

            masterDataUserControl.ReloadInitialize();
        }

        private string GetFileNameFromOpenFileDialog()
        {
            var dialog = new OpenFileDialog();
            bool? result = dialog.ShowDialog();
            if (result == true)
            {
                return dialog.FileName;
            }
            return null;
        }

        private void AddCategoryButton_Clicked(object sender, RoutedEventArgs e)
        {
            masterDataUserControl.HandleParentEvent(
                MasterDataUserControl.MasterDataAction.AddNewCategory
                );
        }

        private void DeleteCategoryButton_Clicked(object sender, RoutedEventArgs e)
        {
            masterDataUserControl.HandleParentEvent(
                MasterDataUserControl.MasterDataAction.DeleteSelectedCategory
                );
        }

        private void AddProductButton_Clicked(object sender, RoutedEventArgs e)
        {
            masterDataUserControl.HandleParentEvent(
                MasterDataUserControl.MasterDataAction.AddNewProduct
                );
        }

        private void EditProductButton_Clicked(object sender, RoutedEventArgs e)
        {
            masterDataUserControl.HandleParentEvent(
                MasterDataUserControl.MasterDataAction.EditSelectedProduct
                );
        }

        private void DeleteProductButton_Clicked(object sender, RoutedEventArgs e)
        {
            masterDataUserControl.HandleParentEvent(
                MasterDataUserControl.MasterDataAction.DeleteSelectedProduct
                );
        }

        private void AddPurchaseButton_Clicked(object sender, RoutedEventArgs e)
        {
            salesUserControl.HandleParentEvent(
                SalesUserControl.SalesUserAction.AddNewPurchase
                );
        }

        private void EditPurchaseButton_Clicked(object sender, RoutedEventArgs e)
        {
            salesUserControl.HandleParentEvent(
                SalesUserControl.SalesUserAction.EditSelectedPurchase
                );
        }

        private void DeletePurchaseButton_Clicked(object sender, RoutedEventArgs e)
        {
            salesUserControl.HandleParentEvent(
                SalesUserControl.SalesUserAction.DeleteSelectedPurchase
                );
        }
    }
}
