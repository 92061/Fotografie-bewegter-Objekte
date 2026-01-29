FROM mcr.microsoft.com/dotnet/runtime:9.0 AS base
USER $APP_UID
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Fotografie Bewegter Objekte/Fotografie Bewegter Objekte.csproj", "Fotografie Bewegter Objekte/"]
RUN dotnet restore "Fotografie Bewegter Objekte/Fotografie Bewegter Objekte.csproj"
COPY . .
WORKDIR "/src/Fotografie Bewegter Objekte"
RUN dotnet build "./Fotografie Bewegter Objekte.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Fotografie Bewegter Objekte.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Fotografie Bewegter Objekte.dll"]
