dev:
	docker build -t syncrojs-dev --build-arg NODE_ENV=development .

.PHONY: dev production check-production-env deploy-production update-production stop-production logs-production package-production load-production run-prod test run-dev run-test

PROD_ENV_FILE ?= .env.production
PROD_COMPOSE = docker compose --env-file $(PROD_ENV_FILE) -f compose.production.yaml
PROD_PACKAGE ?= syncrojs-production.tar

check-production-env:
	@test -f "$(PROD_ENV_FILE)" || { echo "Missing $(PROD_ENV_FILE). Copy .env.production.example and configure it."; exit 1; }

production: check-production-env
	$(PROD_COMPOSE) build

deploy-production: check-production-env
	$(PROD_COMPOSE) up -d --no-build

update-production: production deploy-production

stop-production: check-production-env
	$(PROD_COMPOSE) down

logs-production: check-production-env
	$(PROD_COMPOSE) logs -f

package-production: production
	docker save -o "$(PROD_PACKAGE)" $$($(PROD_COMPOSE) config --images)

load-production:
	docker load -i "$(PROD_PACKAGE)"

run-prod: production deploy-production

test:
	npm test -- --runInBand

run-dev:
	docker kill syncrojs-dev
	docker rm syncrojs-dev
	docker run --name syncrojs-dev -d -p 3000:3000 syncrojs-dev

run-test:
	npm test -- --runInBand
