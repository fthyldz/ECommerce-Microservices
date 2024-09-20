FROM mcr.microsoft.com/dotnet/runtime:8.0 AS base
USER $APP_UID
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/Services/Ordering/Ordering.ProductCreatedConsumer/Ordering.ProductCreatedConsumer.csproj", "src/Services/Ordering/Ordering.ProductCreatedConsumer/"]
COPY ["src/MessageBus/ECommerce.MessageBus/ECommerce.MessageBus.csproj", "src/MessageBus/ECommerce.MessageBus/"]
RUN dotnet restore "src/Services/Ordering/Ordering.ProductCreatedConsumer/Ordering.ProductCreatedConsumer.csproj"
COPY . .
WORKDIR "/src/src/Services/Ordering/Ordering.ProductCreatedConsumer"
RUN dotnet build "Ordering.ProductCreatedConsumer.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Ordering.ProductCreatedConsumer.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Ordering.ProductCreatedConsumer.dll"]
