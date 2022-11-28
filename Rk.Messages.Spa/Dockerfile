#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Rk.Messages.Spa/Rk.Messages.Spa.csproj", "Rk.Messages.Spa/"]
COPY ["Rk.Messages.Common/Rk.Messages.Common.csproj", "Rk.Messages.Common/"]
RUN dotnet restore "Rk.Messages.Spa/Rk.Messages.Spa.csproj"
COPY . .
WORKDIR "/src/Rk.Messages.Spa"
RUN dotnet build "Rk.Messages.Spa.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Rk.Messages.Spa.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Rk.Messages.Spa.dll"]