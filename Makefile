image:
	docker build -t syncrojs --build-arg NODE_ENV=development .
run:
	docker kill syncrojs
	docker rm syncrojs
	docker run --name syncrojs -d -p 80:3000 syncrojs
