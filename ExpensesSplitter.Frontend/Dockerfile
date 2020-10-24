FROM node:15
WORKDIR /app
RUN npm install -g @angular/cli @angular-devkit/build-angular
COPY package*.json ./
RUN npm install
EXPOSE 4200
CMD ["ng", "serve", "--host", "0.0.0.0", "--poll=2000"]
