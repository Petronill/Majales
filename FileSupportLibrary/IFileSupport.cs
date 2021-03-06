using DatabaseDefinitions;
using MiscLibrary.Sanitizing;

namespace FileSupportLibrary;

public interface IFileSupport : IInputSanitizer<string>
{
	public abstract string Workspace { get; }

	public abstract bool ExistsFile(string filename);

	public abstract bool ExistsDirectory(string name);

	public abstract bool ExistsTable(string name);

	public abstract bool ExistsPage(string tableName, int page);

	public abstract string[] AllTables();

	public abstract int AllPages(TableMeta tableMeta);

	public abstract bool GetInfo(string tableName, out TableHead head);

	public abstract bool UpdateInfo(TableMeta tableMeta);

	public abstract bool CreateTable(TableMeta tableMeta);

	public abstract bool DeleteTable(string tableName);

	public abstract int DeleteTables(string[] tableNames);

	public abstract bool GetLines(string tableName, int page, out string[] lines);

	public abstract bool GetPageLines(string tableName, int page, out string[] lines);

	public abstract bool IsPageEmpty(string tableName, int page);

	public abstract int AllLines(string tableName, int page);

	public abstract bool AppendLine(string tableName, int page, string line);

	public abstract bool UpdateLine(string tableName, int page, string line, int number);

	public abstract bool DeleteLine(string tableName, int page, int number);

	public abstract int AppendLines(string tableName, int page, string[] lines);

	public abstract int WriteLines(string tableName, int page, string[] lines);

	public abstract int DeleteLines(string tableName, int page, int[] numbers);

	public abstract bool DeletePage(string tableName, int page);

	public abstract int DeletePages(string tableName, int[] pages);

	public abstract bool DeletePageIfEmpty(string tableName, int page);

	public abstract int DeletePagesIfEmpty(string tableName, int[] pages);

	public abstract int DeleteAllEmptyPages(TableMeta tableMeta);
}
