FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
RUN apt-get update; apt-get install -y fontconfig fonts-liberation
RUN fc-cache -f -v

# RUN apt-get update 
# run apt-get install -y wget
# RUN apt-get update && apt-get install -y unzip
# RUN wget https://github.com/google/fonts/archive/master.zip

# RUN unzip master.zip
# RUN cp fonts-master/ofl/* /usr/share/fonts/truetype/google-fonts/



# RUN apt-get install -y libreoffice 
# # RUN apt-get install -y msttcorefonts-installer 
# # RUN apt-get install -y fontconfig
# # RUN update-ms-fonts
# RUN wget https://github.com/google/fonts/archive/main.tar.gz -O gf.tar.gz --no-check-certificate
# RUN tar -xf gf.tar.gz
# RUN mkdir -p /usr/share/fonts/truetype/google-fonts
# RUN find $PWD/fonts-main/ -name "*.ttf" -exec install -m644 {} /usr/share/fonts/truetype/google-fonts/ \; || return 1
# RUN rm -f gf.tar.gz
# RUN fc-cache -f && rm -rf /var/cache/*


# RUN apt-get install -y \
#     fonts-liberation \
#     gconf-service \
#     libappindicator1 \
#     libasound2 \
#     libatk1.0-0 \
#     libcairo2 \
#     libcups2 \
#     libfontconfig1 \
#     libgbm-dev \
#     libgdk-pixbuf2.0-0 \
#     libgtk-3-0 \
#     libicu-dev \
#     libjpeg-dev \
#     libnspr4 \
#     libnss3 \
#     libpango-1.0-0 \
#     libpangocairo-1.0-0 \
#     libpng-dev \
#     libx11-6 \
#     libx11-xcb1 \
#     libxcb1 \
#     libxcomposite1 \
#     libxcursor1 \
#     libxdamage1 \
#     libxext6 \
#     libxfixes3 \
#     libxi6 \
#     libxrandr2 \
#     libxrender1 \
#     libxss1 \
#     libxtst6 \
#     xdg-utils


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
COPY ["Signature.API/Signature.API.csproj", "Signature.API/"]
COPY ["BuildingBlocks/CommomStartupExtensions/CommomStartupExtensions.csproj", "BuildingBlocks/CommomStartupExtensions/"]
COPY ["BuildingBlocks/PaginationHelper/PaginationHelper.csproj", "BuildingBlocks/PaginationHelper/"]
COPY ["BuildingBlocks/WebHost.Customization/WebHost.Customization.csproj", "BuildingBlocks/WebHost.Customization/"]
COPY ["Signature.Infraestructure/Signature.Infraestructure.csproj", "Signature.Infraestructure/"]
COPY ["BuildingBlocks/FilerHelper/FilerHelper.csproj", "BuildingBlocks/FilerHelper/"]
COPY ["Signature.Domain/Signature.Domain.csproj", "Signature.Domain/"]
COPY ["BuildingBlocks/BrDateTimeUtils/BrDateTimeUtils.csproj", "BuildingBlocks/BrDateTimeUtils/"]
COPY ["BuildingBlocks/StorageProvider/StorageProvider.csproj", "BuildingBlocks/StorageProvider/"]
COPY ["MessageBrokerRabbitMq/MessageBrokerRabbitMq.csproj", "MessageBrokerRabbitMq/"]
COPY ["MessageBrokerCore/MessageBrokerCore.csproj", "MessageBrokerCore/"]
COPY ["BuildingBlocks/HttpClientHelper/HttpClientHelper.csproj", "BuildingBlocks/HttpClientHelper/"]
COPY ["BuildingBlocks/AddressProvider/AddressProvider.csproj", "BuildingBlocks/AddressProvider/"]

RUN dotnet restore "Signature.API/Signature.API.csproj"
COPY . .
WORKDIR "/src/Signature.API"
RUN dotnet build "Signature.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Signature.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

COPY ["Signature.Domain/Resources/DigitalSignature/1.jpg", "Signature.Domain/Resources/DigitalSignature/"]
COPY ["Signature.Domain/Resources/Cert/signing-cert.pfx", "Signature.Domain/Resources/Cert/"]

ENTRYPOINT ["dotnet", "Signature.API.dll"]