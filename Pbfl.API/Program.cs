var builder = WebApplication.CreateBuilder(args);

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

app.MapGet("/", () => new string[] { "Hello", "World!" });

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