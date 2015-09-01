using System.ComponentModel;

// ReSharper disable once CheckNamespace
namespace System.Extensions
{
    internal static class TypeExtensions
    {
        public static void CopyProperties<T>(this T src, T dest)
        {
            foreach (PropertyDescriptor item in TypeDescriptor.GetProperties(src))
            {
                item.SetValue(dest, item.GetValue(src));
            }
        }
    }
}
