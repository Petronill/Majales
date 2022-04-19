namespace MiscLibrary.Sanitizing;

public class UnsanitizedInputException : Exception
{
    public UnsanitizedInputException()
    {
    }

    public UnsanitizedInputException(string? message) : base(message)
    {
    }

    public UnsanitizedInputException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}