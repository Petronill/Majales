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
        protected bool success = false;
        public bool Success { get => success; }

        public ChangePsswdWindow()
        {
            InitializeComponent();
        }

        private void BtnZmenit_Click(object sender, EventArgs e)
        {
            //TODO: zkontrolovat heslo
            if (TxtStare.Text == TxtNove.Text)
            {
                LblMsg.Text = "Nové heslo nesmí být stejné jako staré";
            }
            else if (TxtNove.Text != TxtKontrola.Text)
            {
                LblMsg.Text = "Kontrola je jiná než zadané nové heslo";
            }
            else
            {
                success = true;
                Close();
            }
        }
    }
}
