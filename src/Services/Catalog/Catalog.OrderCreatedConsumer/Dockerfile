FROM mcr.microsoft.com/dotnet/runtime:8.0 AS base
USER $APP_UID
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/Services/Catalog/Catalog.OrderCreatedConsumer/Catalog.OrderCreatedConsumer.csproj", "src/Services/Catalog/Catalog.OrderCreatedConsumer/"]
COPY ["src/MessageBus/ECommerce.MessageBus/ECommerce.MessageBus.csproj", "src/MessageBus/ECommerce.MessageBus/"]
RUN dotnet restore "src/Services/Catalog/Catalog.OrderCreatedConsumer/Catalog.OrderCreatedConsumer.csproj"
COPY . .
WORKDIR "/src/src/Services/Catalog/Catalog.OrderCreatedConsumer"
RUN dotnet build "Catalog.OrderCreatedConsumer.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Catalog.OrderCreatedConsumer.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Catalog.OrderCreatedConsumer.dll"]
