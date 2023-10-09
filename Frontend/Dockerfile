FROM node:18.17.1 AS build-env
WORKDIR /app

RUN apt-get update && apt-get install -y npm

COPY package*.json ./
RUN npm install

COPY . .
RUN npm run build --prod

FROM nginx:alpine AS runtime
WORKDIR /usr/share/nginx/html

COPY --from=build-env /app/dist/frontend .

EXPOSE 80
CMD ["nginx", "-g", "daemon off;"]