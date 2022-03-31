namespace LogicalDatabaseLibrary.Attrs;

public interface IAttr
{
	public abstract string GetName();

	public abstract Type GetAttrType();

	public abstract bool Check(object? o);
	
	public abstract string? ToString(object? o);

	public abstract object? FromString(string str);
}
