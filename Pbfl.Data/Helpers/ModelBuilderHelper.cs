using Microsoft.EntityFrameworkCore;

namespace Pbfl.Data.Helpers;

public static class ModelBuilderHelper
{
    public static void RemovePluralizingTableNameConvention(this ModelBuilder modelBuilder)
    {
        // Override naming the table from the DbSet property name
        // Instead, always use the entity type name
        foreach (var entityType in modelBuilder.Model.GetEntityTypes())
        {
            var conventionName = entityType.GetTableName();
            var entityTypeShortName = entityType.ShortName();
            if (conventionName != entityTypeShortName)
            {
                entityType.SetTableName(entityTypeShortName);
            }
        }
    }
}