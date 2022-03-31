using DatabaseLibrary;
using DatabaseDefinitions;
using DatabaseLibrary.Tables;
using FileSupportLibrary;
using Fei.BaseLib;

class Program
{
    static string workspace = "D:\\Majales";
    static TableHead defaultTableHead = new TableHead { Separator = ";", LineLimit = 10, StartPage = 0};
    static IFileSupport fileSupporter = new FileSupporter(workspace);
    static DatabaseIndex<BufferedTable> dtb = new(fileSupporter, (name, fs) => new BufferedTable(name, fs));

    public static void WriteOptions()
    {
        Console.WriteLine("Nabídka:");
        Console.WriteLine("1. Výpis všech tabulek");
        Console.WriteLine("2. Vytvoření nové tabulky");
        Console.WriteLine("3. Výpis tabulky");
        Console.WriteLine("4. Smazat tabulku");
        Console.WriteLine("5. Přidat řádek");
        Console.WriteLine("6. Přepsat řádek");
        Console.WriteLine("7. Smazat řádek");
        Console.WriteLine("[Konec]");
    }

    public static bool ReadOption()
    {
        switch (Reading.ReadInt("Zvolte si"))
        {
            case 1:
                WriteAllTables();
                return true;
            case 2:
                NewTable();
                return true;
            case 3:
                WriteTable();
                return true;
            case 4:
                DeleteTable();
                return true;
            case 5:
                AddLine();
                return true;
            case 6:
                UpdateLine();
                return true;
            case 7:
                DeleteLine();
                return true;
            default:
                return false;
        }
    }

    private static void WriteAllTables()
    {
        Console.WriteLine("Všechny tabulky v databázi:");
        Console.WriteLine(string.Join('\n', fileSupporter.AllTables()));
    }

    private static void NewTable()
    {
        string name = Reading.ReadString("Zadejte jméno nové tabulky");
        if (dtb.Create(name, defaultTableHead) != null)
        {
            Console.WriteLine($"Tabulka {name} vytvořena."); 
        }
        else
        {
            Console.WriteLine($"Tabulka {name} nebyla vytvořena.");
        }
    }

    private static void WriteTable()
    {
        string name = Reading.ReadString("Zadejte jméno nové tabulky");
        BufferedTable? t = dtb[name];
        if (t != null)
        {
            Console.WriteLine($"Tabulka {name}:");
            Console.WriteLine(t.ToString());
        }
        else
        {
            Console.WriteLine("Došlo k chybě.");
        }
    }

    private static void DeleteTable()
    {
        string name = Reading.ReadString("Zadejte jméno nové tabulky");
        BufferedTable? t = dtb.Delete(name);
        if (t != null)
        {
            Console.WriteLine($"Tabulka {name} smazána.");
        }
        else
        {
            Console.WriteLine("Došlo k chybě.");
        }
    }

    private static void AddLine()
    {
        string name = Reading.ReadString("Zadejte jméno tabulky");
        BufferedTable? t = dtb[name];
        if (t != null)
        {
            t.Add(Reading.ReadString("Zadejte nový řádek"));
        }
        else
        {
            Console.WriteLine("Tabulka nenalezena.");
        }
    }

    private static void UpdateLine()
    {
        string name = Reading.ReadString("Zadejte jméno tabulky");
        BufferedTable? t = dtb[name];
        if (t != null)
        {
            string line = Reading.ReadString("Zadejte nový řádek");
            t[LineFormat.GetId(line)] = line;
        }
        else
        {
            Console.WriteLine("Tabulka nenalezena.");
        }
    }

    private static void DeleteLine()
    {
        string name = Reading.ReadString("Zadejte jméno tabulky");
        BufferedTable? t = dtb[name];
        if (t != null)
        {
            t.Remove(Reading.ReadInt("Zadejte id řádky ke smazání"));
        }
        else
        {
            Console.WriteLine("Tabulka nenalezena.");
        }
    }
    
    public static void Main()
    {
        Console.WriteLine("Vítejte v organizačním nástroji Kolínského majálesu.");
        do
        {
            WriteOptions();
        } while (ReadOption());
        Console.WriteLine("Konec programu");
    }
}
