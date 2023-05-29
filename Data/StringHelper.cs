using System.Reflection;

namespace Pbfl.Data;

public static class StringHelper
{
    public static string GetString<T>(T obj)
    {
        var props = typeof(T).GetProperties()
            .Where(p => !p.GetMethod?.IsVirtual ?? false);
        return $"{typeof(T).Name} {{ {GetPropertyAsString(props, obj!)} }}";
    }

    private static string GetPropertyAsString(IEnumerable<PropertyInfo> props, object obj)
    {
        return string.Join(", ", props.Select(p => $"{p.Name} = {p.GetValue(obj)}"));
    }
}