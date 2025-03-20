Inicialize os serviços necessários:

`docker run -d --name contacts-rabbitmq -p 5672:5672 -p 15672:15672 rabbitmq:management-alpine`

`docker run -e "ACCEPT_EULA=Y" -e "MSSQL_SA_PASSWORD=P@ssw0rd1" -p 1433:1433 -d mcr.microsoft.com/mssql/server:2022-latest`


Após os containeres estarem rodando, no Visual Studio aplique as migrations ao BD.

Utilize o Console do Gerenciador de Pacotes

`Update-Database`
