# Consultez https://aka.ms/customizecontainer pour savoir comment personnaliser votre conteneur de débogage et comment Visual Studio utilise ce Dockerfile pour générer vos images afin d’accélérer le débogage.

# Cet index est utilisé lors de l’exécution à partir de VS en mode rapide (par défaut pour la configuration de débogage)
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 8089
EXPOSE 443


# Cette phase est utilisée pour générer le projet de service
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["BlazorServerDocker/BlazorServerDocker.csproj", "BlazorServerDocker/"]
RUN dotnet restore "./BlazorServerDocker/BlazorServerDocker.csproj"
COPY . .
WORKDIR "/src/BlazorServerDocker"
RUN dotnet build "./BlazorServerDocker.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Cette étape permet de publier le projet de service à copier dans la phase finale
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./BlazorServerDocker.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Cette phase est utilisée en production ou lors de l’exécution à partir de VS en mode normal (par défaut quand la configuration de débogage n’est pas utilisée)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BlazorServerDocker.dll"]