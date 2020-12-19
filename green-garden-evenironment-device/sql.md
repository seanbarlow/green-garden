# SQL Config

docker run -e "ACCEPT_EULA=Y" -e "SA_PASSWORD=B@rl0wtech!" -p 1433:1433 --name SBSQL19  -d mcr.microsoft.com/mssql/server:2019-latest

docker run -p 1433:1433 --name greengardensql -d greengardenserver:sql2019

docker run -p 1433:1433 --name greengardensql -d greengardenserver:sql2019

