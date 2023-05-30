using System.Reflection;

namespace Pbfl.Data.Helpers;

public static class DbContextHelper
{
    public static IQueryable<object>? Set(this AppDbContext context, Type entityType)
    {
        var property = typeof(AppDbContext)
            .GetProperties().FirstOrDefault(p => p.PropertyType.GenericTypeArguments.FirstOrDefault()?.Equals(entityType) ?? false);
        
        return property != null ? (IQueryable<object>?)property.GetValue(context) : null;
    }

    public static IQueryable<object>? Set(this AppDbContext context, string entityType)
    {
        var t = context.GetTypeFromName(entityType);
        return context.Set(t!);
    }

    public static Type? GetTypeFromName(this AppDbContext context, string entityType)
    {
        var assembly = Assembly.GetAssembly(typeof(DbContextHelper))!;
        return assembly.GetType("Pbfl.Pbfl.Data.Models." + entityType);
    }
    
    public static int GetPrimaryKeyValue<T>(this AppDbContext context, T entity)
    {
        var keyName = context.Model.FindEntityType(typeof (T)).FindPrimaryKey().Properties
            .Select(x => x.Name).Single();

        return (int)entity.GetType().GetProperty(keyName).GetValue(entity, null);
    }
}