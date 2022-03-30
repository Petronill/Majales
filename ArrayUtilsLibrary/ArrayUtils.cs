namespace ArrayUtilsLibrary;
public static class ArrayUtils
{
    public static void Append<T>(ref T[] arr, T elem)
    {
        Array.Resize(ref arr, arr.Length + 1);
        arr[arr.Length - 1] = elem;
    }

    public static void Prepend<T>(ref T[] arr, T elem)
    {
        Array.Resize(ref arr, arr.Length + 1);
        Array.Copy(arr, 0, arr, 1, arr.Length - 1);
        arr[0] = elem;
    }

    public static void Delete<T>(ref T[] arr, int i)
    {
        if (i >= 0 && i < arr.Length)
        {
            if (i < arr.Length - 1)
            {
                Array.Copy(arr, i + 1, arr, i, arr.Length - i - 1);
            }
            Array.Resize(ref arr, arr.Length - 1);
        }
    }
}