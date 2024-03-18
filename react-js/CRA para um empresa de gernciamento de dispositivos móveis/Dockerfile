FROM node:12-alpine as build
WORKDIR /app
RUN apk update

COPY package.json ./
COPY .eslintrc.js ./
COPY .env ./
COPY tsconfig.json ./
COPY src ./src
COPY yarn.lock ./
COPY babel.config.js ./
COPY public ./public
RUN npm install --global yarn --force 
RUN yarn install 
RUN yarn build

FROM nginx:1.21-alpine as runtime
RUN apk add --no-cache vim procps curl tar
COPY --from=build /app/build/ /usr/share/nginx/html
COPY nginx/default.conf /etc/nginx/conf.d/default.conf
