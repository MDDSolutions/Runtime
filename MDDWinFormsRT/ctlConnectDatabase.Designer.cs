namespace MDDWinFormsRT
{
    partial class ctlConnectDatabase
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        private Label labelServer;
        private TextBox textBoxServer;
        private Label labelDatabase;
        private TextBox textBoxDatabase;
        private Label labelUser;
        private TextBox textBoxUser;
        private Label labelPassword;
        private TextBox textBoxPassword;
        private CheckBox checkBoxWindowsAuth;
        private Button buttonOK;
        private Button buttonCancel;



        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            labelServer = new Label();
            textBoxServer = new TextBox();
            labelDatabase = new Label();
            textBoxDatabase = new TextBox();
            labelUser = new Label();
            textBoxUser = new TextBox();
            labelPassword = new Label();
            textBoxPassword = new TextBox();
            checkBoxWindowsAuth = new CheckBox();
            buttonOK = new Button();
            buttonCancel = new Button();
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
            textBoxServer.TabIndex = 1;
            // 
            // labelDatabase
            // 
            labelDatabase.AutoSize = true;
            labelDatabase.Location = new Point(12, 52);
            labelDatabase.Name = "labelDatabase";
            labelDatabase.Size = new Size(90, 25);
            labelDatabase.TabIndex = 2;
            labelDatabase.Text = "Database:";
            // 
            // textBoxDatabase
            // 
            textBoxDatabase.Location = new Point(100, 49);
            textBoxDatabase.Name = "textBoxDatabase";
            textBoxDatabase.Size = new Size(200, 31);
            textBoxDatabase.TabIndex = 3;
            // 
            // labelUser
            // 
            labelUser.AutoSize = true;
            labelUser.Location = new Point(12, 140);
            labelUser.Name = "labelUser";
            labelUser.Size = new Size(51, 25);
            labelUser.TabIndex = 4;
            labelUser.Text = "User:";
            // 
            // textBoxUser
            // 
            textBoxUser.Location = new Point(100, 137);
            textBoxUser.Name = "textBoxUser";
            textBoxUser.Size = new Size(200, 31);
            textBoxUser.TabIndex = 5;
            // 
            // labelPassword
            // 
            labelPassword.AutoSize = true;
            labelPassword.Location = new Point(12, 177);
            labelPassword.Name = "labelPassword";
            labelPassword.Size = new Size(91, 25);
            labelPassword.TabIndex = 6;
            labelPassword.Text = "Password:";
            // 
            // textBoxPassword
            // 
            textBoxPassword.Location = new Point(100, 174);
            textBoxPassword.Name = "textBoxPassword";
            textBoxPassword.PasswordChar = '*';
            textBoxPassword.Size = new Size(200, 31);
            textBoxPassword.TabIndex = 7;
            // 
            // checkBoxWindowsAuth
            // 
            checkBoxWindowsAuth.AutoSize = true;
            checkBoxWindowsAuth.Location = new Point(100, 112);
            checkBoxWindowsAuth.Name = "checkBoxWindowsAuth";
            checkBoxWindowsAuth.Size = new Size(141, 19);
            checkBoxWindowsAuth.TabIndex = 8;
            checkBoxWindowsAuth.Text = "Windows Authentication";
            checkBoxWindowsAuth.UseVisualStyleBackColor = true;
            checkBoxWindowsAuth.CheckedChanged += checkBoxWindowsAuth_CheckedChanged;
            // 
            // buttonOK
            // 
            buttonOK.Location = new Point(49, 237);
            buttonOK.Name = "buttonOK";
            buttonOK.Size = new Size(92, 36);
            buttonOK.TabIndex = 9;
            buttonOK.Text = "OK";
            buttonOK.UseVisualStyleBackColor = true;
            buttonOK.Click += buttonOK_Click;
            // 
            // buttonCancel
            // 
            buttonCancel.DialogResult = DialogResult.Cancel;
            buttonCancel.Location = new Point(166, 237);
            buttonCancel.Name = "buttonCancel";
            buttonCancel.Size = new Size(98, 36);
            buttonCancel.TabIndex = 10;
            buttonCancel.Text = "Cancel";
            buttonCancel.UseVisualStyleBackColor = true;
            // 
            // ctlConnectDatabase
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(buttonCancel);
            Controls.Add(buttonOK);
            Controls.Add(checkBoxWindowsAuth);
            Controls.Add(textBoxPassword);
            Controls.Add(labelPassword);
            Controls.Add(textBoxUser);
            Controls.Add(labelUser);
            Controls.Add(textBoxDatabase);
            Controls.Add(labelDatabase);
            Controls.Add(textBoxServer);
            Controls.Add(labelServer);
            Name = "ctlConnectDatabase";
            Size = new Size(342, 298);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
    }
}
