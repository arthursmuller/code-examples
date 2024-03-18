-include .env
#proxy ambiente servidor


TAG ?= ${TAG}
TEST ?= 0
SONAR ?= 0
NAME = ${PROJECT_NAME}
REPO = bempromotora/${NAME}

.PHONY: build test push shell run start stop logs clean release

default: prepare

print:
	echo "TEST: ${TEST}"
	echo "SONAR: ${SONAR}"
	echo "PROJECT_NAME:  "
	echo "DOTNET_VER: ${DOTNET_VER}"
	echo "BUILDCONFIG: ${BUILDCONFIG}"
	echo "ASPNETCORE_ENVIRONMENT: ${ASPNETCORE_ENVIRONMENT}"
	echo "PROXY: ${PROXY}"
	echo "PROXY_BYPASS: ${PROXY_BYPASS}"

prepare: 
	git submodule update --init

buildcicd:
	make build PROXY=http://10.90.1.30:3128

build:
	docker build \
		--no-cache \
		--force-rm \
		--build-arg PROXY=${PROXY} \
		--build-arg PROXY_BYPASS=${PROXY_BYPASS} \
		--build-arg TAG=${TAG} \
		--build-arg DOTNET_VER=${DOTNET_VER} \
		--build-arg BUILDCONFIG=${BUILDCONFIG} \
		--build-arg ASPNETCORE_ENVIRONMENT=${ASPNETCORE_ENVIRONMENT} \
		--build-arg BUILD_NUMBER=$(BUILD_NUMBER) \
		--label BUILD_NUMBER=$(BUILD_NUMBER) \
		-t $(REPO):$(TAG) . 
		
sonar: 
	docker run --rm -t -v $(shell pwd):/src bempromotora/dotnet-sdk:${DOTNET_VER} bash -c "export test=1;export http_proxy=http://10.90.1.30:3128;export https_proxy=http://10.90.1.30:3128;export ftp_proxy=http://10.90.1.30:3128;export PROJECT_NAME=${PROJECT_NAME};export IMAGETAG=${TAG};/usr/bin/dotnet-sonar"

unittest:
	make build TEST=1

push:
	docker push $(REPO):$(TAG)

shell:
	docker-compose exec -T web bash

test-imagem: 
	make start
	sleep 30
	make logs
	make logs | grep -q "Application started" >/dev/null && echo "It works!" || exit 1

run:
	docker-compose up 

start:
	docker-compose up -d --remove-orphans

stop:
	docker-compose down --remove-orphans

logs:
	docker-compose logs

remove:
	docker image rm $(REPO):$(TAG) -f

release: build push