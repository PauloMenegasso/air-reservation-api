FROM ubuntu

COPY . app/src

ENV DEBIAN_FRONTEND=noninteractive

RUN apt-get update && apt-get install -y npm

WORKDIR /app/src

RUN npm install

EXPOSE 3000

CMD ["npm", "start"]