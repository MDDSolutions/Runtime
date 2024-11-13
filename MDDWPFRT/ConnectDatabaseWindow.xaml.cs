using System;
using System.Linq;
using System.Windows;
using MDDDataAccess;

namespace MDDWPFRT
{
    public partial class ConnectDatabaseWindow : Window
    {
        public SqlConnectionElements ConnectionElements { get; private set; } = new SqlConnectionElements();
        public Func<string, string[]>? GetDatabases { get; set; }

        public ConnectDatabaseWindow()
        {
            InitializeComponent();
            checkBoxWindowsAuth.Checked += checkBoxWindowsAuth_CheckedChanged;
            textBoxServer.LostFocus += textBoxServer_Validated;
            textBoxUser.TextChanged += textBoxUser_TextChanged;
            textBoxPassword.PasswordChanged += textBoxPassword_TextChanged;
            cbxDatabase.SelectionChanged += cbxDatabase_SelectedIndexChanged;
        }

        private void checkBoxWindowsAuth_CheckedChanged(object sender, RoutedEventArgs e)
        {
            bool isChecked = checkBoxWindowsAuth.IsChecked == true;
            textBoxUser.IsEnabled = !isChecked;
            textBoxPassword.IsEnabled = !isChecked;
            ConnectionElements.IntegratedSecurity = isChecked;
            RefreshDatabaseList();
        }

        private void buttonOK_Click(object sender, RoutedEventArgs e)
        {
            ConnectionElements = new SqlConnectionElements
            {
                DataSource = textBoxServer.Text,
                InitialCatalog = cbxDatabase.Text
            };

            if (checkBoxWindowsAuth.IsChecked == true)
            {
                ConnectionElements.IntegratedSecurity = true;
            }
            else
            {
                ConnectionElements.UserID = textBoxUser.Text;
                ConnectionElements.Password = textBoxPassword.Password;
            }

            if (!ConnectionElements.IsValid)
            {
                MessageBox.Show("Please enter all required fields.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            DialogResult = true;
            Close();
        }

        private void buttonCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }

        private void textBoxServer_Validated(object sender, RoutedEventArgs e)
        {
            ConnectionElements.DataSource = textBoxServer.Text;
            RefreshDatabaseList();
        }

        private void RefreshDatabaseList()
        {
            var connectionStringMaster = ConnectionElements.ConnectionStringMaster;
            if (GetDatabases != null && !string.IsNullOrEmpty(connectionStringMaster))
            {
                try
                {
                    var dbs = GetDatabases(connectionStringMaster);
                    cbxDatabase.Items.Clear();
                    foreach (var db in dbs)
                    {
                        cbxDatabase.Items.Add(db);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error when retrieving list of databases for server {textBoxServer.Text}: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void textBoxUser_TextChanged(object sender, RoutedEventArgs e)
        {
            ConnectionElements.UserID = textBoxUser.Text;
        }

        private void textBoxPassword_TextChanged(object sender, RoutedEventArgs e)
        {
            ConnectionElements.Password = textBoxPassword.Password;
        }

        private void cbxDatabase_SelectedIndexChanged(object sender, RoutedEventArgs e)
        {
            ConnectionElements.InitialCatalog = cbxDatabase.Text;
        }
    }

    public class SqlConnectionElements
    {
        public string? DataSource { get; set; }
        public string? InitialCatalog { get; set; }
        public string? UserID { get; set; }
        public string? Password { get; set; }
        public bool IntegratedSecurity { get; set; }
        public string ConnectionString
        {
            get
            {
                if (IntegratedSecurity)
                {
                    return $"Data Source={DataSource};Initial Catalog={InitialCatalog};Integrated Security=True";
                }
                else
                {
                    return $"Data Source={DataSource};Initial Catalog={InitialCatalog};User ID={UserID};Password={Password}";
                }
            }
        }
        public string ConnectionStringMaster
        {
            get
            {
                if (!string.IsNullOrEmpty(DataSource) &&
                    (IntegratedSecurity || (!string.IsNullOrEmpty(UserID) && !string.IsNullOrEmpty(Password))))
                {
                    if (IntegratedSecurity)
                    {
                        return $"Data Source={DataSource};Initial Catalog=master;Integrated Security=True";
                    }
                    else
                    {
                        return $"Data Source={DataSource};Initial Catalog=master;User ID={UserID};Password={Password}";
                    }
                }
                return string.Empty;
            }
        }
        public bool IsValid
        {
            get
            {
                return !string.IsNullOrEmpty(DataSource) && !string.IsNullOrEmpty(InitialCatalog) &&
                    (IntegratedSecurity || (!string.IsNullOrEmpty(UserID) && !string.IsNullOrEmpty(Password)));
            }
        }
    }
}
