dev:
	docker build -t syncrojs-dev --build-arg NODE_ENV=development .

production:
	docker build -t syncrojs-prod --build-arg NODE_ENV=production .

run-dev:
	docker kill syncrojs-dev
	docker rm syncrojs-dev
	docker run --name syncrojs-dev -d -p 3000:3000 syncrojs-dev

run-prod:
	docker kill syyncrojs-prod
	docker rm syncrojs-prod
	docker run --name syncrojs-prod -d -p 3000:3000 syncrojs-prod
