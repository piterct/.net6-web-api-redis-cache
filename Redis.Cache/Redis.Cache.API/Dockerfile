#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Redis.Cache.API/Redis.Cache.API.csproj", "Redis.Cache.API/"]
COPY ["Redis.Cache.Application/Redis.Cache.Application.csproj", "Redis.Cache.Application/"]
COPY ["Redis.Cache.Infra/Redis.Cache.Infra.csproj", "Redis.Cache.Infra/"]
RUN dotnet restore "Redis.Cache.API/Redis.Cache.API.csproj"
COPY . .
WORKDIR "/src/Redis.Cache.API"
RUN dotnet build "Redis.Cache.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Redis.Cache.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

# Copy the built application from the build environment
COPY --from=build-env /app/out .

ENTRYPOINT ["dotnet", "Redis.Cache.API.dll"]