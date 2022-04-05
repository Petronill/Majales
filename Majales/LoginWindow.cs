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
        protected bool success = false;
        public bool Success { get => success; }
        
        public LoginWindow()
        {
            InitializeComponent();
        }

        private void BtnPrihlasit_Click(object sender, EventArgs e)
        {
            //TODO: ověřit přihlašovací údaje
            success = true;
            Close();
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
