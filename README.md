# FastTechFoods - ClientAPI

Este microserviço é responsável pelo cadastro e autenticação de clientes da plataforma FastTechFoods.

## 🚀 Como rodar com Docker Compose

1. Execute na raiz do projeto:

`setup-client-api.ps1`

2. Acesse a aplicação em: `http://localhost:5000/swagger/index.html`

## 🧱 Tecnologias

- ASP.NET Core (.NET 8)
- Entity Framework Core
- MediatR (CQRS)
- SQL Server
- Docker + Docker Compose
- Kubernetes (K8s)

## 📦 Endpoints

| Método | Rota                 | Descrição              |
|--------|----------------------|------------------------|
| POST   | `/api/clients`       | Cadastrar cliente      |
| POST   | `/api/clients/login` | Login do cliente       |
| GET    | `/api/clients/{id}`  | Buscar cliente por ID  |
