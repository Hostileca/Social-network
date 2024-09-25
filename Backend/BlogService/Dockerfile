FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8082

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["BlogService/Presentation/Presentation.csproj", "Presentation/"]
COPY ["BlogService/Infrastructure/Infrastructure.csproj", "Infrastructure/"]
COPY ["BlogService/Domain/Domain.csproj", "Domain/"]
COPY ["BlogService/Application/Application.csproj", "Application/"]
COPY ["../SharedResources/SharedResources/SharedResources.csproj", "../SharedResources/"]
RUN dotnet restore "Presentation/Presentation.csproj"
COPY . .
WORKDIR "/src/BlogService/Presentation"
RUN dotnet build "Presentation.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Presentation.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENV ASPNETCORE_ENVIRONMENT=Container
ENV ASPNETCORE_URLS=http://*:8082
ENTRYPOINT ["dotnet", "Presentation.dll"]
