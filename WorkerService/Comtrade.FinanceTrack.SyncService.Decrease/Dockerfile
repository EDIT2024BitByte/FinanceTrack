
FROM mcr.microsoft.com/dotnet/runtime:8.0 AS base
USER app
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY . /src

WORKDIR /src
RUN ls WorkerService/Comtrade.FinanceTrack.SyncService.Decrease
RUN dotnet restore WorkerService/Comtrade.FinanceTrack.SyncService.Decrease/Comtrade.FinanceTrack.SyncService.Desrease.csproj
COPY . .
WORKDIR /src
#WORKDIR /WorkerService/Comtrade.FinanceTrack.SyncService.Desrease
RUN dotnet build WorkerService/Comtrade.FinanceTrack.SyncService.Decrease/Comtrade.FinanceTrack.SyncService.Desrease.csproj -c Debug -o /app/build #-c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
WORKDIR /src
RUN dotnet publish WorkerService/Comtrade.FinanceTrack.SyncService.Decrease/Comtrade.FinanceTrack.SyncService.Desrease.csproj --configuration Release -o /app/publish #-c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Comtrade.FinanceTrack.SyncService.Desrease.dll"]