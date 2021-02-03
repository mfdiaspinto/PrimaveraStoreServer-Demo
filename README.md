# Primavera Store Server - Demo
Demo de projeto de integração com o PRIMAVERA Invoicing Engine

Este prototipo tem como objetivo dar alguns exemplos de utilização da API do Invoicing Engine e do seu Middleware

Para tal é composto por dois projetos
 - PrimaveraStoreServer (WebAPI)
 > Este projeto tem a responsabilidade de mapear os dados para objetos validos e executar os request's ao PRIMAVERA Invoicing Engine
 
 - TestIntegrations (UNITTEST)
 > Este projeto tem a responsabilidade ter conjunto de testes unitários que executa métodos desenvolvidos no PrimaveraStore.
 
 ## Requests desenvolvidos no prototipo
 
  - Criar Artigo
  - Criar Cliente
  - Criar Fatura
  - Criar Fatura via middleware
  - Criar Faturas em bulk
  
   ## Como começar:
   
   Alterar no ficheiro de constantes as seguintes variaveis se necessário
   
![alt text](https://github.com/mfdiaspinto/PrimaveraStoreServer-Demo/blob/master/Files/Configura%C3%A7%C3%A3o.PNG?raw=true)

- ClientId 
> Cliente para autenticar os pedidos
- ClientSecret
> Secret para autenticar os pedidos
-Account e Subscription
> o Cliente deve ter acesso a esta conta
- IdentityUrl 
> por defeito está o url de staging
-BaseAppUrl
> por defeito está o url de staging
- Company
> empresa tem de existir na subscrição
