#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER root
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Server/Travels.API/Travels.API.csproj", "Server/Travels.API/"]
COPY ["Server/Travels.Application/Travels.Application.csproj", "Server/Travels.Application/"]
COPY ["Server/Travels.Domain/Travels.Domain.csproj", "Server/Travels.Domain/"]
COPY ["Server/Travels.Infrastructure/Travels.Infrastructure.csproj", "Server/Travels.Infrastructure/"]
RUN dotnet restore "./Server/Travels.API/Travels.API.csproj"
COPY . .
WORKDIR "/src/Server/Travels.API"
RUN dotnet build "./Travels.API.csproj" -c %BUILD_CONFIGURATION% -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Travels.API.csproj" -c %BUILD_CONFIGURATION% -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Travels.API.dll"]