namespace Majales
{
    public partial class MainWindow : Form
    {
        private TeamWindow? teamWindow;
        private EventWindow? eventWindow;
        private TaskWindow? taskWindow;
        
        public MainWindow()
        {
            InitializeComponent();

            LoginWindow login = new();
            login.ShowDialog();

            if (!login.Success)
            {
                this.Enabled = false;
            }
        }

        private void MainWindow_Load(object sender, EventArgs e)
        {
        }

        private void BtnTym_Click(object sender, EventArgs e)
        {
            if (teamWindow is null)
            {
                teamWindow = new();
                teamWindow.FormClosed += (s, e) => teamWindow = null;
                teamWindow.Show();
            }
            else
            {
                teamWindow.Activate();
            }
        }

        private void BtnProgram_Click(object sender, EventArgs e)
        {
            if (eventWindow is null)
            {
                eventWindow = new();
                eventWindow.FormClosed += (s, e) => teamWindow = null;
                eventWindow.Show();
            }
            else
            {
                eventWindow.Activate();
            }
        }

        private void BtnUkoly_Click(object sender, EventArgs e)
        {
            if (taskWindow is null)
            {
                taskWindow = new();
                taskWindow.FormClosed += (s, e) => teamWindow = null;
                taskWindow.Show();
            }
            else
            {
                taskWindow.Activate();
            }
        }

        private void PsswdChangeResult(bool success)
        {
            const string message = "Zmìna hesla";
            if (success)
            {
                const string caption = "Heslo bylo zmìnìno";
                MessageBox.Show(caption, message, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                const string caption = "Došlo k chybì pøi zmìnì hesla";
                MessageBox.Show(caption, message, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnZmenitHeslo_Click(object sender, EventArgs e)
        {
            ChangePsswdWindow changePsswd = new();
            changePsswd.FormClosed += (s, e) => PsswdChangeResult(changePsswd.Success);
            changePsswd.Show();
        }
    }
}