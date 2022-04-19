namespace MiscLibrary.Sanitizing;

public class UnsanitizedInputOfTypeInDomainException<D> : UnsanitizedInputException
{
    public D Domain { get; private set; }
    public Type Type { get; private set; }

    public UnsanitizedInputOfTypeInDomainException(D domain, Type type)
    {
        Domain = domain;
        Type = type;
    }

    public UnsanitizedInputOfTypeInDomainException(D domain, Type type, string? message) : base(message)
    {
        Domain = domain
        Type = type;
    }

    public UnsanitizedInputOfTypeInDomainException(D domain, Type type, string? message, Exception? innerException) : base(message, innerException)
    {
        Domain = domain
        Type = type;
    }
}
