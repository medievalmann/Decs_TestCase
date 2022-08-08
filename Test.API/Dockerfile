#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Test.API/Test.API.csproj", "Test.API/"]
COPY ["ConfigurationManager.Reader/ConfigurationManager.Reader.csproj", "ConfigurationManager.Reader/"]
COPY ["ConfigurationManager.Data/ConfigurationManager.Data.csproj", "ConfigurationManager.Data/"]
COPY ["ConfigurationManager.Core/ConfigurationManager.Core.csproj", "ConfigurationManager.Core/"]
COPY ["CacheManager.Infrastructere/CacheManager.Infrastructere.csproj", "CacheManager.Infrastructere/"]
COPY ["CacheManager.Domain/CacheManager.Domain.csproj", "CacheManager.Domain/"]
COPY ["ConfigurationManager.Services/ConfigurationManager.Services.csproj", "ConfigurationManager.Services/"]
RUN dotnet restore "Test.API/Test.API.csproj"
COPY . .
WORKDIR "/src/Test.API"
RUN dotnet build "Test.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Test.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Test.API.dll"]
