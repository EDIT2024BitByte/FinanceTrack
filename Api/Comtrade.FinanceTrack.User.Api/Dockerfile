#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 9102
ENV ASPNETCORE_URLS=http://*:9102

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY . /src
RUN dotnet restore "/src/Api/Comtrade.FinanceTrack.User.Api/Comtrade.FinanceTrack.User.Api.csproj"
COPY . .
WORKDIR "/src/Api/Comtrade.FinanceTrack.User.Api"
RUN dotnet build "Comtrade.FinanceTrack.User.Api.csproj" -c Debug -o /app/build 

FROM build AS publish
RUN dotnet publish "Comtrade.FinanceTrack.User.Api.csproj" --configuration Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Comtrade.FinanceTrack.User.Api.dll"]