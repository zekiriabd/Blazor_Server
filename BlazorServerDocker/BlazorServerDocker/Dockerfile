# Étape 1 : Image de base ASP.NET pour l'exécution
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 8089
EXPOSE 443

# Étape 2 : Image SDK pour la compilation
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src
COPY BlazorServerDocker.csproj ./
RUN dotnet restore

# Étape 3 : Publication
COPY . ./
RUN dotnet publish -c Release -o /app/publish /p:UseAppHost=false

# Étape 4 : Image finale pour exécuter l'application
FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "BlazorServerDocker.dll"]
