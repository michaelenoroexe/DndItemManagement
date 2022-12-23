FROM node:19.3.0-alpine AS node
# Building client
WORKDIR /app
COPY package.json package-lock.json ./
RUN npm install
COPY . .
RUN npm run build
# Running nginx
FROM nginx:1.23.3-alpine
COPY nginx/ /etc/nginx/
RUN apk add curl
COPY --from=node /app/dist/item-manager-client /usr/share/nginx/html
EXPOSE 80
EXPOSE 49531
