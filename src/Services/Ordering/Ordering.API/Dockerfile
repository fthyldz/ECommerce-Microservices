FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/Services/Ordering/Ordering.API/Ordering.API.csproj", "src/Services/Ordering/Ordering.API/"]
COPY ["src/Services/Ordering/Ordering.Application/Ordering.Application.csproj", "src/Services/Ordering/Ordering.Application/"]
COPY ["src/Services/Ordering/Ordering.Domain/Ordering.Domain.csproj", "src/Services/Ordering/Ordering.Domain/"]
COPY ["src/Common/ECommerce.Domain/ECommerce.Domain.csproj", "src/Common/ECommerce.Domain/"]
COPY ["src/MessageBus/ECommerce.MessageContracts/ECommerce.MessageContracts.csproj", "src/MessageBus/ECommerce.MessageContracts/"]
COPY ["src/Common/ECommerce.Application/ECommerce.Application.csproj", "src/Common/ECommerce.Application/"]
COPY ["src/Services/Ordering/Ordering.Persistence/Ordering.Persistence.csproj", "src/Services/Ordering/Ordering.Persistence/"]
COPY ["src/Common/ECommerce.Persistence/ECommerce.Persistence.csproj", "src/Common/ECommerce.Persistence/"]
COPY ["src/Common/ECommerce.API/ECommerce.API.csproj", "src/Common/ECommerce.API/"]
COPY ["src/Common/ECommerce.Infrastructure/ECommerce.Infrastructure.csproj", "src/Common/ECommerce.Infrastructure/"]
RUN dotnet restore "src/Services/Ordering/Ordering.API/Ordering.API.csproj"
COPY . .
WORKDIR "/src/src/Services/Ordering/Ordering.API"
RUN dotnet build "Ordering.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Ordering.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Ordering.API.dll"]
