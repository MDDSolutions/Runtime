using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MDDWinFormsRT
{
    public partial class ctlConnectDatabase : UserControl
    {
        public ctlConnectDatabase()
        {
            InitializeComponent();
        }
        private void checkBoxWindowsAuth_CheckedChanged(object sender, EventArgs e)
        {
            bool isChecked = checkBoxWindowsAuth.Checked;
            textBoxUser.Enabled = !isChecked;
            textBoxPassword.Enabled = !isChecked;
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            //var builder = new SqlConnectionStringBuilder
            //{
            //    DataSource = textBoxServer.Text,
            //    InitialCatalog = textBoxDatabase.Text
            //};

            //if (checkBoxWindowsAuth.Checked)
            //{
            //    builder.IntegratedSecurity = true;
            //}
            //else
            //{
            //    builder.UserID = textBoxUser.Text;
            //    builder.Password = textBoxPassword.Text;
            //}

            //ConnectionString = builder.ConnectionString;
            //DialogResult = DialogResult.OK;
            //Close();
        }
    }
}
