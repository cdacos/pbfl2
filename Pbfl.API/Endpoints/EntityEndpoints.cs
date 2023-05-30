using Microsoft.EntityFrameworkCore;
using Pbfl.API.Helpers;
using Pbfl.Data;
using Pbfl.Data.Helpers;

namespace Pbfl.API.Endpoints;

public class EntityEndpoints<T> : IEntityEndpoints<T> where T : class
{
    public virtual async Task<IResult> CreateEntity(AppDbContext db, T entity)
    {
        await db.Set<T>().AddAsync(entity);
        await db.SaveChangesAsync();
        return Results.Created($"/{db.GetPrimaryKeyValue(entity)}", entity);
    }

    public virtual async Task<List<T>> ReadEntity(AppDbContext db)
    {
        return await db.Set<T>().ToListAsync();
    }

    public virtual async Task<T?> ReadEntity(AppDbContext db, object id)
    {
        return await db.Set<T>().FindAsync((int)id);
    }
    
    public virtual async Task<IResult> UpdateEntity(AppDbContext db, int id, T updatedEntity)
    {
        var entity = await db.Set<T>().FindAsync(id);
        if (entity is null)
        {
            return Results.NotFound();
        }

        ObjectHelper.Clone(updatedEntity, entity);
        await db.SaveChangesAsync();
        return Results.NoContent();
    }

    public async Task<IResult> DeleteEntity(AppDbContext db, int id)
    {
        var entity = await db.Set<T>().FindAsync(id);
        if (entity is null)
        {
            return Results.NotFound();
        }

        db.Set<T>().Remove(entity);
        await db.SaveChangesAsync();
        return Results.Ok();
    }
}