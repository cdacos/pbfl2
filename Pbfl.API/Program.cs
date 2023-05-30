using Microsoft.EntityFrameworkCore;
using Pbfl.API.Helpers;
using Pbfl.Data;
using Pbfl.Data.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(builder.GetDbContextOptions());
    
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// https://learn.microsoft.com/en-us/aspnet/core/fundamentals/minimal-apis/openapi

app.MapGet("/leagues", async (AppDbContext db) => await db.Leagues.ToListAsync());
app.MapGet("/leagues/{id:int}", async (AppDbContext db, int id) => await db.Leagues.FindAsync(id));
app.MapPost("/leagues", async (AppDbContext db, League league) =>
{
    await db.Leagues.AddAsync(league);
    await db.SaveChangesAsync();
    return Results.Created($"/leagues/{league.LeagueId}", league);
});
app.MapPut("/leagues/{id:int}", async (AppDbContext db, League updatedLeague, int id) =>
{
    var league = await db.Leagues.FindAsync(id);
    if (league is null) return Results.NotFound();
    league.Name = updatedLeague.Name;
    league.Description = updatedLeague.Description;
    await db.SaveChangesAsync();
    return Results.NoContent();
});
app.MapDelete("/leagues/{id:int}", async (AppDbContext db, int id) =>
{
    var league = await db.Leagues.FindAsync(id);
    if (league is null)
    {
        return Results.NotFound();
    }
    db.Leagues.Remove(league);
    await db.SaveChangesAsync();
    return Results.Ok();
});

app.MapGet("/", () => new string[] { "Hello", "World!" });

app.MapGet("/health", async (AppDbContext db) =>
{
    var leagueCount = await db.Leagues.CountAsync();
    return leagueCount > 0 ? Results.Ok(leagueCount) : Results.StatusCode(StatusCodes.Status503ServiceUnavailable);
});

app.MapGet("/swag", () => "Hello Swagger!")
    .WithOpenApi();

app.MapGet("/skipme", () => "Skipping Swagger.")
    .ExcludeFromDescription();

app.MapGet("/obsolete", () => "Some obsolete endpoint")
    .WithOpenApi(operation => new(operation)
    {
        Deprecated = true
    });

app.Run();