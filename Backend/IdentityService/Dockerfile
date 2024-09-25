FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["IdentityService/PresentationLayer/PresentationLayer.csproj", "PresentationLayer/"]
COPY ["IdentityService/BusinessLogicLayer/BusinessLogicLayer.csproj", "BusinessLogicLayer/"]
COPY ["IdentityService/DataAccessLayer/DataAccessLayer.csproj", "DataAccessLayer/"]
COPY ["../SharedResources/SharedResources/SharedResources.csproj", "../SharedResources/"]
RUN dotnet restore "PresentationLayer/PresentationLayer.csproj"
COPY . .
WORKDIR "/src/IdentityService/PresentationLayer"
RUN dotnet build "PresentationLayer.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "PresentationLayer.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENV ASPNETCORE_ENVIRONMENT=Container
ENV ASPNETCORE_URLS=http://*:8081
ENTRYPOINT ["dotnet", "PresentationLayer.dll"]
