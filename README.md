# DeveloperEvaluationProject Solution

Este repositório contém a solução para o projeto **DeveloperEvaluationProject**, incluindo código-fonte, testes automatizados e configurações para executar o aplicativo em um contêiner Docker.

---

## Estrutura do Projeto

- **src/**: Contém o código-fonte do projeto principal.
- **tests/**: Contém os projetos de teste automatizado.
- - **sales-management-frontend/**: Contém o código-fonte do projeto frontend.

---

## Requisitos

Para executar a solução, certifique-se de ter os seguintes itens instalados no sistema:

- **Docker** (versão 20.10 ou superior)
- **.NET SDK** (opcional para execução local, versão 8.0 ou superior)
- **NODE JS** (opcional para execução local)
- **Angular** (opcional para execução local, versão 14.0 ou superior)

---

## Como Executar

### 1. Construção de Contêiner e Execução

Para construir o contêiner backend e frontend Docker, execute o comando abaixo na raiz do repositório (onde o **Dockerfile** está localizado):

```bash
docker-compose up --build .
```
O aplicativo backend estará acessível em [http://localhost:5000](http://localhost:5000).

O aplicativo frontend estará acessível em [http://localhost:4200](http://localhost:4200).

---

## Testes Automatizados

Os testes são definidos no projeto `DeveloperStore.Tests`, localizado em `tests/DeveloperStore.Tests`. Eles cobrem os seguintes aspectos:

- Validação de regras de negócio.
- Comportamento esperado em cenários de erro.

Para executar os testes localmente, navegue até o diretório de testes e execute:

```bash
cd tests/DeveloperStore.Tests
dotnet test
```

---
## Licença

Este projeto é licenciado sob a [MIT License](LICENSE).
