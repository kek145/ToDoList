# Первый этап: сборка Angular-приложения
FROM node:18.18.0 AS build-env

WORKDIR /app

COPY package*.json ./

RUN npm install

COPY . .

RUN npm run build

# Второй этап: настройка Apache сервера
FROM httpd:alpine3.15

WORKDIR /usr/local/apache2/htdocs

# Копируем конфигурацию Apache
COPY httpd.conf /usr/local/apache2/conf/httpd.conf

# Копируем собранные файлы Angular в каталог htdocs Apache
COPY --from=build-env /app/dist/to-do-list.frontend/ .

# Добавляем .htaccess файл, если он необходим
COPY .htaccess /usr/local/apache2/htdocs/

# Ваш файл Dockerfile готов для сборки Angular-приложения и запуска его с использованием Apache в контейнере Docker
