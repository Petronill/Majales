using PasswordManagementLibrary;
using ClientLibrary;
using DatabaseLibrary;
using DatabaseDefinitions;
using LogicalDatabaseLibrary;

namespace Majales
{
    public partial class MainWindow : Form
    {
        private TeamWindow? teamWindow;
        private EventWindow? eventWindow;
        private TaskWindow? taskWindow;
        private IDatabaseManager database = new SimpleManager();
        private IPasswordManager pwdmng;
        private string? username;
        
        public MainWindow()
        {
            InitializeComponent();
            pwdmng = new PasswordManager(database);

            Login();
        }

        private void Login()
        {
            string autouser = string.Empty;
            database.OpenDatabase(
                new() { Name = "settings", Path = Path.Combine(Directory.GetCurrentDirectory(), "settings") }
            );
            Table autologin = database.GetDatabaseTables(
                (dm) => dm.Name == "settings",
                (tm) => tm.Name == "autologin"
            )[0];
            foreach (var r in autologin)
            {
                if ((bool)r.Line[2])
                {
                    autouser = (string)r.Line[1];
                    break;
                }
            }

            LoginWindow login = new(autouser, pwdmng);
            login.ShowDialog();

            if (!login.Success)
            {
                this.Enabled = false;
            }
            else
            {
                username = login.Username;
                foreach (Row r in autologin)
                {
                    if ((string)r.Line[1] == username)
                    {
                        r.Line[2] = login.Remember;
                        Console.WriteLine(r.Line[2]);
                        autologin[r.Line.GetId()] = r.Line;
                        return;
                    }

                }

                if (login.Remember)
                {
                    autologin.Add(new TableLine(0, username, true));
                }
            }
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
                eventWindow.FormClosed += (s, e) => eventWindow = null;
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
                taskWindow.FormClosed += (s, e) => taskWindow = null;
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
            ChangePsswdWindow changePsswd = new(username, pwdmng);
            changePsswd.FormClosed += (s, e) => PsswdChangeResult(changePsswd.Success);
            changePsswd.Show();
        }
    }
}