FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/Services/Catalog/Catalog.API/Catalog.API.csproj", "src/Services/Catalog/Catalog.API/"]
COPY ["src/Common/ECommerce.API/ECommerce.API.csproj", "src/Common/ECommerce.API/"]
COPY ["src/Common/ECommerce.Application/ECommerce.Application.csproj", "src/Common/ECommerce.Application/"]
COPY ["src/Common/ECommerce.Domain/ECommerce.Domain.csproj", "src/Common/ECommerce.Domain/"]
COPY ["src/Services/Catalog/Catalog.Application/Catalog.Application.csproj", "src/Services/Catalog/Catalog.Application/"]
COPY ["src/Services/Catalog/Catalog.Domain/Catalog.Domain.csproj", "src/Services/Catalog/Catalog.Domain/"]
COPY ["src/MessageBus/ECommerce.MessageBus/ECommerce.MessageBus.csproj", "src/MessageBus/ECommerce.MessageBus/"]
COPY ["src/MessageBus/ECommerce.MessageContracts/ECommerce.MessageContracts.csproj", "src/MessageBus/ECommerce.MessageContracts/"]
COPY ["src/Services/Catalog/Catalog.Persistence/Catalog.Persistence.csproj", "src/Services/Catalog/Catalog.Persistence/"]
COPY ["src/Common/ECommerce.Persistence/ECommerce.Persistence.csproj", "src/Common/ECommerce.Persistence/"]
COPY ["src/Common/ECommerce.Infrastructure/ECommerce.Infrastructure.csproj", "src/Common/ECommerce.Infrastructure/"]
RUN dotnet restore "src/Services/Catalog/Catalog.API/Catalog.API.csproj"
COPY . .
WORKDIR "/src/src/Services/Catalog/Catalog.API"
RUN dotnet build "Catalog.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Catalog.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Catalog.API.dll"]
