namespace ArrayUtilsLibrary;
public static class ArrayUtils
{
    public static void Append<T>(ref T[] arr, T elem)
    {
        Array.Resize(ref arr, arr.Length + 1);
        arr[arr.Length - 1] = elem;
    }

    public static void Delete<T>(ref T[] arr, int i)
    {
        for (int j = i; j < arr.Length;)
        {
            arr[j] = arr[j++];
        }
        Array.Resize(ref arr, arr.Length - 1);
    }
}