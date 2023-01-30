FROM docker-proxy.dev.roscosmos.digital/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM docker-proxy.dev.roscosmos.digital/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Services/Statistic/Rk.Statistic.WebApi/Rk.Statistic.WebApi.csproj", "Services/Statistic/Rk.Statistic.WebApi/"]
COPY ["Services/Statistic/Rk.Statistic.Logic/Rk.Statistic.Logic.csproj", "Services/Statistic/Rk.Statistic.Logic/"]
COPY ["Rk.Messages.Common/Rk.Messages.Common.csproj", "Rk.Messages.Common/"]
COPY ["Services/Statistic/Rk.Statistic.Infrastructure/Rk.Statistic.Infrastructure.csproj", "Services/Statistic/Rk.Statistic.Infrastructure/"]
COPY ["Services/Statistic/Rk.Statistic.Interfaces/Rk.Statistic.Interfaces.csproj", "Services/Statistic/Rk.Statistic.Interfaces/"]
RUN dotnet restore "Services/Statistic/Rk.Statistic.WebApi/Rk.Statistic.WebApi.csproj"
COPY . .
WORKDIR "/src/Services/Statistic/Rk.Statistic.WebApi"
RUN dotnet build "Rk.Statistic.WebApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Rk.Statistic.WebApi.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Rk.Statistic.WebApi.dll"]

