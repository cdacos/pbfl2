# PBFL Website

## appSettings.*.json

### Database Connection

Looks for a key called `DatabaseProvider` and then picks the appropriate connection string:

Supported databases are:

- `Sqlite` (The default if no other is specified)
- `Postgres` (untested, and no migrations have been created)

For example:

```json
"DatabaseProvider": "Postgres",
"ConnectionStrings": {
    "Postgres": "Server=127.0.0.1;Port=5432;Database=pbfl;User Id=postgres;Password=blah",
    "Sqlite": "Data Source=pbfl.db"
}
```

Note: If no `DatabaseProvider` is provided, it falls back to `Sqlite`.
Then if no `Sqlite` connection string is provided, it falls back to `"Data Source=./Database/pbfl.db"`.
See [Source/PbflApp/Data/DbContextOptionsHelper.cs](Source/PbflApp/Data/DbContextOptionsHelper.cs).

### Logging

```json
  "Serilog": {
    "MinimumLevel": {
      "Default": "Error",
      "Override": {
        "Microsoft.EntityFrameworkCore": "Error"
      }
    }
  }
```

## EF migrations

See [Source/Tools/ef-migrations.sh](Source/Tools/ef-migrations.sh)

```
dotnet ef migrations add MyMigration --project ../Migrations/PostgresMigrations -- --DatabaseProvider Postgres
```

### Migration Remove Last Migration

```
dotnet ef migrations remove --project ../Migrations/PostgresMigrations -- --DatabaseProvider Postgres
```

### Rollback Database

```
dotnet ef database update SomeMigrationName
```

### EF Tools Upgrade

```
dotnet tool update --global dotnet-ef
```


### Using a Separate Migrations Project

https://learn.microsoft.com/en-us/ef/core/managing-schemas/migrations/projects?tabs=dotnet-core-cli


## Docker

```
docker build --no-cache -t up:local -f PbflApp/Dockerfile .
docker run -it --entrypoint sh up:local
docker run --publish 8080:80 up:local
```
