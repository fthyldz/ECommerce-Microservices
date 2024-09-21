FROM mcr.microsoft.com/dotnet/runtime:8.0 AS base
USER $APP_UID
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/Services/Catalog/Consumers/Catalog.OrderCreatedEventConsumer/Catalog.OrderCreatedEventConsumer.csproj", "src/Services/Catalog/Consumers/Catalog.OrderCreatedEventConsumer/"]
COPY ["src/MessageBus/ECommerce.MessageContracts/ECommerce.MessageContracts.csproj", "src/MessageBus/ECommerce.MessageContracts/"]
COPY ["src/Services/Catalog/Consumers/Common/Catalog.Consumers.Common/Catalog.Consumers.Common.csproj", "src/Services/Catalog/Consumers/Common/Catalog.Consumers.Common/"]
COPY ["src/MessageBus/ECommerce.Consumer/ECommerce.Consumer.csproj", "src/MessageBus/ECommerce.Consumer/"]
RUN dotnet restore "src/Services/Catalog/Consumers/Catalog.OrderCreatedEventConsumer/Catalog.OrderCreatedEventConsumer.csproj"
COPY . .
WORKDIR "/src/src/Services/Catalog/Consumers/Catalog.OrderCreatedEventConsumer"
RUN dotnet build "Catalog.OrderCreatedEventConsumer.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Catalog.OrderCreatedEventConsumer.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Catalog.OrderCreatedEventConsumer.dll"]
