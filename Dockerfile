# syntax=docker/dockerfile:1.6
ARG IMG_VERSION=20.18-alpine3.20
FROM node:${IMG_VERSION}

ENV NODE_ENV=production
WORKDIR /usr/src/app

# Copy only package manifests first for better caching
COPY package.json ./
COPY package-lock.json* ./

# Install dependencies with cache mount
RUN --mount=type=cache,target=/root/.npm     if [ -f package-lock.json ]; then       npm ci --omit=dev;     else       npm install --omit=dev;     fi

# Download and patch csv-to-xlsx
ADD https://github.com/Aternus/csv-to-xlsx/archive/refs/tags/v2.1.5.tar.gz .
RUN tar -zxf v2.1.5.tar.gz     && cp -a csv-to-xlsx-2.1.5/. node_modules/@aternus/csv-to-xlsx/     && rm -rf v2.1.5.tar.gz csv-to-xlsx-2.1.5

ADD https://github.com/Aternus/csv-to-xlsx/releases/download/v2.1.5/csv-to-xlsx-linux node_modules/@aternus/csv-to-xlsx/bin/

# Switch to non-root user
USER node

# Copy the rest of the source
COPY . .

EXPOSE 3000
CMD ["npm", "start"]
