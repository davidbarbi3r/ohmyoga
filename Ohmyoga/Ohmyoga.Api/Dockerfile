FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Ohmyoga.Api/Ohmyoga.Api.csproj", "Ohmyoga.Api/"]
COPY ["Ohmyoga.Application/Ohmyoga.Application.csproj", "Ohmyoga.Application/"]
COPY ["Ohmyoga.Contracts/Ohmyoga.Contracts.csproj", "Ohmyoga.Contracts/"]
RUN dotnet restore "Ohmyoga.Api/Ohmyoga.Api.csproj"
COPY . .
WORKDIR "/src/Ohmyoga.Api"
RUN dotnet build "Ohmyoga.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Ohmyoga.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Ohmyoga.Api.dll"]
