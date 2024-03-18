FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
RUN apt-get update && \
    apt-get install -yq tzdata && \
    ln -fs /usr/share/zoneinfo/America/Sao_Paulo /etc/localtime && \
    dpkg-reconfigure -f noninteractive tzdata

ENV TZ="America/Sao_Paulo"

WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["ExternalEntities.API/ExternalEntities.API.csproj", "ExternalEntities.API/"]
COPY ["BuildingBlocks/CommomStartupExtensions/CommomStartupExtensions.csproj", "BuildingBlocks/CommomStartupExtensions/"]
COPY ["BuildingBlocks/PaginationHelper/PaginationHelper.csproj", "BuildingBlocks/PaginationHelper/"]
COPY ["BuildingBlocks/WebHost.Customization/WebHost.Customization.csproj", "BuildingBlocks/WebHost.Customization/"]
COPY ["ExternalEntities.Infraestructure/ExternalEntities.Infraestructure.csproj", "ExternalEntities.Infraestructure/"]
COPY ["MessageBrokerRabbitMq/MessageBrokerRabbitMq.csproj", "MessageBrokerRabbitMq/"]
COPY ["MessageBrokerCore/MessageBrokerCore.csproj", "MessageBrokerCore/"]
COPY ["BuildingBlocks/QuodProvider/QuodProvider.csproj", "BuildingBlocks/QuodProvider/"]
COPY ["BuildingBlocks/HttpClientHelper/HttpClientHelper.csproj", "BuildingBlocks/HttpClientHelper/"]
COPY ["BuildingBlocks/BrValidators/BrValidators.csproj", "BuildingBlocks/BrValidators/"]
COPY ["ExternalEntities.Domain/ExternalEntities.Domain.csproj", "ExternalEntities.Domain/"]
COPY ["BuildingBlocks/BrDateTimeUtils/BrDateTimeUtils.csproj", "BuildingBlocks/BrDateTimeUtils/"]
RUN dotnet restore "ExternalEntities.API/ExternalEntities.API.csproj"
COPY . .
WORKDIR "/src/ExternalEntities.API"
RUN dotnet build "ExternalEntities.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ExternalEntities.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ExternalEntities.API.dll"]