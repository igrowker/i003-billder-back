#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Billder.API/Billder.API.csproj", "Billder.API/"]
COPY ["Billder.Application/Billder.Application.csproj", "Billder.Application/"]
COPY ["Billder.Infrastructure/Billder.Infrastructure.csproj", "Billder.Infrastructure/"]
RUN dotnet restore "./Billder.API/Billder.API.csproj"
COPY . .
WORKDIR "/src/Billder.API"
RUN dotnet build "./Billder.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Billder.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Asegúrate de que appsettings.json y appsettings.Development.json estén incluidos en la publicación
COPY ["Billder.API/appsettings.json", "/app/publish/"]
COPY ["Billder.API/appsettings.Development.json", "/app/publish/"]

FROM base AS final
WORKDIR /app

# Configurar el entorno de desarrollo
ENV ASPNETCORE_ENVIRONMENT=Development

COPY --from=publish /app/publish . 
ENTRYPOINT ["dotnet", "Billder.API.dll"]
