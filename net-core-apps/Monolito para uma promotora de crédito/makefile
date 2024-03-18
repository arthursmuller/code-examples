-include env_make
DOTNET_VER?= 2.2
#proxy ambiente servidor
PROXY?= http://10.90.1.30:3128

TAG ?= latest
NAME = analiserisco
REPO = bempromotora/$(NAME)

.PHONY: build test push shell run start stop logs clean release

default: build

build:
	docker-compose build

sonar:
	docker build -t $(REPO):$(TAG)  \
		--build-arg DOTNET_VER=$(DOTNET_VER) \
		--build-arg PROXY=${PROXY} \
		--build-arg TEST=0 \
		--build-arg SONAR=0 \
		./

push:
	docker push $(REPO):$(TAG)

shell:
	docker run --rm --name $(NAME) -i -t $(PORTS) $(VOLUMES) $(ENV) $(REPO):$(TAG) /bin/bash

run:
	docker run --rm --name $(NAME) $(PORTS) $(VOLUMES) $(ENV) $(REPO):$(TAG) $(CMD)

start:
	docker run -d --name $(NAME) $(PORTS) $(VOLUMES) $(ENV) $(REPO):$(TAG)

stop:
	docker stop $(NAME)

logs:
	docker logs $(NAME)

clean:
	-docker rm -f $(NAME)

release: build push