using ClientLibrary;
using LogicalDatabaseLibrary;
using PasswordManagementLibrary;
using DatabaseDefinitions;
using Fei.BaseLib;

class Program
{   
    public static void Main()
    {
        //InitPsswds();
        //InitSettings();
    }

    private static void InitSettings()
    {
        IDatabaseManager dtb = new SimpleManager();
        dtb.NewDatabase(new() { Name = "settings", Path = Path.Combine(GlobalDefinitions.WorkingDirectory, "settings") });
        dtb.AddTable(
            (dm) => dm.Name == "settings",
            new()
            {
                TableName = "autologin",
                Head = new()
                {
                    Separator = ";",
                    LineLimit = 10,
                    MaxId = 0,
                    StartPage = 0,
                    Entity = new TableEntity(new Attr("username"), new Attr("auto", typeof(bool)))
                }
            }
        );
        dtb.AddLine(
            (dm) => dm.Name == "settings",
            (tm) => tm.Name == "autologin",
            new TableLine(0, "petr", true)
        );
    }

    private static void InitPsswds()
    {
        IPasswordManager pwdmng = new LocalPasswordManager(new LocalPasswordStorage(new SimpleManager()));
        pwdmng.NewPassword("petr", "veliceTajne");
    }
}
