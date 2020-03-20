#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["TestApp.API/TestApp.API.csproj", "TestApp.API/"]
COPY ["TestApp.Services/TestApp.Services.csproj", "TestApp.Services/"]
COPY ["TestApp.BelTMemcached/TestApp.BelTMemcached.csproj", "TestApp.BelTMemcached/"]
COPY ["TestApp.Models/TestApp.Models.csproj", "TestApp.Models/"]
RUN dotnet restore "TestApp.API/TestApp.API.csproj"
COPY . .
WORKDIR "/src/TestApp.API"
RUN dotnet build "TestApp.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TestApp.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TestApp.API.dll"]