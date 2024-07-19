#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 9105
ENV ASPNETCORE_URLS=http://*:9105

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY . /src
RUN dotnet restore "/src/Gateways/Comtrade.FinanceTrack.Gateways.Default/Comtrade.FinanceTrack.Gateways.Default.csproj"
COPY . .
WORKDIR "/src/Gateways/Comtrade.FinanceTrack.Gateways.Default"
RUN dotnet build "Comtrade.FinanceTrack.Gateways.Default.csproj" -c Debug -o /app/build 

FROM build AS publish
RUN dotnet publish "Comtrade.FinanceTrack.Gateways.Default.csproj" --configuration Release -o /app/publish 

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Comtrade.FinanceTrack.Gateways.Default.dll"]