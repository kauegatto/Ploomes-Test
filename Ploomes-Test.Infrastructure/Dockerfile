FROM mcr.microsoft.com/dotnet/runtime:8.0 AS base
USER $APP_UID
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Ploomes-Test.Infrastructure/Ploomes-Test.Infrastructure.csproj", "Ploomes-Test.Infrastructure/"]
RUN dotnet restore "Ploomes-Test.Infrastructure/Ploomes-Test.Infrastructure.csproj"
COPY . .
WORKDIR "/src/Ploomes-Test.Infrastructure"
RUN dotnet build "Ploomes-Test.Infrastructure.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Ploomes-Test.Infrastructure.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Ploomes-Test.Infrastructure.dll"]
