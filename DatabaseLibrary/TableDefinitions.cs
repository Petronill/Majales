using FileSupportLibrary;

namespace DatabaseLibrary;

public delegate T TableProvider<T>(string name, IFileSupport fileSupporter) where T : Table;
public delegate bool TableSelector(Table table);