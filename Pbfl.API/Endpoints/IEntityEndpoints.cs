using Pbfl.Data;

namespace Pbfl.API.Endpoints;

public interface IEntityEndpoints<T> where T : class
{
    Task<IResult> CreateEntity(AppDbContext db, T entity);
    
    Task<List<T>> ReadEntity(AppDbContext db);

    Task<T?> ReadEntity(AppDbContext db, object id);

    Task<IResult> UpdateEntity(AppDbContext db, int id, T updatedEntity);

    Task<IResult> DeleteEntity(AppDbContext db, int id);
}