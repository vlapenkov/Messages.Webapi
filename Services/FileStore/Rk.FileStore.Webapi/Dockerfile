#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM docker-proxy.dev.roscosmos.digital/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM docker-proxy.dev.roscosmos.digital/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Services/FileStore/Rk.FileStore.Webapi/Rk.FileStore.Webapi.csproj", "Services/FileStore/Rk.FileStore.Webapi/"]
COPY ["Rk.Messages.Common/Rk.Messages.Common.csproj", "Rk.Messages.Common/"]
COPY ["Services/FileStore/Rk.FileStore.Infrastructure/Rk.FileStore.Infrastructure.csproj", "Services/FileStore/Rk.FileStore.Infrastructure/"]
COPY ["Services/FileStore/Rk.FileStore.Logic/Rk.FileStore.Logic.csproj", "Services/FileStore/Rk.FileStore.Logic/"]
COPY ["Services/FileStore/Rk.FileStore.Interfaces/Rk.FileStore.Interfaces.csproj", "Services/FileStore/Rk.FileStore.Interfaces/"]
COPY ["Services/FileStore/Rk.FileStore.Domain/Rk.FileStore.Domain.csproj", "Services/FileStore/Rk.FileStore.Domain/"]
RUN dotnet restore "Services/FileStore/Rk.FileStore.Webapi/Rk.FileStore.Webapi.csproj"
COPY . .
WORKDIR "/src/Services/FileStore/Rk.FileStore.Webapi"
RUN dotnet build "Rk.FileStore.Webapi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Rk.FileStore.Webapi.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Rk.FileStore.Webapi.dll"]