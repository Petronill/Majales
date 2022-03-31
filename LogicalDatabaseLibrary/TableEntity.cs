using LogicalDatabaseLibrary.Attrs;
using ArrayUtilsLibrary;

namespace LogicalDatabaseLibrary;

public class TableEntity : Entity
{
    public TableEntity(params IAttr[] attributes) : base(attributes)
    {
        ArrayUtils.Prepend(ref attrs, new PrimaryAttr());
    }
}