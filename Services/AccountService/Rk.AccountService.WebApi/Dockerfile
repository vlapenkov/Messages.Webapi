#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM docker-proxy.dev.roscosmos.digital/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM docker-proxy.dev.roscosmos.digital/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Services/AccountService/Rk.AccountService.WebApi/Rk.AccountService.WebApi.csproj", "Services/AccountService/Rk.AccountService.WebApi/"]
COPY ["Services/AccountService/Rk.AccountService.Logic/Rk.AccountService.Logic.csproj", "Services/AccountService/Rk.AccountService.Logic/"]
COPY ["Rk.Messages.Common/Rk.Messages.Common.csproj", "Rk.Messages.Common/"]
COPY ["Services/AccountService/Rk.AccountService.Infrastructure/Rk.AccountService.Infrastructure.csproj", "Services/AccountService/Rk.AccountService.Infrastructure/"]
COPY ["Services/AccountService/Rk.AccountService.Interfaces/Rk.AccountService.Interfaces.csproj", "Services/AccountService/Rk.AccountService.Interfaces/"]
RUN dotnet restore "Services/AccountService/Rk.AccountService.WebApi/Rk.AccountService.WebApi.csproj"
COPY . .
WORKDIR "/src/Services/AccountService/Rk.AccountService.WebApi"
RUN dotnet build "Rk.AccountService.WebApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Rk.AccountService.WebApi.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Rk.AccountService.WebApi.dll"]