FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER root

RUN apt-get update && apt-get install -y ca-certificates
COPY ["../certs/identity-service.crt", "/usr/local/share/ca-certificates/identity-service.crt"]
COPY ["../certs/blog-service.crt", "/usr/local/share/ca-certificates/blog-serivce.crt"]
COPY ["../certs/message-service.crt", "/usr/local/share/ca-certificates/message-service.crt"]
COPY ["../certs/api-gateway.crt", "/usr/local/share/ca-certificates/api-gateway.crt"]

COPY ["../certs/blog-service.pfx", "https/"]
RUN update-ca-certificates

WORKDIR /app
EXPOSE 8082

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR "/src"
COPY ["BlogService/Presentation/Presentation.csproj", "BlogService/Presentation/"]
COPY ["BlogService/Infrastructure/Infrastructure.csproj", "BlogService/Infrastructure/"]
COPY ["BlogService/Domain/Domain.csproj", "BlogService/Domain/"]
COPY ["BlogService/Application/Application.csproj", "BlogService/Application/"]
COPY ["SharedResources/SharedResources/SharedResources.csproj", "SharedResources/SharedResources/"]
RUN dotnet restore "BlogService/Presentation/Presentation.csproj"

COPY . .
WORKDIR "/src"
RUN dotnet build "BlogService/Presentation/Presentation.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "BlogService/Presentation/Presentation.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

ENV ASPNETCORE_ENVIRONMENT=Container
ENV ASPNETCORE_URLS=https://*:8082
ENV ASPNETCORE_Kestrel__Certificates__Default__Path=/https/blog-service.pfx
ENV ASPNETCORE_Kestrel__Certificates__Default__Password=1256
ENTRYPOINT ["dotnet", "Presentation.dll"]
