#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/runtime:8.0 AS base
USER app
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY . /src
RUN dotnet restore "/src/WorkerService/Comtrade.FinanceTrack.SyncService.Increase/Comtrade.FinanceTrack.SyncService.Increase.csproj"
COPY . .
WORKDIR "/src/WorkerService/Comtrade.FinanceTrack.SyncService.Increase"
RUN dotnet build "Comtrade.FinanceTrack.SyncService.Increase.csproj" -c Debug -o /app/build #-c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
RUN dotnet publish "Comtrade.FinanceTrack.SyncService.Increase.csproj" --configuration Release -o /app/publish #-c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Comtrade.FinanceTrack.SyncService.Increase.dll"]