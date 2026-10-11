FROM alpine:3.20 AS build

# Optional corporate CA (e.g. TLS-inspecting proxy); empty secret is a no-op.
RUN --mount=type=secret,id=extra_ca,required=false \
    if grep -q -- '-----BEGIN CERTIFICATE-----' /run/secrets/extra_ca; then cat /run/secrets/extra_ca >> /etc/ssl/certs/ca-certificates.crt; fi
RUN apk add --no-cache g++ boost-dev
WORKDIR /src
COPY tools/classic_generator.cpp ./classic_generator.cpp
RUN g++ -std=c++17 -O2 -Wall -Wextra -Werror classic_generator.cpp -o /usr/local/bin/classic-generator

FROM alpine:3.20

RUN --mount=type=secret,id=extra_ca,required=false \
    if grep -q -- '-----BEGIN CERTIFICATE-----' /run/secrets/extra_ca; then cat /run/secrets/extra_ca >> /etc/ssl/certs/ca-certificates.crt; fi
RUN apk add --no-cache libgcc libstdc++ \
    && adduser -D -u 1000 node \
    && mkdir -p /run/syncrojs \
    && chown node:node /run/syncrojs
COPY --from=build /usr/local/bin/classic-generator /usr/local/bin/classic-generator

USER node
ENTRYPOINT ["/usr/local/bin/classic-generator", "--socket", "/run/syncrojs/classic-generator.sock"]
