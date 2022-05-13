namespace MiscLibrary.Sanitizing;

public class UnsanitizedInputOfTypeInDomainException<T> : UnsanitizedInputException
{
    public T Domain { get; private set; }
    public Type Type { get; private set; }

    public UnsanitizedInputOfTypeInDomainException(T domain, Type type)
    {
        Domain = domain;
        Type = type;
    }

    public UnsanitizedInputOfTypeInDomainException(T domain, Type type, string? message) : base(message)
    {
        Domain = domain;
        Type = type;
    }

    public UnsanitizedInputOfTypeInDomainException(T domain, Type type, string? message, Exception? innerException) : base(message, innerException)
    {
        Domain = domain;
        Type = type;
    }
}
