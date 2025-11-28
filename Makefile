
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

# Base image name (repo/project); change to your registry/repo if needed
IMAGE_NAME := syncrojs

# Tags for environments
TAG_DEV  := dev
TAG_PROD := prod
TAG_TEST := test

# Convenience full image references
IMAGE_DEV  := $(IMAGE_NAME):$(TAG_DEV)
IMAGE_PROD := $(IMAGE_NAME):$(TAG_PROD)
IMAGE_TEST := $(IMAGE_NAME):$(TAG_TEST)

# Common container names (avoid conflicts across runs)
CONT_DEV  := $(IMAGE_NAME)-dev
CONT_PROD := $(IMAGE_NAME)-prod
CONT_TEST := $(IMAGE_NAME)-test

# Optional env-file (create ./env/dev.env ./env/prod.env ./env/test.env if you want)
ENV_DIR := env

help:
	@echo "Targets:"
	@echo "  build-dev       Build development image (NODE_ENV=development)"
	@echo "  build-prod      Build production   image (NODE_ENV=production)"
	@echo "  build-test      Build test         image (NODE_ENV=test)"
	@echo "  run-dev         Run dev container (port 3000)"
	@echo "  run-prod        Run prod container (port 3000)"
	@echo "  run-test        Run test container (no port binding)"
	@echo "  stop-dev        Stop & remove dev   container"
	@echo "  stop-prod       Stop & remove prod  container"
	@echo "  stop-test       Stop & remove test  container"
	@echo "  clean           Remove containers and images (dev/prod/test)"
	@echo "  tag VERSION=?   Tag prod image with a version (e.g., 1.2.3)"
	@echo "  push            Push dev/prod/test images to registry"
	@echo "Notes:"
	@echo "  - Uses DOCKER_BUILDKIT=1"
	@echo "  - If you have multi-stage Dockerfile, you can add --target <stage>"
	@echo "  - Create env files in ./env if you need runtime variables"

### Build targets
build-dev:
	@echo "Building $(IMAGE_DEV) with NODE_ENV=development"
	docker build --build-arg NODE_ENV=development -t $(IMAGE_DEV) .

build-prod:
	@echo "Building $(IMAGE_PROD) with NODE_ENV=production"
	docker build --build-arg NODE_ENV=production -t $(IMAGE_PROD) .

build-test:
	@echo "Building $(IMAGE_TEST) with NODE_ENV=test"
	docker build --build-arg NODE_ENV=test -t $(IMAGE_TEST) .

### Run targets
run-dev: stop-dev
    @echo "Starting container $(CONT_DEV) from $(IMAGE_DEV)"
    docker run --name $(CONT_DEV) -d -p 3000:3000 \
        --env NODE_ENV=development \
        $(if $(wildcard $(ENV_DIR)/dev.env),--env-file $(ENV_DIR)/dev.env,) \
        $(IMAGE_DEV)

run-prod: stop-prod
    @echo "Starting container $(CONT_PROD) from $(IMAGE_PROD)"
    docker run --name $(CONT_PROD) -d -p 3000:3000 \
        --env NODE_ENV=production \
        $(if $(wildcard $(ENV_DIR)/prod.env),--env-file $(ENV_DIR)/prod.env,) \
        $(IMAGE_PROD)

run-test: stop-test
    @echo "Starting container $(CONT_TEST) from $(IMAGE_TEST)"
    docker run --name $(CONT_TEST) -d \
        --env NODE_ENV=test \
        $(if $(wildcard $(ENV_DIR)/test.env),--env-file $(ENV_DIR)/test.env,) \
        $(IMAGE_TEST)

### Stop/remove containers (idempotent)
stop-dev:
	- docker stop $(CONT_DEV) 2>/dev/null || true
	- docker rm   $(CONT_DEV) 2>/dev/null || true

stop-prod:
	- docker stop $(CONT_PROD) 2>/dev/null || true
	- docker rm   $(CONT_PROD) 2>/dev/null || true

stop-test:
	- docker stop $(CONT_TEST) 2>/dev/null || true
	- docker rm   $(CONT_TEST) 2>/dev/null || true

### Tag & push (optional)
# Usage: make tag VERSION=1.2.3
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

### Optional: Multi-arch build (requires buildx and QEMU)
# Usage:
#   make buildx-prod         # build linux/amd64 + linux/arm64
#   make buildx-prod PUSH=1  # build & push multi-arch image
buildx-prod:
	@echo "Building multi-arch $(IMAGE_PROD)"
	- docker buildx create --use --name syncrojs_builder 2>/dev/null || true
	docker buildx build \
        	--platform linux/amd64,linux/arm64 \
        	--build-arg NODE_ENV=production \
        	-t $(IMAGE_PROD) \
        	$(if $(PUSH),--push,--load) \
        	.
