using PasswordManagementLibrary;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Majales
{
    public partial class ChangePsswdWindow : Form
    {
        protected IPasswordManager pwdmng;
        protected string username;
        protected bool success = false;
        public bool Success { get => success; }

        public ChangePsswdWindow(string username, IPasswordManager pwdmng)
        {
            InitializeComponent();
            this.pwdmng = pwdmng;
            this.username = username;
        }

        private void BtnZmenit_Click(object sender, EventArgs e)
        {
            if (txtNove.Text != txtKontrola.Text)
            {
                lblMsg.Text = "Kontrola je jiná než zadané nové heslo";
            }
            else
            {
                try
                {
                    if (pwdmng.ChangePassword(username, txtStare.Text.Trim(), txtNove.Text.Trim()))
                    {
                        success = true;
                        Close();
                    }
                }
                catch (ArgumentException aex)
                {
                    lblMsg.Text = aex.Message;
                }
            }
        }
    }
}
