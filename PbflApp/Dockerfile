FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["PbflApp/PbflApp.csproj", "PbflApp/"]
RUN dotnet restore "PbflApp/PbflApp.csproj"
COPY . .
WORKDIR "/src/PbflApp"
RUN dotnet build "PbflApp.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "PbflApp.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PbflApp.dll"]
