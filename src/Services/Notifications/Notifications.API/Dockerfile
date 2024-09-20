FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/Services/Notifications/Notifications.API/Notifications.API.csproj", "src/Services/Notifications/Notifications.API/"]
COPY ["src/Services/Notifications/Notifications.Data/Notifications.Data.csproj", "src/Services/Notifications/Notifications.Data/"]
COPY ["src/Services/Notifications/Notifications.Business/Notifications.Business.csproj", "src/Services/Notifications/Notifications.Business/"]
RUN dotnet restore "src/Services/Notifications/Notifications.API/Notifications.API.csproj"
COPY . .
WORKDIR "/src/src/Services/Notifications/Notifications.API"
RUN dotnet build "Notifications.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Notifications.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Notifications.API.dll"]
