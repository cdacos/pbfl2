using System.Reflection;

namespace Pbfl.API.Helpers;

public static class ObjectHelper
{
    public static void Clone(object fromObject, object toObject)
    {
        const BindingFlags bindFlags = BindingFlags.Instance | BindingFlags.Public;

        var properties = fromObject.GetType().GetProperties(bindFlags);
        foreach (var fi in properties)
        {
            var fromProperty = fromObject.GetType().GetProperty(fi.Name, bindFlags);
            var toProperty = toObject.GetType().GetProperty(fi.Name, bindFlags);
            if (fromProperty != null && toProperty != null)
            {
                toProperty.SetValue(toObject, fromProperty.GetValue(fromObject));
            }
        }
    }
}