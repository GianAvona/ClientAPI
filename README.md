# FastTechFoods - ClientAPI

Este microserviço é responsável pelo cadastro e autenticação de clientes da plataforma FastTechFoods.

## 🚀 Como rodar com Docker Compose

1. Execute na raiz do projeto:

```bash
docker-compose up --build
```

2. Acesse a aplicação em: `http://localhost:5000/api/clients`

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






### Docker

## Network

docker network create fasttechfoods-network

## SQL Server

docker run --name fasttechfoods-customers-db --hostname customers-mssql --network fasttechfoods-network -e "ACCEPT_EULA=Y" -e "MSSQL_SA_PASSWORD=FastTechFoods23@" -p 1433:1433 -d mcr.microsoft.com/mssql/server:2022-latest



## Build

docker build -t client-api:latest -f .\src\Client.API\Dockerfile .

## Run

docker run --name fasttechfoods-clients-api `
           --network fasttechfoods-network `
           -p 6004:8080 `
           -e "ASPNETCORE_ENVIRONMENT=Development" `
           -e "ConnectionStrings__Database=Server=customers-mssql;Database=CustomersDB;User Id=sa;Password=FastTechFoods23@;Encrypt=False;TrustServerCertificate=True" `
           client-api