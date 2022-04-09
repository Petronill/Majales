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
    public partial class LoginWindow : Form
    {
        protected int attempts = 3;
        protected bool success = false;
        protected IPasswordManager pwdmng;
        public bool Success { get => success; }
        public string? Username { get; set; }
        public bool Remember { get; set; }
        
        public LoginWindow(string initial, IPasswordManager pwdmng)
        {
            InitializeComponent();
            this.pwdmng = pwdmng;
            if (initial.Length != 0)
            {
                jmenoBox.Text = initial;
                chckName.Checked = true;
            }
        }

        private void BtnPrihlasit_Click(object sender, EventArgs e)
        {
            try
            {
                if (pwdmng.VerifyPassword(jmenoBox.Text.Trim(), hesloBox.Text.Trim()))
                {
                    success = true;
                    Username = jmenoBox.Text.Trim();
                    Remember = chckName.Checked;
                    Close();
                }
                else {
                    attempts--;
                    if (attempts == 0)
                    {
                        success = false;
                        Neprihlaseno();
                        Close();
                    }
                    else
                    {
                        lblMsg.Text = $"Špatně, zbývá {attempts} pokusů";
                    }
                }
            } catch (ArgumentException aex)
            {
                lblMsg.Text = aex.Message;
            }
        }

        private void Neprihlaseno()
        {
            const string message = "Přihlášení";
            const string caption = "Zadané uživatelské jméno nebo heslo není platné";
            MessageBox.Show(caption, message, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void ZapomenuteHeslo()
        {
            const string message = "Řešení zapomenutého hesla";
            const string caption = "Obraťte se na svého admina (petr.cech@kolinskymajales.cz)";
            MessageBox.Show(caption, message, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        
        private void BtnZapomenute_Click(object sender, EventArgs e)
        {
            ZapomenuteHeslo();
            success = false;
            Close();
        }
    }
}
