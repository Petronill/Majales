using DatabaseDefinitions;
using LogicalDatabaseLibrary;
using DatabaseLibrary;
using MiscLibrary;
using MiscLibrary.Sanitizing;

namespace ClientLibrary;

public interface IDatabaseManager : IInputSanitizable<Domain<Table>, TableLine>
{
	public abstract int Contains(DatabaseSelector dtbsel);
	public abstract int Contains(DatabaseSelector dtbsel, TableSelector tblsel);
	public abstract int Contains(DatabaseSelector dtbsel, TableSelector tblsel, RowSelector rowsel);
	public abstract bool OpenDatabase(DatabaseMeta dtbmeta);
	public abstract int CloseDatabase(DatabaseSelector dtbsel);
	public abstract bool NewDatabase(DatabaseMeta dtbmeta);
	public abstract int DeleteDatabase(DatabaseSelector dtbsel);
	public abstract DatabaseMeta[] GetAllDatabases();
	public abstract DatabaseMeta[] GetDatabases(DatabaseSelector dtbsel);
	public abstract Table[] GetAllTables(DatabaseSelector dtbsel);
	public abstract Table[] GetDatabaseTables(DatabaseSelector dtbsel, TableSelector tblsel);
	public abstract int AddTable(DatabaseSelector dtbsel, TableMeta table);
	public abstract int DeleteTable(DatabaseSelector dtbsel, TableSelector tblsel);
	public abstract TableLine[] GetAllLines(DatabaseSelector dtbsel, TableSelector tblsel);
	public abstract int AddLine(DatabaseSelector dtbsel, TableSelector tblsel, TableLine line);
	public abstract int DeleteLine(DatabaseSelector dtbsel, TableSelector tblsel, RowSelector rowsel);
	public abstract int DeleteLine(DatabaseSelector dtbsel, TableSelector tblsel, int lineId);
	public abstract TableLine[] GetTableLines(DatabaseSelector dtbsel, TableSelector tblsel, RowSelector rowsel);
	public abstract TableLine? GetLine(string databaseName, string tableName, int lineId);
	public abstract int SetLine(DatabaseSelector dtbsel, TableSelector tblsel, TableLine line);
	public abstract int UpdateLine(DatabaseSelector dtbsel, TableSelector tblsel, TableLine line);
	public abstract T? GetAttr<T>(string databaseName, string tableName, int lineId, AttrSeparator<T> attrsep);
	public abstract int SetAttr(DatabaseSelector dtbsel, TableSelector tblsel, RowSelector rowsel, int attrIndex, object? value);
	public abstract int SetAttr(DatabaseSelector dtbsel, TableSelector tblsel, int lineId, int attrIndex, object? value);
	public abstract int SetAttr(DatabaseSelector dtbsel, TableSelector tblsel, RowSelector rowsel, string attrName, object? value);
	public abstract int SetAttr(DatabaseSelector dtbsel, TableSelector tblsel, int lineId, string attrName, object? value);
}
