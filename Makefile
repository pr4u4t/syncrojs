.PHONY: dev dev-down up down production check-production-env deploy-production update-production restart-production stop-production logs-production package-production load-production run-prod test run-test

PROD_ENV_FILE ?= .env.production
PROD_COMPOSE = docker compose --env-file $(PROD_ENV_FILE) -f compose.production.yaml
PROD_PACKAGE ?= syncrojs-production.tar
DEV_COMPOSE = docker compose -f compose.yaml -f compose.local-db.yaml

# Dev: database container from compose.local-db.yaml.
dev:
	$(DEV_COMPOSE) up --build

dev-down:
	$(DEV_COMPOSE) down

# Normal: external database configured in .env.
up:
	docker compose up --build

down:
	docker compose down

check-production-env:
	@test -f "$(PROD_ENV_FILE)" || { echo "Missing $(PROD_ENV_FILE). Copy .env.production.example and configure it."; exit 1; }

production: check-production-env
	$(PROD_COMPOSE) build

deploy-production: check-production-env
	$(PROD_COMPOSE) up -d --no-build

update-production: production deploy-production

restart-production: check-production-env
	$(PROD_COMPOSE) restart

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

run-test:
	npm test -- --runInBand
