#docker build --build-arg MODE=production --pull -t "frd_app_ui:production" .
#docker build --build-arg MODE=production --pull -t "frd_app_ui:develop" .
#docker build --build-arg MODE=local --pull -t "frd_app_ui:local" .

# build
FROM node:lts-alpine as build-stage
ARG MODE='production'
WORKDIR /app
COPY package.json ./
RUN npm install
COPY . .
RUN ["sh","-c","npm run build -- --mode ${MODE}"]
# deploy
FROM nginx:stable-alpine as production-stage
COPY --from=build-stage /app/dist /usr/share/nginx/html
COPY --from=build-stage /app/deploy/nginx.conf /etc/nginx/
EXPOSE 80
CMD ["nginx", "-g", "daemon off;"]
