namespace MiscLibrary.Sanitizing;

public class UnsanitizedInputInDomainException<T> : UnsanitizedInputException
{
    public T Domain { get; private set; }

    public UnsanitizedInputInDomainException(T domain)
    {
        Domain = domain;
    }

    public UnsanitizedInputInDomainException(T domain, string? message) : base(message)
    {
        Domain = domain;
    }

    public UnsanitizedInputInDomainException(T domain, string? message, Exception? innerException) : base(message, innerException)
    {
        Domain = domain;
    }
}
