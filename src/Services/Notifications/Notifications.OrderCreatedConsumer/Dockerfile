FROM mcr.microsoft.com/dotnet/runtime:8.0 AS base
USER $APP_UID
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/Services/Notifications/Notifications.OrderCreatedConsumer/Notifications.OrderCreatedConsumer.csproj", "src/Services/Notifications/Notifications.OrderCreatedConsumer/"]
COPY ["src/MessageBus/ECommerce.MessageBus/ECommerce.MessageBus.csproj", "src/MessageBus/ECommerce.MessageBus/"]
RUN dotnet restore "src/Services/Notifications/Notifications.OrderCreatedConsumer/Notifications.OrderCreatedConsumer.csproj"
COPY . .
WORKDIR "/src/src/Services/Notifications/Notifications.OrderCreatedConsumer"
RUN dotnet build "Notifications.OrderCreatedConsumer.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Notifications.OrderCreatedConsumer.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Notifications.OrderCreatedConsumer.dll"]
