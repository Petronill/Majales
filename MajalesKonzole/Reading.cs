namespace Fei.BaseLib;


/// <summary>
/// This class allows for easier reading of basic user input.
/// </summary>
public class Reading
{
    /*
     * For implementation use WriteLine and ReadLine instead of direct use of Console.WriteLine/Console.ReadLine
     * WriteLine(prompt);
     * ... = ReadLine();
     */
    public static Action<string?> WriteLine = Console.WriteLine;
    public static Func<string?> ReadLine = Console.ReadLine;

    /// <summary>
    /// Reads an int from the user input.
    /// </summary>
    /// <param name="prompt">The prompt to be shown before user input.</param>
    /// <returns>The read int.</returns>
    public static int ReadInt(string prompt)
    {
        int res;
        do {
            WriteLine(prompt + ": ");
        } while (!int.TryParse((ReadLine() ?? "").Trim(), out res));
        return res;
    }


    /// <summary>
    /// Reads a double from the user input.
    /// </summary>
    /// <param name="prompt">The prompt to be shown before user input.</param>
    /// <returns>The read double.</returns>
    public static double ReadDouble(string prompt)
    {
        double res;
        do {
            WriteLine(prompt + ": ");
        } while (!double.TryParse((ReadLine() ?? "").Trim(), out res));
        return res;
    }


    /// <summary>
    /// Tries to get the (first) char of provided string.
    /// </summary>
    /// <param name="s">The string to be parsed.</param>
    /// <param name="result">The resulting parsed character.</param>
    /// <returns>Whether or not was the parsing successful.</returns>
    private static bool TryParseChar(string s, out char result)
    {
        if ((s == null) || (s.Length < 1)) {
            result = '\0';
            return false;
        }
        result = s[0];
        return true;
    }


    /// <summary>
    /// Reads a char from the user input.
    /// </summary>
    /// <param name="prompt">The prompt to be shown before user input.</param>
    /// <returns>The read char.</returns>
    public static char ReadChar(string prompt)
    {
        char res;
        do {
            WriteLine(prompt + ": ");
        } while (!TryParseChar((ReadLine() ?? ""), out res));
        return res;
    }


    /// <summary>
    /// Reads a string from the user input.
    /// </summary>
    /// <param name="prompt">The prompt to be shown before user input.</param>
    /// <returns>The read string.</returns>
    public static string ReadString(string prompt)
    {
        WriteLine(prompt + ": ");
        return (ReadLine() ?? "");
    }
}
