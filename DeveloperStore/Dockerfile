# Etapa 1: Build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copiar o arquivo da solução (DeveloperStore.sln) para o diretório de trabalho
COPY DeveloperStore.sln ./

# Copiar a estrutura de pastas dos projetos
COPY src/ ./src/
COPY tests/ ./tests/

# Restaurar as dependências
RUN dotnet restore

# Alterar para o diretório onde o arquivo .csproj do projeto principal está localizado
WORKDIR /app/src/DeveloperStore.WebApi

# Publicar o projeto principal
RUN dotnet publish -c Release -o /publish --no-restore

# Etapa 2: Runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app

# Copiar os arquivos publicados da etapa anterior
COPY --from=build /publish .

# Configurar a porta exposta
EXPOSE 5000
EXPOSE 5001

# Definir a entrada do container
ENTRYPOINT ["dotnet", "DeveloperStore.WebApi.dll"]