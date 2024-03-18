<img width="300" src="https://www.bempromotora.com.br/wp-content/uploads/2019/12/logo-bem.png" alt="Bem Promotora Logo" />

<hr/>
#[Plataforma Cliente Final - BackEnd]
 é a solução que faz a integração entre os dados apresentados 
pelo projeto [Plataforma Cliente Final - FrontEnd] e as demais integrações necessárias.

<hr/>

## Tecnologias

1. Dot Net Core 3.1
2. Entity Framework Core

## Dependências

1. B.Nuget - Pacotes padrões de desenvolvimento da Bem
2. TecBemLabs - AuthAPI : Autenticação no ambiente da BemPromotora ()
3. TecBemLabs - BemAPI : Obtenção de dados cadastrais dos clientes
4. TecBemLabs - Produto.Consignado : Obtenção das simulações e inclusão de Proposta.
5. Robô de Envio de e-mail
6. Robô de Envio de SMS
7. Geolocalização IP: MaxMind
8. Gerenciamento de Filas - MassTransit
9. Logs Azure Insights - A aplicar. 
10. Anexos - BlobStorage

## Ambientes

PROD - Ambiente de Produção ([Para acessar: ](http://clientefinalback.azurewebsites.net/))
[![Build Status](https://dev.azure.com/tecbem/Plataforma%20Cliente%20Final%20-%20Back/_apis/build/status/clientefinalback%20-%20prod%20-%20CI?branchName=master)](https://dev.azure.com/tecbem/Plataforma%20Cliente%20Final%20-%20Back/_build/latest?definitionId=7&branchName=master)

DEV: Testes específicos [Para acessar: ](https://clientefinalback-develop.azurewebsites.net/)
[![Build Status](https://dev.azure.com/tecbem/Plataforma%20Cliente%20Final%20-%20Back/_apis/build/status/clientefinalback%20-%20develop%20-%20CI?branchName=dev)](https://dev.azure.com/tecbem/Plataforma%20Cliente%20Final%20-%20Back/_build/latest?definitionId=8&branchName=dev)

HOMOLOG: Testes específicos [Para acessar: ](https://clientefinalback-homolog.azurewebsites.net/)
[![Build Status](https://dev.azure.com/tecbem/Plataforma%20Cliente%20Final%20-%20Back/_apis/build/status/clientefinalback%20-%20homolog%20-%20CI?branchName=homolog)](https://dev.azure.com/tecbem/Plataforma%20Cliente%20Final%20-%20Back/_build/latest?definitionId=9&branchName=homolog)

## Padrões de Desenvolvimento

### SQL

Existem alguns padrões que foram definidos para o desenvolvimento de soluções usando o banco de dados SQL Server. Tais padrões devem ser inseridos como regra no nosso desenvolviemnto de código. [Padrões de Desenvolvimento T-SQL](https://conhecimento.bempromotora.com.br/pages/viewpage.action?pageId=39815615)

### API

    Documentar modelo de Desenvolvimento na API - Pastas


### HTTP

*GET* - 
*POST* -
*PUT* - 
*PATH* -
*DELETE* - 