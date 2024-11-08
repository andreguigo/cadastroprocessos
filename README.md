# CadProcessosApp
O código é exemplo de um CRUD em Asp.Net MVC com Razor Pages e exemplifica o processo de Cadastro de Processos e faz parte do teste da FSBR.

## Construção

### Pré requisitos
* .Net SDK 8
* EntityFrameworkCore 8

###### Outros pacotes na aplicação
* EntityFrameworkCore.Tools
* Pomelo.EntityFrameworkCore.MySql
* Pomelo.EntityFrameworkCore.MySql.Design
* X.PagedList

### Banco de dados e migrations

Se preferir, crie uma base de dados chamada `cadprocessos`
~~~mysql
create database cadprocessos;
~~~
Após configurar a string de conexão em `appsettings.json` na raiz da aplicação, execute a migração conforme o comando abaixo:
~~~pm
update-database
~~~

### Git e/ou Versionamento
`master` Branch de versão finalizada e publicada.

`develop` Branch para desenvolvimento. A partir dela deverá ser criada novas branchs para _feature, patch, bugfix, etc_ como por exemplo `feat-organizacao-tabela`.

_Aqui não há necessidade de uma branch para homologação ou outras situações por se tratar de uma aplicação de laboratório_.

## Execução

Finalizado os comandos citados na sessão 'Construção', certifique-se que você está na raíz da solução `...\CadastroDeProcessos>` e então execute:
~~~
.\run-local
~~~

## Atualizações

__Vs 1.2.0__
* Adicionado paginação da tabela principal de Processos

__Vs 1.1.0__
- Foi adicionado o padrão de projeto Unit of Work para garantir uniformidade e escalabilidade.
- Retirado a paginação temporariamente.

__Vs 1.0.0__
- Versão inicial do projeto
