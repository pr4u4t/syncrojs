# Makefile
# Usage examples:
#   make                 # default: help
#   make build-dev       # build development image
#   make run-dev         # run dev container
#   make clean           # clean containers and images

.PHONY: help build-dev build-prod build-test run-dev run-prod run-test stop-dev stop-prod stop-test clean tag push

# Default target
.DEFAULT_GOAL := help

# Enable BuildKit
export DOCKER_BUILDKIT=1

# Base image name (repo/project)
IMAGE_NAME := syncrojs

# Tags for environments
TAG_DEV  := dev
TAG_PROD := prod
TAG_TEST := test

# Full image references
IMAGE_DEV  := $(IMAGE_NAME):$(TAG_DEV)
IMAGE_PROD := $(IMAGE_NAME):$(TAG_PROD)
IMAGE_TEST := $(IMAGE_NAME):$(TAG_TEST)

# Container names
CONT_DEV  := $(IMAGE_NAME)-dev
CONT_PROD := $(IMAGE_NAME)-prod
CONT_TEST := $(IMAGE_NAME)-test

# Optional env directory
ENV_DIR := env

help:
	@echo "Targets:"
	@echo "  build-dev       Build development image"
	@echo "  build-prod      Build production image"
	@echo "  build-test      Build test image"
	@echo "  run-dev         Run dev container (port 3000)"
	@echo "  run-prod        Run prod container (port 3000)"
	@echo "  run-test        Run test container"
	@echo "  stop-dev        Stop & remove dev container"
	@echo "  stop-prod       Stop & remove prod container"
	@echo "  stop-test       Stop & remove test container"
	@echo "  clean           Remove containers and images"
	@echo "  tag VERSION=?   Tag prod image with version"
	@echo "  push            Push all images"

### Install deps
# Install npm dependencies locally
npm-install:
	@echo "Installing npm dependencies locally..."
	npm install

# Prepare csv-to-xlsx if not already present
prepare-csv:
	@if [ ! -d "node_modules/@aternus/csv-to-xlsx" ]; then 		echo "csv-to-xlsx not found. Downloading and patching..."; 		curl -L -o v2.1.5.tar.gz https://github.com/Aternus/csv-to-xlsx/archive/refs/tags/v2.1.5.tar.gz; 		tar -zxf v2.1.5.tar.gz; 		mkdir -p node_modules/@aternus/csv-to-xlsx; 		cp -a csv-to-xlsx-2.1.5/. node_modules/@aternus/csv-to-xlsx/; 		rm -rf v2.1.5.tar.gz csv-to-xlsx-2.1.5; 		curl -L -o node_modules/@aternus/csv-to-xlsx/bin/csv-to-xlsx-linux https://github.com/Aternus/csv-to-xlsx/releases/download/v2.1.5/csv-to-xlsx-linux; 		echo "csv-to-xlsx installed successfully."; 	else 		echo "csv-to-xlsx already exists. Skipping download."; 	fi


### Build targets
build-dev:
	@echo "Building $(IMAGE_DEV)"
	docker build --build-arg NODE_ENV=development -t $(IMAGE_DEV) .

build-prod:
	@echo "Building $(IMAGE_PROD)"
	docker build --build-arg NODE_ENV=production -t $(IMAGE_PROD) .

build-test:
	@echo "Building $(IMAGE_TEST)"
	docker build --build-arg NODE_ENV=test -t $(IMAGE_TEST) .

### Run targets
run-dev: stop-dev
	@echo "Starting container $(CONT_DEV) from $(IMAGE_DEV)"
	docker run --name $(CONT_DEV) -d -p 3000:3000 		--env NODE_ENV=development 		$(if $(wildcard $(ENV_DIR)/dev.env),--env-file $(ENV_DIR)/dev.env,) 		$(IMAGE_DEV)

run-prod: stop-prod
	@echo "Starting container $(CONT_PROD) from $(IMAGE_PROD)"
	docker run --name $(CONT_PROD) -d -p 3000:3000 		--env NODE_ENV=production 		$(if $(wildcard $(ENV_DIR)/prod.env),--env-file $(ENV_DIR)/prod.env,) 		$(IMAGE_PROD)

run-test: stop-test
	@echo "Starting container $(CONT_TEST) from $(IMAGE_TEST)"
	docker run --name $(CONT_TEST) -d 		--env NODE_ENV=test 		$(if $(wildcard $(ENV_DIR)/test.env),--env-file $(ENV_DIR)/test.env,) 		$(IMAGE_TEST)

### Stop/remove containers
stop-dev:
	- docker stop $(CONT_DEV) 2>/dev/null || true
	- docker rm   $(CONT_DEV) 2>/dev/null || true

stop-prod:
	- docker stop $(CONT_PROD) 2>/dev/null || true
	- docker rm   $(CONT_PROD) 2>/dev/null || true

stop-test:
	- docker stop $(CONT_TEST) 2>/dev/null || true
	- docker rm   $(CONT_TEST) 2>/dev/null || true

### Tag & push
tag:
	@if [ -z "$(VERSION)" ]; then echo "VERSION is required (e.g., make tag VERSION=1.2.3)"; exit 1; fi
	docker tag $(IMAGE_PROD) $(IMAGE_NAME):$(VERSION)
	@echo "Tagged $(IMAGE_PROD) as $(IMAGE_NAME):$(VERSION)"

push:
	@echo "Pushing images: $(IMAGE_DEV) $(IMAGE_PROD) $(IMAGE_TEST)"
	- docker push $(IMAGE_DEV)
	- docker push $(IMAGE_PROD)
	- docker push $(IMAGE_TEST)

### Clean
clean: stop-dev stop-prod stop-test
	@echo "Removing images: $(IMAGE_DEV) $(IMAGE_PROD) $(IMAGE_TEST)"
	- docker rmi $(IMAGE_DEV)  2>/dev/null || true
	- docker rmi $(IMAGE_PROD) 2>/dev/null || true
	- docker rmi $(IMAGE_TEST) 2>/dev/null || true
