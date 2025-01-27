# DeveloperEvaluationProject Solution

Este reposit�rio cont�m a solu��o para o projeto **TaxCalculator**, incluindo c�digo-fonte, testes automatizados e configura��es para executar o aplicativo em um cont�iner Docker. O objetivo � fornecer uma solu��o escal�vel e modular para c�lculos de impostos com uma cobertura de testes confi�vel.

---

## Estrutura do Projeto

- **src/**: Cont�m o c�digo-fonte do projeto principal.
- **tests/**: Cont�m os projetos de teste automatizado.
- - **sales-management-frontend/**: Cont�m o c�digo-fonte do projeto frontend.

---

## Requisitos

Para executar a solu��o, certifique-se de ter os seguintes itens instalados no sistema:

- **Docker** (vers�o 20.10 ou superior)
- **.NET SDK** (opcional para execu��o local, vers�o 8.0 ou superior)
- **NODE JS** (opcional para execu��o local)
- **Angular** (opcional para execu��o local, vers�o 14.0 ou superior)

---

## Como Executar

### 1. Constru��o de Cont�iner e Execu��o

Para construir o cont�iner backend e frontend Docker, execute o comando abaixo na raiz do reposit�rio (onde o **Dockerfile** est� localizado):

```bash
docker-compose up --build .
```
O aplicativo backend estar� acess�vel em [http://localhost:5000](http://localhost:5000).

O aplicativo frontend estar� acess�vel em [http://localhost:4200](http://localhost:4200).

---

## Testes Automatizados

Os testes s�o definidos no projeto `DeveloperStore.Tests`, localizado em `tests/DeveloperStore.Tests`. Eles cobrem os seguintes aspectos:

- Valida��o de regras de neg�cio.
- Comportamento esperado em cen�rios de erro.

Para executar os testes localmente, navegue at� o diret�rio de testes e execute:

```bash
cd tests/DeveloperStore.Tests
dotnet test
```

---
## Licen�a

Este projeto � licenciado sob a [MIT License](LICENSE).