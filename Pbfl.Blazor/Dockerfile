FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Pbfl.Blazor/Pbfl.Blazor.csproj", "Pbfl.Blazor/"]
RUN dotnet restore "Pbfl.Blazor/Pbfl.Blazor.csproj"
COPY . .
WORKDIR "/src/Pbfl.Blazor"
RUN dotnet build "Pbfl.Blazor.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Pbfl.Blazor.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Pbfl.Blazor.dll"]
