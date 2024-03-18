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
COPY ["Notifications.API/Notifications.API.csproj", "Notifications.API/"]
COPY ["Notifications.Infraestructure/Notifications.Infraestructure.csproj", "Notifications.Infraestructure/"]
COPY ["MessageBrokerRabbitMq/MessageBrokerRabbitMq.csproj", "MessageBrokerRabbitMq/"]
COPY ["MessageBrokerCore/MessageBrokerCore.csproj", "MessageBrokerCore/"]
COPY ["Notifications.Domain/Notifications.Domain.csproj", "Notifications.Domain/"]
COPY ["BuildingBlocks/BrDateTimeUtils/BrDateTimeUtils.csproj", "BuildingBlocks/BrDateTimeUtils/"]
COPY ["BuildingBlocks/RichEnumeration/RichEnumeration.csproj", "BuildingBlocks/RichEnumeration/"]
COPY ["BuildingBlocks/CommomStartupExtensions/CommomStartupExtensions.csproj", "BuildingBlocks/CommomStartupExtensions/"]
COPY ["BuildingBlocks/PaginationHelper/PaginationHelper.csproj", "BuildingBlocks/PaginationHelper/"]
COPY ["BuildingBlocks/WebHost.Customization/WebHost.Customization.csproj", "BuildingBlocks/WebHost.Customization/"]
RUN dotnet restore "Notifications.API/Notifications.API.csproj"
COPY . .
WORKDIR "/src/Notifications.API"
RUN dotnet build "Notifications.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Notifications.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
COPY ["Notifications.Domain/Resources/EmailTemplates/ResetEmailTemplate.html", "Notifications.Domain/Resources/EmailTemplates/"]
COPY ["Notifications.Domain/Resources/EmailTemplates/ResetPasswordTemplate.html", "Notifications.Domain/Resources/EmailTemplates/"]
COPY ["Notifications.Domain/Resources/EmailTemplates/WelcomeTemplate.html", "Notifications.Domain/Resources/EmailTemplates/"]
COPY ["Notifications.Domain/Resources/EmailTemplates/WelcomeUserTemplate.html", "Notifications.Domain/Resources/EmailTemplates/"]

ENTRYPOINT ["dotnet", "Notifications.API.dll"]