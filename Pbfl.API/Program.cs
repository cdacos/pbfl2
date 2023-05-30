using Microsoft.EntityFrameworkCore;
using Pbfl.API.Endpoints;
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

app.MapEntityEndpoints<Error>("/errors");
app.MapEntityEndpoints<League>("/leagues");
app.MapEntityEndpoints<Login>("/logins");
app.MapEntityEndpoints<Team>("/teams");

// Examples:

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