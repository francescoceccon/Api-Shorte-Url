# Api-Shorte-Url

 Este projeta contempla de maneira sucinta porém de toda forma 'robusta' para aquilo que se propoe sendo isto , um encurtamento de URL's.


## Tecnologias utilizadas
#### C# 
#### .Net 6
#### EntityFramework
#### PostGres
#### Docker
#### Swagger

Como citado anteriormente o projeto apesar de sua simplicidade tanto em escopo e implementacao possui já todo um esqueleto e ambiente para tornar-se uma app muito mais robusta e escalavel.
Foram utilizadas boas praticas de codificaçao com base nas tecnologias utilizadas.

# Primeiros passos 

Ao buildar o dockerfile seram instalados todos os pacotes necessarios para a aplicação rodar,inclusive ja sera gerado automaticamente a migration para o banco.

![image](https://github.com/francescoceccon/Api-Shorte-Url/assets/61200730/474a2d9c-460a-444f-b782-4c067b140d80)

Para esta aplicacao foram criados endpoints que compoem um CRUD vou explicar um a um.

# 1 ConsumeMottuJsonReceiving 
Este endpoint recebe o json com os objetos de url especificaods de maneira externa ou seja o cliente que insere manualmente no corpo da requisicao

# 2 ConsumeMottuJsonInternal
Este endpoint persiste automaticamente no DB todas as URL's definidas no Json de maneira automatica sem a necessidade do cliente preencher o corpo

# 3 UrlShorterAsync
Este endpoint persiste uma URL a escolha do cliente no DB ,nota a URL sera transformada em uma menor

# 4 RedirectUrl
Este endpoint redireciona o cliente para uma URL e incrementa o 'hit' no banco.
Este endpoint recebe uma url ja tratada, ou seja , ja transformada em uma menor e redireciona atualizando o numero do 'hit' que o cliente fez a esta URL
Ponto importante a se tratar aqui é que como estamos utilizando o Swagger ele nativamente impede que realizemos um redirect para outra pagina, neste cenario 
para que possamos realmente comtemplar o redirect se faz necessario outro cliente HTTP, a exemplo podemos utilizar o POSTMAN e realizar a request utilizando o Curl
tal como as fotos

# Exemplo para rota do youtube #
![image](https://github.com/francescoceccon/Api-Shorte-Url/assets/61200730/b9f758c3-2313-4192-b750-a3b32db5d7ce)
![image](https://github.com/francescoceccon/Api-Shorte-Url/assets/61200730/cb58093a-df0f-4ae1-83d8-db112a7240d5)
![image](https://github.com/francescoceccon/Api-Shorte-Url/assets/61200730/5fa16166-f9bb-4e52-97b9-e7272a182347)


# 5 GetMottuUrl
Este endpoint recebe uma Url ja tratada e devolve a linha inteira do registro do DB , esta rota foi criada para podermos utilizar o delete e update

# 6 GetUrl
Este endpoint recebe um Id e devolve a linha inteira do registro do DB , esta rota foi criada para podermos utilizar o delete e update

# 7 DeleteUrl
Este endpoint recebe um Id e realiza o delete da linha respectiva no DB

# 8 UpdateUrl
Este endpoint recebe uma DTO que reflete a entidade persistida no banco e realiza o update de todas as properties com execao do Id
