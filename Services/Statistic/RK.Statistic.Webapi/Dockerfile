FROM docker-proxy.dev.roscosmos.digital/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM docker-proxy.dev.roscosmos.digital/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Services/Statistic/RK.Statistic.Webapi/RK.Statistic.Webapi.csproj", "Services/Statistic/RK.Statistic.Webapi/"]
COPY ["Services/Statistic/RK.Statistic.Logic/RK.Statistic.Logic.csproj", "Services/Statistic/RK.Statistic.Logic/"]
COPY ["Rk.Messages.Common/Rk.Messages.Common.csproj", "Rk.Messages.Common/"]
COPY ["Services/Statistic/RK.Statistic.Infrastructure/RK.Statistic.Infrastructure.csproj", "Services/Statistic/RK.Statistic.Infrastructure/"]
COPY ["Services/Statistic/RK.Statistic.Interfaces/RK.Statistic.Interfaces.csproj", "Services/Statistic/RK.Statistic.Interfaces/"]
RUN dotnet restore "Services/Statistic/RK.Statistic.Webapi/RK.Statistic.Webapi.csproj"
COPY . .
WORKDIR "/src/Services/Statistic/RK.Statistic.Webapi"
RUN dotnet build "RK.Statistic.Webapi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "RK.Statistic.Webapi.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "RK.Statistic.Webapi.dll"]
