using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
namespace ShopManagementApp
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
        }


        private void button1_Click(object sender, EventArgs e)
        {

            string user1 = AppConfig.GetValue(AppConfig.username1);
            string user2 = AppConfig.GetValue(AppConfig.username2);
            string user3 = AppConfig.GetValue(AppConfig.username3);

            System.Diagnostics.Debug.WriteLine(EncryptString(ToSecureString(passwordBox1.Password)));
            if (textBoxUser.Text.Equals(user1) || textBoxUser.Text.Equals(user2) || textBoxUser.Text.Equals(user3))
            {
                if (textBoxUser.Text.Equals(user1))
                {
                    string k = new System.Net.NetworkCredential(string.Empty, DecryptString(AppConfig.GetValue(AppConfig.password1))).Password;
                    System.Diagnostics.Debug.WriteLine(k);
                    if (k.Equals(passwordBox1.Password))
                    {
                        MessageBox.Show("Login successfully");

                        if (checkBox1.IsChecked == true)
                        {
                            Properties.Settings.Default.username = textBoxUser.Text;
                            Properties.Settings.Default.password = EncryptString(ToSecureString(passwordBox1.Password));
                            Properties.Settings.Default.Save();

                        }
                        else if (checkBox1.IsChecked == false)
                        {
                            Properties.Settings.Default.username = "";
                            Properties.Settings.Default.password = "";
                            Properties.Settings.Default.Save();

                        }

                    }
                    else
                    {
                        Properties.Settings.Default.username = "";
                        Properties.Settings.Default.password = "";
                        Properties.Settings.Default.Save();
                        MessageBox.Show("Wrong password");
                    }


                }


                if (textBoxUser.Text.Equals(user2))
                {
                    string k = new System.Net.NetworkCredential(string.Empty, DecryptString(AppConfig.GetValue(AppConfig.password2))).Password;
                    System.Diagnostics.Debug.WriteLine(k);
                    if (k.Equals(passwordBox1.Password))
                    {
                        MessageBox.Show("Login successfully");

                        if (checkBox1.IsChecked == true)
                        {
                            Properties.Settings.Default.username = textBoxUser.Text;
                            Properties.Settings.Default.password = EncryptString(ToSecureString(passwordBox1.Password));
                            Properties.Settings.Default.Save();

                        }
                        else if (checkBox1.IsChecked == false)
                        {
                            Properties.Settings.Default.username = "";
                            Properties.Settings.Default.password = "";
                            Properties.Settings.Default.Save();

                        }

                    }
                    else
                    {
                        Properties.Settings.Default.username = "";
                        Properties.Settings.Default.password = "";
                        Properties.Settings.Default.Save();
                        MessageBox.Show("Wrong password");
                    }
                }



                if (textBoxUser.Text.Equals(user3))
                {
                    string k = new System.Net.NetworkCredential(string.Empty, DecryptString(AppConfig.GetValue(AppConfig.password3))).Password;
                    System.Diagnostics.Debug.WriteLine(k);
                    if (k.Equals(passwordBox1.Password))
                    {
                        MessageBox.Show("Login successfully");

                        if (checkBox1.IsChecked == true)
                        {
                            Properties.Settings.Default.username = textBoxUser.Text;
                            Properties.Settings.Default.password = EncryptString(ToSecureString(passwordBox1.Password));
                            Properties.Settings.Default.Save();

                        }
                        else if (checkBox1.IsChecked == false)
                        {
                            Properties.Settings.Default.username = "";
                            Properties.Settings.Default.password = "";
                            Properties.Settings.Default.Save();

                        }

                    }
                    else
                    {
                        Properties.Settings.Default.username = "";
                        Properties.Settings.Default.password = "";
                        Properties.Settings.Default.Save();
                        MessageBox.Show("Wrong password");
                    }
                }

            }

            else
            {
                Properties.Settings.Default.username = "";
                Properties.Settings.Default.password = "";
                Properties.Settings.Default.Save();
                MessageBox.Show("Wrong username");
            }








        }






        class AppConfig
        {
            public static string Server = "Server";
            public static string Instance = "Instance";
            public static string Database = "Database";
            public static string Username = "Username";
            public static string Password = "Password";


            public static string password1 = "password1";
            public static string password2 = "password2";
            public static string password3 = "password3";

            public static string username1 = "username1";
            public static string username2 = "username2";
            public static string username3 = "username3";

            public static string? GetValue(string key)
            {
                string? value = ConfigurationManager
                    .AppSettings[key];
                return value;
            }

            public static void SetValue(string key, string value)
            {
                var configFile = ConfigurationManager
                .OpenExeConfiguration(ConfigurationUserLevel.None);
                var settings = configFile.AppSettings.Settings;
                settings[key].Value = value;

                configFile.Save(ConfigurationSaveMode.Minimal);
            }

            public static string? ConnectionString()
            {
                string? result = "";

                var builder = new SqlConnectionStringBuilder();
                string? server = AppConfig.GetValue(AppConfig.Server);
                string? instance = AppConfig.GetValue(AppConfig.Instance);
                string? database = AppConfig.GetValue(AppConfig.Database);
                string? username = AppConfig.GetValue(AppConfig.Username);
                string? password = AppConfig.GetValue(AppConfig.Password);

                builder.DataSource = $"{server}\\{instance}";
                builder.InitialCatalog = database;
                builder.IntegratedSecurity = true;
                builder.ConnectTimeout = 3; // s

                result = builder.ToString();
                return result;
            }
        }


        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //textBox1.Text = "UserName";
            //textBox2.Text = "Password";
            if (Properties.Settings.Default.username != string.Empty)
            {
                textBoxUser.Text = Properties.Settings.Default.username;
                checkBox1.IsChecked = true;
                SecureString password = DecryptString(Properties.Settings.Default.password);
                string readable = ToInsecureString(password);
                passwordBox1.Password = readable;
            }
        }











        static byte[] entropy = System.Text.Encoding.Unicode.GetBytes("Salt Is Not A Password");

        public static string EncryptString(System.Security.SecureString input)
        {
            byte[] encryptedData = System.Security.Cryptography.ProtectedData.Protect(
                System.Text.Encoding.Unicode.GetBytes(ToInsecureString(input)),
                entropy,
                System.Security.Cryptography.DataProtectionScope.CurrentUser);
            return Convert.ToBase64String(encryptedData);
        }

        public static SecureString DecryptString(string encryptedData)
        {
            try
            {
                byte[] decryptedData = System.Security.Cryptography.ProtectedData.Unprotect(
                    Convert.FromBase64String(encryptedData),
                    entropy,
                    System.Security.Cryptography.DataProtectionScope.CurrentUser);
                return ToSecureString(System.Text.Encoding.Unicode.GetString(decryptedData));
            }
            catch
            {
                return new SecureString();
            }
        }

        public static SecureString ToSecureString(string input)
        {
            SecureString secure = new SecureString();
            foreach (char c in input)
            {
                secure.AppendChar(c);
            }
            secure.MakeReadOnly();
            return secure;
        }

        public static string ToInsecureString(SecureString input)
        {
            string returnValue = string.Empty;
            IntPtr ptr = System.Runtime.InteropServices.Marshal.SecureStringToBSTR(input);
            try
            {
                returnValue = System.Runtime.InteropServices.Marshal.PtrToStringBSTR(ptr);
            }
            finally
            {
                System.Runtime.InteropServices.Marshal.ZeroFreeBSTR(ptr);
            }
            return returnValue;
        }


    }
}
