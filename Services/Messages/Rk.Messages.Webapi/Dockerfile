#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM docker-proxy.dev.roscosmos.digital/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM docker-proxy.dev.roscosmos.digital/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Services/Messages/Rk.Messages.Webapi/Rk.Messages.Webapi.csproj", "Services/Messages/Rk.Messages.Webapi/"]
COPY ["Services/Messages/Rk.Messages.Logic/Rk.Messages.Logic.csproj", "Services/Messages/Rk.Messages.Logic/"]
COPY ["Rk.Messages.Common/Rk.Messages.Common.csproj", "Rk.Messages.Common/"]
COPY ["Services/Messages/Rk.Messages.Infrastructure/Rk.Messages.Infrastructure.csproj", "Services/Messages/Rk.Messages.Infrastructure/"]
COPY ["Services/Messages/Rk.Messages.Interfaces/Rk.Messages.Interfaces.csproj", "Services/Messages/Rk.Messages.Interfaces/"]
COPY ["Services/Messages/Rk.Messages.Domain/Rk.Messages.Domain.csproj", "Services/Messages/Rk.Messages.Domain/"]
RUN dotnet restore "Services/Messages/Rk.Messages.Webapi/Rk.Messages.Webapi.csproj"
COPY . .
WORKDIR "/src/Services/Messages/Rk.Messages.Webapi"
RUN dotnet build "Rk.Messages.Webapi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Rk.Messages.Webapi.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Rk.Messages.Webapi.dll"]