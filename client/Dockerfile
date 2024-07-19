#FROM node:20.10.0 AS build
#WORKDIR /usr/src/app
#ENV PATH /usr/src/app/node_modules/.bin:$PATH

#COPY ../client /usr/src/app

#COPY . /usr/src/app
#RUN npm install --legacy-peer-deps
#RUN npm run build
FROM bitnami/nginx:1.25.1
COPY ./nginx.conf  /opt/bitnami/nginx/conf/nginx.conf
COPY ./dist/client/browser /app
#COPY  /usr/src/app/dist /opt/bitnami/nginx/html
#COPY --from=build /usr/src/app/dist /opt/bitnami/nginx/html