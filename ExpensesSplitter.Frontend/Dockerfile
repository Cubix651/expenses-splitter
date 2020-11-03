FROM node:13
WORKDIR /app
COPY package*.json ./
RUN npm install
EXPOSE 4200
CMD [ "npm", "start", "--", "--host", "0.0.0.0", "--poll=2000"]
