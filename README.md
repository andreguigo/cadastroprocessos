# CadProcessosApp

O código é exemplo de um CRUD em Asp.Net MVC com Razor Pages e exemplifica o processo de Cadastro de Processos e faz parte do teste da FSBR.

## Construção

### Pré requisitos
* .Net SDK 8
* EntityFrameworkCore 8

##### Outros pacotes na aplicação
* EntityFrameworkCore.Tools
* Pomelo.EntityFrameworkCore.MySql
* Pomelo.EntityFrameworkCore.MySql.Design

##### Banco de dados e migrations

Se preferir, crie uma base de dados chamada `cadprocessos`
~~~mysql
create database cadprocessos;
~~~
Apos configurar a string de conexão em `appsettings.json` na raiz da aplicação, execute a migração conforme o comando abaixo:
~~~pm
update-database
~~~
