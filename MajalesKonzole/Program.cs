using ClientLibrary;
using LogicalDatabaseLibrary;
using PasswordManagementLibrary;
using DatabaseDefinitions;
using MajalesLibrary;
using Fei.BaseLib;

class Program
{   
    public static void Main()
    {
        //InitPsswds();
        //InitSettings();
        InitMajalesTables();
    }

    private static void InitMajalesTables()
    {
        IDatabaseManager dtb = new SimpleManager();
        dtb.NewDatabase(new() { Name = "majales", Path = Path.Combine(GlobalDefinitions.WorkingDirectory, "majales") });

        dtb.AddTable(
            (dm) => dm.Name == "majales",
            new()
            {
                TableName = "sekce",
                Head = new()
                {
                    Separator = ";",
                    LineLimit = 10,
                    MaxId = 0,
                    StartPage = 0,
                    Entity = new TableEntity(
                        new Attr("jmeno"),
                        new Attr("email"),
                        new Attr("privilegia", typeof(uint)),
                        new Attr("rozpocet", typeof(int))
                    )
                }
            }
        );

        dtb.AddTable(
            (dm) => dm.Name == "majales",
            new()
            {
                TableName = "lide",
                Head = new()
                {
                    Separator = ";",
                    LineLimit = 100,
                    MaxId = 0,
                    StartPage = 0,
                    Entity = new TableEntity(
                        new Attr("jmeno"),
                        new Attr("prijmeni"),
                        new Attr("narozeni", typeof(DateOnly)),
                        new Attr("telefon", typeof(ulong)),
                        new Attr("email"),
                        new Attr("funkce")
                    )
                }
            }
        );

        dtb.AddTable(
            (dm) => dm.Name == "majales",
            new()
            {
                TableName = "projekty",
                Head = new()
                {
                    Separator = ";",
                    LineLimit = 100,
                    MaxId = 0,
                    StartPage = 0,
                    Entity = new TableEntity(
                        new Attr("nazev"),
                        new Attr("narocnost", typeof(int)),
                        new Attr("stav")
                    )
                }
            }
        );

        dtb.AddTable(
            (dm) => dm.Name == "majales",
            new()
            {
                TableName = "akce",
                Head = new()
                {
                    Separator = ";",
                    LineLimit = 100,
                    MaxId = 0,
                    StartPage = 0,
                    Entity = new TableEntity(
                        new Attr("jmeno"),
                        new Attr("popis"),
                        new Attr("kategorie"),
                        new Attr("tema"),
                        new Attr("misto"),
                        new Attr("zacatek", typeof(DateTime)),
                        new Attr("konec", typeof(DateTime)),
                        new Attr("rezim", typeof(RezimAkce)),
                        new Attr("stav"),
                        new Attr("cilovka"),
                        new Attr("kapacita", typeof(uint))
                    )
                }
            }
        );

        dtb.AddTable(
            (dm) => dm.Name == "majales",
            new()
            {
                TableName = "ukoly",
                Head = new()
                {
                    Separator = ";",
                    LineLimit = 100,
                    MaxId = 0,
                    StartPage = 0,
                    Entity = new TableEntity(
                        new Attr("zadavatel", typeof(int)),
                        new Attr("radani"),
                        new Attr("zadano", typeof(DateTime)),
                        new Attr("uzaverka", typeof(DateTime)),
                        new Attr("priorita", typeof(int)),
                        new Attr("stav")
                    )
                }
            }
        );

        dtb.AddTable(
            (dm) => dm.Name == "majales",
            new()
            {
                TableName = "komentare",
                Head = new()
                {
                    Separator = ";",
                    LineLimit = 100,
                    MaxId = 0,
                    StartPage = 0,
                    Entity = new TableEntity(
                        new Attr("autor", typeof(int)),
                        new Attr("datum", typeof(DateTime)),
                        new Attr("text")
                    )
                }
            }
        );

        dtb.AddTable(
            (dm) => dm.Name == "majales",
            new()
            {
                TableName = "tym",
                Head = new()
                {
                    Separator = ";",
                    LineLimit = 100,
                    MaxId = 0,
                    StartPage = 0,
                    Entity = new TableEntity(
                        new Attr("id_clovek", typeof(int)),
                        new Attr("id_sekce", typeof(int))
                    )
                }
            }
        );

        dtb.AddTable(
            (dm) => dm.Name == "majales",
            new()
            {
                TableName = "staziste",
                Head = new()
                {
                    Separator = ";",
                    LineLimit = 100,
                    MaxId = 0,
                    StartPage = 0,
                    Entity = new TableEntity(
                        new Attr("id_stazista", typeof(int)),
                        new Attr("id_vedouci", typeof(int))
                    )
                }
            }
        );

        dtb.AddTable(
            (dm) => dm.Name == "majales",
            new()
            {
                TableName = "prace",
                Head = new()
                {
                    Separator = ";",
                    LineLimit = 100,
                    MaxId = 0,
                    StartPage = 0,
                    Entity = new TableEntity(
                        new Attr("id_clovek", typeof(int)),
                        new Attr("id_projekt", typeof(int))
                    )
                }
            }
        );

        dtb.AddTable(
            (dm) => dm.Name == "majales",
            new()
            {
                TableName = "program",
                Head = new()
                {
                    Separator = ";",
                    LineLimit = 100,
                    MaxId = 0,
                    StartPage = 0,
                    Entity = new TableEntity(
                        new Attr("id_projekt", typeof(int)),
                        new Attr("id_akce", typeof(int))
                    )
                }
            }
        );

        dtb.AddTable(
            (dm) => dm.Name == "majales",
            new()
            {
                TableName = "zadani",
                Head = new()
                {
                    Separator = ";",
                    LineLimit = 100,
                    MaxId = 0,
                    StartPage = 0,
                    Entity = new TableEntity(
                        new Attr("id_ukol", typeof(int)),
                        new Attr("id_pracant", typeof(int))
                    )
                }
            }
        );

        dtb.AddTable(
            (dm) => dm.Name == "majales",
            new()
            {
                TableName = "poznamkyProjektu",
                Head = new()
                {
                    Separator = ";",
                    LineLimit = 100,
                    MaxId = 0,
                    StartPage = 0,
                    Entity = new TableEntity(
                        new Attr("id_projekt", typeof(int)),
                        new Attr("id_komentar", typeof(int))
                    )
                }
            }
        );

        dtb.AddTable(
            (dm) => dm.Name == "majales",
            new()
            {
                TableName = "poznamkyAkce",
                Head = new()
                {
                    Separator = ";",
                    LineLimit = 100,
                    MaxId = 0,
                    StartPage = 0,
                    Entity = new TableEntity(
                        new Attr("id_akce", typeof(int)),
                        new Attr("id_komentar", typeof(int))
                    )
                }
            }
        );

        dtb.AddTable(
            (dm) => dm.Name == "majales",
            new()
            {
                TableName = "poznamkyUkolu",
                Head = new()
                {
                    Separator = ";",
                    LineLimit = 100,
                    MaxId = 0,
                    StartPage = 0,
                    Entity = new TableEntity(
                        new Attr("id_ukol", typeof(int)),
                        new Attr("id_komentar", typeof(int))
                    )
                }
            }
        );
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
