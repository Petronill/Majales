namespace MiscLibrary.Sanitizing;

public class UnsanitizedInputInDomainException<D> : UnsanitizedInputException
{
    public D Domain { get; private set; }

    public UnsanitizedInputInDomainException(D domain)
    {
        Domain = domain;
    }

    public UnsanitizedInputInDomainException(D domain, string? message) : base(message)
    {
        Domain = domain;
    }

    public UnsanitizedInputInDomainException(D domain, string? message, Exception? innerException) : base(message, innerException)
    {
        Domain = domain;
    }
}
