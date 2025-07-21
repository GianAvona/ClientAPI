
# 1. Criar a rede Docker se ainda não existir
docker network create fasttechfood-network 2>$null

# 2. Subir o container do SQL Server com senha segura
docker run --name fasttechfood-client-db --hostname client-mssql --network fasttechfood-network -e "ACCEPT_EULA=Y" -e "MSSQL_SA_PASSWORD=SuaSenhaForte123@" -p 1433:1433 -d mcr.microsoft.com/mssql/server:2022-latest

# Aguarda o SQL Server iniciar completamente (ajustável)
Start-Sleep -Seconds 20

# 3. Criar o banco de dados ClientDB via sqlcmd em container auxiliar
docker run -it --rm --network fasttechfood-network mcr.microsoft.com/mssql-tools     /bin/bash -c "/opt/mssql-tools/bin/sqlcmd -S fasttechfood-client-db -U sa -P 'SuaSenhaForte123@' -Q 'IF DB_ID(N''ClientDB'') IS NULL CREATE DATABASE ClientDB'"

# 4. Build da imagem da API
docker build -t client-api:latest -f ./docker/Dockerfile .

# 5. Subir a API conectada à mesma rede do banco
docker run --name client-api-container --network fasttechfood-network -p 5000:80 -e "ASPNETCORE_ENVIRONMENT=Development" -e "ConnectionStrings__DefaultConnection=Server=fasttechfood-client-db;Database=ClientDB;User Id=sa;Password=SuaSenhaForte123@;Encrypt=False;TrustServerCertificate=True" -d client-api:latest

# Aguarda a API subir
Start-Sleep -Seconds 10

# 6. Rodar as migrations para garantir a criação das tabelas (usando container temporário)
docker run -it --rm --network fasttechfood-network -v "${PWD}:/app" -w /app/src/Client.API mcr.microsoft.com/dotnet/sdk:8.0 bash -c 'dotnet tool install --global dotnet-ef && export PATH=$PATH:/root/.dotnet/tools && dotnet ef database update -p ../Client.Infrastructure -s .'
