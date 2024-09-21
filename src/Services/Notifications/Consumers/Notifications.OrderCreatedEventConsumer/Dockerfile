FROM mcr.microsoft.com/dotnet/runtime:8.0 AS base
USER $APP_UID
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/Services/Notifications/Consumers/Notifications.OrderCreatedEventConsumer/Notifications.OrderCreatedEventConsumer.csproj", "src/Services/Notifications/Consumers/Notifications.OrderCreatedEventConsumer/"]
COPY ["src/Services/Notifications/Consumers/Common/Notifications.Consumers.Common/Notifications.Consumers.Common.csproj", "src/Services/Notifications/Consumers/Common/Notifications.Consumers.Common/"]
COPY ["src/MessageBus/ECommerce.Consumer/ECommerce.Consumer.csproj", "src/MessageBus/ECommerce.Consumer/"]
COPY ["src/MessageBus/ECommerce.MessageContracts/ECommerce.MessageContracts.csproj", "src/MessageBus/ECommerce.MessageContracts/"]
RUN dotnet restore "src/Services/Notifications/Consumers/Notifications.OrderCreatedEventConsumer/Notifications.OrderCreatedEventConsumer.csproj"
COPY . .
WORKDIR "/src/src/Services/Notifications/Consumers/Notifications.OrderCreatedEventConsumer"
RUN dotnet build "Notifications.OrderCreatedEventConsumer.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Notifications.OrderCreatedEventConsumer.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Notifications.OrderCreatedEventConsumer.dll"]
