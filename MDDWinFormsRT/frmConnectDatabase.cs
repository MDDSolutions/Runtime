using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace MDDWinFormsRT
{
    public partial class frmConnectDatabase : Form
    {
        public SqlConnectionElements ConnectionElements { get; private set; } = new SqlConnectionElements();

        public frmConnectDatabase()
        {
            InitializeComponent();
        }
        public Func<string, string[]>? GetDatabases { get; set; }

        private void InitializeComponent()
        {
            labelServer = new Label();
            textBoxServer = new TextBox();
            labelDatabase = new Label();
            labelUser = new Label();
            textBoxUser = new TextBox();
            labelPassword = new Label();
            textBoxPassword = new TextBox();
            checkBoxWindowsAuth = new CheckBox();
            buttonOK = new Button();
            buttonCancel = new Button();
            cbxDatabase = new ComboBox();
            SuspendLayout();
            // 
            // labelServer
            // 
            labelServer.AutoSize = true;
            labelServer.Location = new Point(12, 15);
            labelServer.Name = "labelServer";
            labelServer.Size = new Size(65, 25);
            labelServer.TabIndex = 0;
            labelServer.Text = "Server:";
            // 
            // textBoxServer
            // 
            textBoxServer.Location = new Point(100, 12);
            textBoxServer.Name = "textBoxServer";
            textBoxServer.Size = new Size(200, 31);
            textBoxServer.TabIndex = 0;
            textBoxServer.Validated += textBoxServer_Validated;
            // 
            // labelDatabase
            // 
            labelDatabase.AutoSize = true;
            labelDatabase.Location = new Point(12, 163);
            labelDatabase.Name = "labelDatabase";
            labelDatabase.Size = new Size(90, 25);
            labelDatabase.TabIndex = 2;
            labelDatabase.Text = "Database:";
            // 
            // labelUser
            // 
            labelUser.AutoSize = true;
            labelUser.Location = new Point(12, 89);
            labelUser.Name = "labelUser";
            labelUser.Size = new Size(51, 25);
            labelUser.TabIndex = 4;
            labelUser.Text = "User:";
            // 
            // textBoxUser
            // 
            textBoxUser.Location = new Point(100, 86);
            textBoxUser.Name = "textBoxUser";
            textBoxUser.Size = new Size(200, 31);
            textBoxUser.TabIndex = 3;
            textBoxUser.TextChanged += textBoxUser_TextChanged;
            // 
            // labelPassword
            // 
            labelPassword.AutoSize = true;
            labelPassword.Location = new Point(12, 126);
            labelPassword.Name = "labelPassword";
            labelPassword.Size = new Size(91, 25);
            labelPassword.TabIndex = 6;
            labelPassword.Text = "Password:";
            // 
            // textBoxPassword
            // 
            textBoxPassword.Location = new Point(100, 123);
            textBoxPassword.Name = "textBoxPassword";
            textBoxPassword.PasswordChar = '*';
            textBoxPassword.Size = new Size(200, 31);
            textBoxPassword.TabIndex = 4;
            textBoxPassword.TextChanged += textBoxPassword_TextChanged;
            // 
            // checkBoxWindowsAuth
            // 
            checkBoxWindowsAuth.AutoSize = true;
            checkBoxWindowsAuth.Location = new Point(100, 51);
            checkBoxWindowsAuth.Name = "checkBoxWindowsAuth";
            checkBoxWindowsAuth.Size = new Size(232, 29);
            checkBoxWindowsAuth.TabIndex = 2;
            checkBoxWindowsAuth.Text = "Windows Authentication";
            checkBoxWindowsAuth.UseVisualStyleBackColor = true;
            checkBoxWindowsAuth.CheckedChanged += checkBoxWindowsAuth_CheckedChanged;
            // 
            // buttonOK
            // 
            buttonOK.Location = new Point(101, 199);
            buttonOK.Name = "buttonOK";
            buttonOK.Size = new Size(79, 39);
            buttonOK.TabIndex = 5;
            buttonOK.Text = "OK";
            buttonOK.UseVisualStyleBackColor = true;
            buttonOK.Click += buttonOK_Click;
            // 
            // buttonCancel
            // 
            buttonCancel.DialogResult = DialogResult.Cancel;
            buttonCancel.Location = new Point(186, 199);
            buttonCancel.Name = "buttonCancel";
            buttonCancel.Size = new Size(83, 39);
            buttonCancel.TabIndex = 6;
            buttonCancel.Text = "Cancel";
            buttonCancel.UseVisualStyleBackColor = true;
            // 
            // cbxDatabase
            // 
            cbxDatabase.FormattingEnabled = true;
            cbxDatabase.Location = new Point(100, 160);
            cbxDatabase.Name = "cbxDatabase";
            cbxDatabase.Size = new Size(200, 33);
            cbxDatabase.TabIndex = 1;
            cbxDatabase.SelectedIndexChanged += cbxDatabase_SelectedIndexChanged;
            // 
            // frmConnectDatabase
            // 
            AcceptButton = buttonOK;
            CancelButton = buttonCancel;
            ClientSize = new Size(347, 269);
            Controls.Add(cbxDatabase);
            Controls.Add(buttonCancel);
            Controls.Add(buttonOK);
            Controls.Add(checkBoxWindowsAuth);
            Controls.Add(textBoxPassword);
            Controls.Add(labelPassword);
            Controls.Add(textBoxUser);
            Controls.Add(labelUser);
            Controls.Add(labelDatabase);
            Controls.Add(textBoxServer);
            Controls.Add(labelServer);
            Name = "frmConnectDatabase";
            Text = "Database Connection";
            ResumeLayout(false);
            PerformLayout();
        }

        private void checkBoxWindowsAuth_CheckedChanged(object? sender, EventArgs? e)
        {
            bool isChecked = checkBoxWindowsAuth.Checked;
            textBoxUser.Enabled = !isChecked;
            textBoxPassword.Enabled = !isChecked;
            ConnectionElements.IntegratedSecurity = isChecked;
            RefreshDatabaseList();
        }

        private void buttonOK_Click(object? sender, EventArgs? e)
        {
            ConnectionElements = new SqlConnectionElements
            {
                DataSource = textBoxServer.Text,
                InitialCatalog = cbxDatabase.Text
            };

            if (checkBoxWindowsAuth.Checked)
            {
                ConnectionElements.IntegratedSecurity = true;
            }
            else
            {
                ConnectionElements.UserID = textBoxUser.Text;
                ConnectionElements.Password = textBoxPassword.Text;
            }
            if (!ConnectionElements.IsValid)
            {
                MessageBox.Show("Please enter all required fields.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            DialogResult = DialogResult.OK;
            Close();
        }

        private Label labelServer = null!;
        private TextBox textBoxServer = null!;
        private Label labelDatabase = null!;
        private Label labelUser = null!;
        private TextBox textBoxUser = null!;
        private Label labelPassword = null!;
        private TextBox textBoxPassword = null!;
        private CheckBox checkBoxWindowsAuth = null!;
        private Button buttonOK = null!;
        private ComboBox cbxDatabase = null!;
        private Button buttonCancel = null!;

        private void textBoxServer_Validated(object? sender, EventArgs? e)
        {
            ConnectionElements.DataSource = textBoxServer.Text;
            RefreshDatabaseList();
        }
        private void RefreshDatabaseList()
        {
            var connectionStringMaster = ConnectionElements.ConnectionStringMaster;
            if (GetDatabases != null && connectionStringMaster != string.Empty)
            {
                try
                {
                    var dbs = GetDatabases(connectionStringMaster);
                    cbxDatabase.Items.Clear();
                    cbxDatabase.Items.AddRange(dbs);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error when retrieving list of databases for server {textBoxServer.Text}: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void textBoxUser_TextChanged(object? sender, EventArgs? e)
        {
            ConnectionElements.UserID = textBoxUser.Text;
        }

        private void textBoxPassword_TextChanged(object? sender, EventArgs? e)
        {
            ConnectionElements.Password = textBoxPassword.Text;
        }

        private void cbxDatabase_SelectedIndexChanged(object? sender, EventArgs? e)
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