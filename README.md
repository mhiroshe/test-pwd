# Objetivo
Validar senhas conforme as definições:

- Nove ou mais caracteres
- Ao menos 1 dígito
- Ao menos 1 letra minúscula
- Ao menos 1 letra maiúscula
- Ao menos 1 caractere especial

## Arquitetura
O projeto foi desenvolvido em .net core, utilizando o padrão DDD e seguindo os padrões Clean Code, SOLID e 12 fatores, exposto por uma Api Rest.

## Pré-requisitos
- Docker
ou
- .NET Core SDK 2.2

## Execução
Clonar este repositório, e seguir um dos procedimentos abaixo:

### Docker
Na pasta raiz do projeto, executar os comandos:
- docker-compose build --no-cache 
- docker-compose up

> O comando docker-compose build --no-cache executa os testes unitários da aplicação, caso ocorra alguma falha o processo é interrompido.

Para testar a aplicação basta executar o curl:
```
curl --location --request POST 'http://localhost:5001/password/is-valid' \
--header 'Content-Type: application/json' \
--data-raw '{
	"password":"D@tabase01"
}'
```

### Execução local
Para iniciar a aplicação, entrar na pasta src\Password.Application e executar o comando
```
dotnet run
```
Para executar os testes unitários, entrar na pasta tests\Password.Tests e executar o comando
```
dotnet test
```

Para testar a aplicação basta executar o curl:
```
curl --location --request POST 'http://localhost:5000/password/is-valid' \
--header 'Content-Type: application/json' \
--data-raw '{
	"password":"D@tabase01"
}'
```

## Observações
- Como é uma aplicação simples, utilizei uma arquitetura mais compacta
- Dockerizei para facilitar a execução
- Em um cenário real, implementaria logs, tracings e demais itens para acompanhamento da aplicação no ambiente de produção
- Utilizei como regra para a validação das senhas regex. Um debito técnico que mapeei, seria deixar a regra de validação configuravel em um gerenciador de configuração como o Consul, para não termos que alterar o código.