# Etapa 1: Build
FROM node:18 AS build

WORKDIR /app

# Copie os arquivos necessários
COPY package*.json ./
COPY angular.json tsconfig*.json ./
COPY src ./src

# Instale as dependências e construa o app
RUN npm install
RUN npm run build --prod

# Etapa 2: Servir
FROM nginx:1.23

# Copie os arquivos do build para o NGINX
COPY --from=build /app/dist/[sales-frontend] /usr/share/nginx/html

# Exponha a porta 80 para servir a aplicação
EXPOSE 80

# Inicie o NGINX
CMD ["nginx", "-g", "daemon off;"]
