using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ShopManagementApp.UserControls
{
    /// <summary>
    /// Interaction logic for SalesUserControl.xaml
    /// </summary>
    public partial class SalesUserControl : UserControl
    {
        public enum SalesUserAction
        {
            AddNewPurchase,
            EditSelectedPurchase,
            DeleteSelectedPurchase,
        }

        public void HandleParentEvent(SalesUserAction action)
        {
            switch (action)
            {
                //TODO
                case SalesUserAction.AddNewPurchase:
                    {

                    }
                    break;
                case SalesUserAction.EditSelectedPurchase:
                    {


                    }
                    break;
                case SalesUserAction.DeleteSelectedPurchase:
                    {
                        var sv = (Purchase)ProductsDataGrid.SelectedItem;
                        var k = ProductsDataGrid.SelectedIndex;
                        System.Diagnostics.Debug.WriteLine(sv.ID);

                        var connectionString =
                          "Server=.\\sqlexpress;Database=MyShop;Trusted_Connection=True;";

                        // Kết nối
                        var connection = new SqlConnection(connectionString);
                        string sqlStatement = "DELETE FROM Purchase WHERE ID = @IDtemp";

                        try
                        {
                            connection.Open();
                            SqlCommand cmd = new SqlCommand(sqlStatement, connection);
                            cmd.Parameters.AddWithValue("@IDtemp", sv.ID);
                            cmd.CommandType = CommandType.Text;
                            cmd.ExecuteNonQuery();

                            list_pur.RemoveAt(k);
                            ProductsDataGrid.ItemsSource = null;
                            ProductsDataGrid.ItemsSource = list_pur;
                        }
                        finally
                        {
                            connection.Close();
                        }
                    }
                    break;
                default:
                    break;
            }
        }
        List<Purchase> list_pur = new List<Purchase>();
        class Purchase
        {
            public int ID { get; set; }
            public int CustomerID { get; set; }
            public int FinalTotal { get; set; }
        }

        private void SalesUserControl_Initialized(object sender, EventArgs e)
        {
            ReloadInitialize();
        }
        public void ReloadInitialize()
        {
            string? result = "";

            var builder = new SqlConnectionStringBuilder();
            builder.DataSource = $"{"localhost"}\\{"SqlExpress"}";
            builder.InitialCatalog = "MyShop";
            builder.IntegratedSecurity = true;
            builder.ConnectTimeout = 3; // s

            result = builder.ToString();

            var connectionString =
                "Server=.\\sqlexpress;Database=MyShop;Trusted_Connection=True;";

            // Kết nối
            var connection = new SqlConnection(connectionString);

            try
            {
                connection.Open();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            // Sau khi kết nối thành công
            var sql = "select * from Purchase";
            var command = new SqlCommand(sql, connection);

            var reader = command.ExecuteReader();



            // Xử lí kết quả trả về của câu truy vấn
            // ORM - Object relational mapping

            while (reader.Read())
            {
                var id = (int)reader["ID"];
                var cus_id = (int)reader["CustomerID"];
                var price = (int)reader["FinalToTal"];
                Purchase purchase = new Purchase()
                {
                    ID = id,
                    CustomerID = cus_id,
                    FinalTotal = price
                };
                list_pur.Add(purchase);
            }

            // Hiển thị
            ProductsDataGrid.ItemsSource = list_pur;

            reader.Close();


            /*   SqlConnection con = new SqlConnection(result);
               con.Open();
               SqlCommand cmd = con.CreateCommand();
               cmd.CommandType = System.Data.CommandType.Text;
               cmd.CommandText = "select * from Purchase";
               cmd.ExecuteNonQuery();
               SqlDataAdapter da = new SqlDataAdapter(cmd);
               DataTable dt = new DataTable();
               da.Fill(dt);
               var temp = new List<Purchase>();
               var s = dt.Rows.Count;
               ProductsDataGrid.ItemsSource = dt.DefaultView;
               ProductsDataGrid.SelectedIndex = 0;
               System.Diagnostics.Debug.WriteLine(s);*/



        }

        private void Changed_ABC(object sender, SelectedCellsChangedEventArgs e)
        {
            ProductsDataGrid.ItemsSource = list_pur;
        }
        public SalesUserControl()
        {
            InitializeComponent();
            /*            string? result = "";

                        var builder = new SqlConnectionStringBuilder();
                        builder.DataSource = $"{"localhost"}\\{"SqlExpress"}";
                        builder.InitialCatalog = "MyShop";
                        builder.IntegratedSecurity = true;
                        builder.ConnectTimeout = 3; // s

                        result = builder.ToString();
                        SqlConnection con = new SqlConnection(result);
                        con.Open();
                        SqlCommand cmd = con.CreateCommand();
                        cmd.CommandType = System.Data.CommandType.Text;
                        cmd.CommandText = "select * from Purchase";
                        cmd.ExecuteNonQuery();
                        DataTable dt = new DataTable();
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        da.Fill(dt);
                        CHECK.ItemsSource = dt.DefaultView;
                        var sv =CHECK.SelectedItem;
                        var temp = sv[0];
                        System.Diagnostics.Debug.WriteLine(sv[0].ID);*/
        }
    }
}
