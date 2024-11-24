# Étape 1: Utiliser l'image de base ASP.NET
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 8089
EXPOSE 443

# Étape 2: Restaurer les dépendances
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src
COPY BlazorServerDocker.csproj ./ 
RUN dotnet restore

# Étape 3: Publier l'application
COPY . ./
RUN dotnet publish -c Release -o /app/publish /p:UseAppHost=false

# Étape 4: Construire l'image finale
FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .  # Utiliser "build" ici, pas "publish"
ENTRYPOINT ["dotnet", "BlazorServerDocker.dll"]
