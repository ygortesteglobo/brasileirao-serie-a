O Serviço Brasileirão Série A permite o cadastro de clubes da Série A do Campeonato Brasileiro através de uma API REST.

Utilizamos para esse serviço as seguintes tecnologias cloud da AWS (Amazon Web Services):

1. Dynamo DB;
2. Beanstalk;
3. EC2;

## Cadastro de um clube

#### Request
url: 
```
PUT http://{{host}}/api/teams
```
headers:
```
  content-type: application/json
  authorization: bearer {{authenticated-token}}
```
body:
```
{
  "name": "Clube de Regatas Flamengo",
  "shortName": "Flamengo",
  "image": "http://flamengo.com.br/image.jpg"
}
```
#### Response
body:
```
{
  "id": "12345",
}
```

Status Code:
- 200: Registro atualizado com sucesso
- 201: Registro criado com sucesso
- 400: Erro no JSON enviado
- 401: Usuário não autenticado (adicionar o header authorization para resolver este caso)
- 403: Acesso negado (usuário autenticado não possui acesso para cadastrar um clube


## Consultando um clube
url: 
```
GET http://{{host}}/api/teams/{id}
```
headers:
```
  content-type: application/json
  authorization: bearer {{authenticated-token}}
```
#### Response
body:
```
{
  "id": "12345",
  "name": "Clube de Regatas Flamengo",
  "shortName": "Flamengo",
  "image": "http://flamengo.com.br/image.jpg"
}
```

Status Code:
- 200: Retornar o registro ou um objeto nulo
- 401: Usuário não autenticado (adicionar o header authorization para resolver este caso)
- 403: Acesso negado (usuário autenticado não possui acesso para cadastrar um clube

## Consultando a listagem de clubes
url: 
```
GET http://{{host}}/api/teams
```
headers:
```
  content-type: application/json
  authorization: bearer {{authenticated-token}}
```
#### Response
body:
```
[
  {
    "id": "12345",
    "name": "Clube de Regatas Flamengo",
    "shortName": "Flamengo",
    "image": "http://flamengo.com.br/image.jpg"
  },
  {
    ...
  }
]
```

Status Code:
- 200: Retornar os registros cadastrados
- 401: Usuário não autenticado (adicionar o header authorization para resolver este caso)
- 403: Acesso negado (usuário autenticado não possui acesso para cadastrar um clube

## Removendo um clube
url: 
```
DELETE http://{{host}}/api/teams/{id}
```
headers:
```
  content-type: application/json
  authorization: bearer {{authenticated-token}}
```
#### Response
Status Code:
- 200: Remove o registro com sucesso
- 401: Usuário não autenticado (adicionar o header authorization para resolver este caso)
- 403: Acesso negado (usuário autenticado não possui acesso para cadastrar um clube
