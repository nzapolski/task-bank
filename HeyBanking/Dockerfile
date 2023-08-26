#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["HeyBanking/HeyBanking.API.csproj", "HeyBanking/"]
COPY ["HeyBanking.Data/HeyBanking.App.csproj", "HeyBanking.Data/"]
COPY ["HeyBanking.Domain/HeyBanking.Domain.csproj", "HeyBanking.Domain/"]
COPY ["HeyBanking.Infrastructure/HeyBanking.Infrastructure.csproj", "HeyBanking.Infrastructure/"]
RUN dotnet restore "HeyBanking/HeyBanking.API.csproj"
COPY . .
WORKDIR "/src/HeyBanking"
RUN dotnet build "HeyBanking.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "HeyBanking.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "HeyBanking.API.dll"]