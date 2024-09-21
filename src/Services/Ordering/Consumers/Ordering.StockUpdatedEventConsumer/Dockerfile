FROM mcr.microsoft.com/dotnet/runtime:8.0 AS base
USER $APP_UID
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/Services/Ordering/Consumers/Ordering.StockUpdatedEventConsumer/Ordering.StockUpdatedEventConsumer.csproj", "src/Services/Ordering/Consumers/Ordering.StockUpdatedEventConsumer/"]
COPY ["src/MessageBus/ECommerce.MessageContracts/ECommerce.MessageContracts.csproj", "src/MessageBus/ECommerce.MessageContracts/"]
COPY ["src/Services/Ordering/Consumers/Common/Ordering.Consumers.Common/Ordering.Consumers.Common.csproj", "src/Services/Ordering/Consumers/Common/Ordering.Consumers.Common/"]
COPY ["src/MessageBus/ECommerce.Consumer/ECommerce.Consumer.csproj", "src/MessageBus/ECommerce.Consumer/"]
RUN dotnet restore "src/Services/Ordering/Consumers/Ordering.StockUpdatedEventConsumer/Ordering.StockUpdatedEventConsumer.csproj"
COPY . .
WORKDIR "/src/src/Services/Ordering/Consumers/Ordering.StockUpdatedEventConsumer"
RUN dotnet build "Ordering.StockUpdatedEventConsumer.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Ordering.StockUpdatedEventConsumer.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Ordering.StockUpdatedEventConsumer.dll"]
