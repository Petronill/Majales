namespace MiscLibrary.Sanitizing;

public interface IInputSanitizer<T> : IInputSanitizable<T>
{
    public abstract T Sanitize(T input);
}

public interface IInputSanitizer<D, T> : IInputSanitizable<D, T>
{
    public abstract T Sanitize(D domain, T input);
}
