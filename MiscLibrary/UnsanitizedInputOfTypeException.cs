namespace MiscLibrary.Sanitizing;

public class UnsanitizedInputOfTypeException : UnsanitizedInputException
{
    public Type Type { get; private set; }

    public UnsanitizedInputOfTypeException(Type type)
    {
        Type = type;
    }

    public UnsanitizedInputOfTypeException(Type type, string? message) : base(message)
    {
        Type = type;
    }

    public UnsanitizedInputOfTypeException(Type type, string? message, Exception? innerException) : base(message, innerException)
    {
        Type = type;
    }
}
