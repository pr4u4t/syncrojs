# syntax=docker/dockerfile:1

# Comments are provided throughout this file to help you get started.
# If you need more help, visit the Dockerfile reference guide at
# https://docs.docker.com/go/dockerfile-reference/

# Want to help us make this template better? Share your feedback here: https://forms.gle/ybq9Krt8jtBL3iCk7

ARG IMG_VERSION=20.18-alpine3.20
FROM node:${IMG_VERSION}

# Optional corporate CA (e.g. TLS-inspecting proxy); empty secret is a no-op.
RUN --mount=type=secret,id=extra_ca,required=false \
    if [ -s /run/secrets/extra_ca ]; then cat /run/secrets/extra_ca >> /etc/ssl/certs/ca-certificates.crt; fi
ENV NODE_EXTRA_CA_CERTS=/etc/ssl/certs/ca-certificates.crt

ARG INSTALL_CPP_TOOLS=false
RUN if [ "$INSTALL_CPP_TOOLS" = "true" ]; then apk add --no-cache g++ boost-dev; fi

ENV NODE_ENV=production

WORKDIR /usr/src/app
   
# Download dependencies as a separate step to take advantage of Docker's caching.
# Leverage a cache mount to /root/.npm to speed up subsequent builds.
# Leverage a bind mounts to package.json and package-lock.json to avoid having to copy them into
# into this layer.
RUN --mount=type=bind,source=package.json,target=package.json \
    --mount=type=bind,source=package-lock.json,target=package-lock.json \
    --mount=type=cache,target=/root/.npm \
    npm ci --omit=dev

ADD https://github.com/Aternus/csv-to-xlsx/archive/refs/tags/v2.1.5.tar.gz .
RUN tar -zxf v2.1.5.tar.gz
RUN cp -a csv-to-xlsx-2.1.5/. node_modules/@aternus/csv-to-xlsx/
RUN rm -rf v2.1.5.tar.gz
RUN rm -rf csv-to-xlsx-2.1.5

ADD https://github.com/Aternus/csv-to-xlsx/releases/download/v2.1.5/csv-to-xlsx-linux node_modules/@aternus/csv-to-xlsx/bin/

RUN mkdir -p uploads && chown node:node uploads

# Run the application as a non-root user.
USER node

# Copy the rest of the source files into the image.
COPY . .

# Expose the port that the application listens on.
EXPOSE 3000

# Run the application.
CMD [ "npm", "start" ]
