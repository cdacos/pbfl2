using System.Diagnostics.CodeAnalysis;
using Pbfl.Data;

namespace Pbfl.API.Endpoints;

public static class EndpointHelper
{
    public static RouteGroupBuilder MapEntityEndpoints<T>(this WebApplication app, [StringSyntax("Route")] string prefix, IEntityEndpoints<T>? entityEndpointsManager = null) where T : class
    {
        entityEndpointsManager ??= new EntityEndpoints<T>();
        
        var builder = app.MapGroup(prefix).WithTags(typeof(T).Name);

        builder.MapGet("/", async (AppDbContext db) => await entityEndpointsManager.ReadEntity(db));

        builder.MapGet("/{id:int}", async (AppDbContext db, int id) => await entityEndpointsManager.ReadEntity(db, id));

        builder.MapPost("/", async (AppDbContext db, T entity) => await entityEndpointsManager.CreateEntity(db, entity));

        builder.MapPut("/{id:int}", async (AppDbContext db, T updatedEntity, int id) => await entityEndpointsManager.UpdateEntity(db, id, updatedEntity));

        builder.MapDelete("/{id:int}", async (AppDbContext db, int id) => await entityEndpointsManager.DeleteEntity(db, id));

        return builder;
    }
}