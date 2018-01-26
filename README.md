# CoruscantMarketplace

Aplicativo da API do CoruscantMarketplace.

Para executar, só realizar o build e iniciar o projeto CoruscantMarketplace que irá disponibilidar a WebAPI.

A WebAPI está documentada usando o Swagger, que pode ser acessada via <endereco da aplicacao>/swagger

A API principal fica em <endereco da aplicacao>/api/produto:
* GET /api/Produto
* POST /api/Produto
* PUT /api/Produto/preco/{id}
* DELETE /api/Produto/{id}
* GET /api/Produto/produtocategorialojapreco
* GET /api/Produto/produtoslojaprecoporcategoria/{categoria}
* GET /api/Produto/produtoscategoriaprecoporloja/{loja}
* GET /api/Produto/lojas
* GET /api/Produto/categorias
* GET /api/Produto/resumoproduto/{produto}

Todas as APIs devem ser autenticadas utilizando um token válido, passando no cabeçalho:
"Authorization: Bearer {token}"

Para geração do Token de acesso disponibilizei uma API <endereco da aplicacao>/api/AuthToken que irá gerar um token válido.
A autenticação é feita através do Auth0 (https://auth0.com/).

Para a geração da massa de teste, foi criado o projeto CoruscantMarketplace.GerardorMassaTeste, que é uma aplicação console.
Só executá-la e digitar a url base da API (ex: http://localhost/api).

Também foi disponibilizado uma versão no Azure e pode ser acesso através de:
Documentação Swagger: http://coruscantmarketplace.azurewebsites.net/swagger/
API Produto: http://coruscantmarketplace.azurewebsites.net/api/produto/
API AuthToken: http://coruscantmarketplace.azurewebsites.net/api/AuthToken/

Como servidor na nuvem para o MongoDB, foi usado o mLab (mlab.com).
