# AuthCar API – Teste Prático .NET 10

## Visão Geral

AuthCar é uma API RESTful desenvolvida para o teste prático de Desenvolvedor .NET Sênior / Arquiteto. O objetivo é demonstrar arquitetura em camadas, boas práticas, organização e domínio das tecnologias modernas do ecossistema .NET.

---

## Tecnologias e Padrões Utilizados

- **.NET 10**
- **ASP.NET Core Web API** (Controllers)
- **OpenAPI (Swagger)**
- **Entity Framework Core InMemory** (persistência em memória)
- **JWT para autenticação e autorização**
- **MediatR** (Commands, Queries, Handlers)
- **FluentValidation** (validação de dados)
- **Argon2** para hash de senha (mais seguro que BCrypt)
- **Arquitetura em camadas** (WebApi, Application, Domain, Infra)
- **Tratamento global de erros com ExceptionHandlingMiddleware**
- **Arquivo `.env` para informações sensíveis (na pasta Config, ignorado pelo Git)**

---


## Arquitetura da Solução

A arquitetura do projeto é inspirada em princípios de **DDD (Domain-Driven Design)**, pois:
- O domínio do sistema está centralizado na camada `Domain`, com entidades, enums e interfaces de repositório.
- A camada `Application` orquestra os casos de uso e lógica de aplicação, desacoplada da infraestrutura.
- A camada `Infra` implementa persistência e repositórios, separando detalhes técnicos do domínio.
- A camada `WebApi` expõe apenas endpoints HTTP, sem lógica de negócio.

Embora não implemente todos os padrões avançados de DDD (como Aggregates, Value Objects, Domain Events), a estrutura está adaptada para fácil evolução e segue boas práticas de separação de responsabilidades.

Além disso, o projeto utiliza o padrão **RCBA (Request-Command-Behavior-Action)** através do MediatR:
- Cada requisição HTTP é convertida em um Command ou Query, tratado por um Handler específico.
- Commands representam ações que alteram o estado do sistema; Queries realizam consultas.
- Essa abordagem facilita manutenção, testes e evolução do sistema, promovendo clareza e organização.

A solução está dividida em camadas bem definidas:

### 1. WebApi

- **Controllers**
  - `AuthController`: Login e geração de token JWT.
  - `UsuarioController`: Cadastro e consulta de usuários.
  - `VeiculoController`: CRUD de veículos (protegido por JWT).
- **Swagger/OpenAPI**: Documentação e testes interativos.
- **Configuração do JWT**: Autenticação e autorização.
- **Middleware de tratamento global de exceções**: `ExceptionHandlingMiddleware` garante respostas padronizadas e detalhadas para erros.


### 2. Application

- **Handlers do MediatR**
  - Cada operação (Command/Query) possui um Handler específico, que centraliza a lógica de negócio de cada caso de uso.
  - Quando a regra de negócio não é compartilhada entre múltiplos casos de uso, não há necessidade de criar um Service separado. O Handler já cumpre esse papel, mantendo o código simples e coeso.
  - Caso uma regra fosse utilizada em diferentes operações, seria recomendável extrair para um Service reutilizável.
  - Commands e Queries para usuários, autenticação e veículos.
  - Validação com FluentValidation.

### 3. Domain

- **Entidades**
  - `Usuario`: Id, Nome, Login, Senha (hash).
  - `Veiculo`: Id, Descrição, Marca (Enum), Modelo, Valor (opcional).
- **Enumeradores**
  - `Marca`: Marcas de veículos.
- **Interfaces de Repositório**
  - `IUsuarioRepository`
  - `IVeiculoRepository`

### 4. Infra

- **DbContext**: Contexto EF Core InMemory.
- **Repositórios**: Implementações para acesso a dados de usuários e veículos.

---

## Funcionalidades Extras do Projeto

- **Hash de senha com Argon2**: Mais seguro que BCrypt.
- **Seed de usuário admin**: Usuário admin criado automaticamente na inicialização.
- **Arquivo `.env` para dados sensíveis**: Configurações como chaves secretas do JWT podem ser armazenadas em `.env` dentro da pasta `Config`, que está no `.gitignore` para evitar exposição de informações sensíveis.

---


## Tratativas de Erro

O projeto trata e retorna respostas padronizadas para os seguintes tipos de erro, além dos mínimos exigidos pela especificação:

- **400 Bad Request:** Erros de validação (FluentValidation) ou requisições inválidas. Retorna mensagens detalhadas.
- **401 Unauthorized:** Falha de autenticação ou token ausente/inválido. Retorna mensagem de acesso não autorizado.
- **403 Forbidden:** Sem permissão (roles/claims, futuro). Retornaria acesso proibido.
- **409 Conflict:** Situações de conflito, como cadastro duplicado ou violação de regras de negócio.
- **500 Internal Server Error:** Exceções não tratadas, indicando erro interno do servidor.

Todos esses erros são tratados pelo `ExceptionHandlingMiddleware` e retornam respostas padronizadas via `ProblemDetails`, garantindo clareza e padronização para o consumidor da API.

---

## Como Executar

1. **Clone o repositório:**
   ```sh
   git clone https://github.com/maiconcardozo/AuthCar.git
   cd AuthCar
   ```

2. **Abra a solução no Visual Studio 2022 ou superior.**

3. **Compile e execute o projeto (`AuthCar.API`).**
   - O Swagger estará disponível em `http://localhost:<porta>/swagger`.

---

## Como Criar o Arquivo `.env` na Pasta Config

A pasta `Config` já existe no projeto. Para criar o arquivo `.env` nela, siga um dos métodos abaixo:

### Via comando (Windows PowerShell ou CMD)

Abra o terminal na raiz do projeto e execute:

```sh
echo # ========================================================================= > AuthCar.API\Config\.env
echo # (JWT Token) >> AuthCar.API\Config\.env
echo # ========================================================================= >> AuthCar.API\Config\.env
echo. >> AuthCar.API\Config\.env
echo JWT_ISSUER=AuthCarAPI >> AuthCar.API   \Config\.env
echo JWT_AUDIENCE=AuthCarClient >> AuthCar.API\Config\.env
echo JWT_KEY=S0Z6N3Z4dTVHeXpBcmU5V21uUXpMcjV0R2JJY05tS3A4ZzV3RVYyeU9YclQ2YmU1bjM0Z2hLcDltU0ZqdzI >> AuthCar.API\Config\.env
```

### Manualmente pelo Visual Studio

1. Clique com o botão direito na pasta `Config` dentro do projeto `AuthCar.API`.
2. Selecione “Add” > “New Item” > “Text File”.
3. Nomeie o arquivo como `.env`.
4. Insira o seguinte conteúdo:

```ini
# =========================================================================
# (JWT Token)
# =========================================================================

JWT_ISSUER=AuthCarAPI
JWT_AUDIENCE=AuthCarClient
JWT_KEY=S0Z6N3Z4dTVHeXpBcmU5V21uUXpMcjV0R2JJY05tS3A4ZzV3RVYyeU9YclQ2YmU1bjM0Z2hLcDltU0ZqdzI
```

> **Atenção:** O arquivo `.env` está no `.gitignore` e não será versionado no Git, protegendo informações sensíveis.
>
> **Importante:** A chave JWT utilizada no exemplo é gerada aleatoriamente apenas para fins de teste e não oferece risco. Em ambientes reais, **nunca exponha chaves ou segredos no código ou em arquivos públicos**. Utilize variáveis de ambiente seguras e mantenha todas as informações sensíveis protegidas.

---


## Cadastro e Autenticação de Usuário

> **Primeiro uso:** Ao iniciar o projeto, já existe um usuário admin criado automaticamente para facilitar testes:
> - **Login:** admin
> - **Senha:** senha123

### 1. Cadastro de Usuário

- **Endpoint:** `POST /api/usuarios`
- **Exemplo de JSON:**
  ```json
  {
    "nome": "João Silva",
    "login": "joaosilva",
    "senha": "minhasenha123"
  }
  ```

### 2. Login

- **Endpoint:** `POST /api/auth/login`
- **Exemplo de JSON:**
  ```json
  {
    "login": "joaosilva",
    "senha": "minhasenha123"
  }
  ```
- **Resposta:** Token JWT

---

## Utilizando o Token JWT no Swagger

1. **Faça login** para obter o token JWT.
2. **Clique em "Authorize"** no Swagger UI.
3. **Cole o token** no formato: `Bearer <seu_token_aqui>`.
4. **Acesse os endpoints protegidos** (CRUD de veículos).

---

## Endpoints de Veículos (Protegidos por JWT)

- **Cadastrar veículo:** `POST /api/veiculos`
  ```json
  {
    "descricao": "Sedan confortável",
    "marca": "Toyota",
    "modelo": "Corolla",
    "valor": 85000
  }
  ```
- **Atualizar veículo:** `PUT /api/veiculos/{id}`
- **Consultar veículo por Id:** `GET /api/veiculos/{id}`
- **Listar veículos:** `GET /api/veiculos`
- **Remover veículo:** `DELETE /api/veiculos/{id}`

---

## Exemplos de JSON

### Usuário
```json
{
  "nome": "Maria Souza",
  "login": "mariasouza",
  "senha": "senhaSegura"
}
```

### Veículo
```json
{
  "descricao": "SUV espaçoso",
  "marca": "Honda",
  "modelo": "CR-V",
  "valor": 120000
}
```

---


## Foundation.Base

Este projeto utiliza o pacote NuGet [`Foundation.Base`](https://www.nuget.org/packages/Foundation.Base/3.0.3-rc.1), uma biblioteca de autoria própria que serve como base para entidades em todas as aplicações desenvolvidas. O `Foundation.Base` fornece abstrações e funcionalidades comuns para o trabalho com entidades e integrações com o Entity Framework, promovendo padronização, reutilização e maior produtividade no desenvolvimento.

## Estrutura do Projeto

```
AuthCar/
│
├── AuthCar.API/           # Web API (Controllers, Swagger, JWT, Middleware)
├── AuthCar.Application/   # Camada de aplicação (Services, MediatR Handlers)
├── AuthCar.Domain/        # Camada de domínio (Entidades, Enums, Interfaces)
├── AuthCar.Infra/         # Infraestrutura (DbContext, Repositórios)
└── Authcar.Shared/        # Camada de funções e exceptions compartilhadas
```

---


## Observações sobre Claims/Roles

> **Trade-off:** A feature de roles/claims no JWT não foi implementada devido ao tempo limitado para testes e validação. Optei por estruturar o projeto de forma robusta e adaptada para fácil inclusão dessa funcionalidade no futuro, priorizando qualidade e organização do código.

---

## Possíveis Evoluções

**Claims/Roles no JWT:**
Para adicionar controle de acesso por perfil (roles) e claims, basta incluir as informações de perfil do usuário (ex: `role: admin`, `role: user`) no token JWT gerado durante o login. Os endpoints protegidos podem ser configurados para exigir determinados roles usando `[Authorize(Roles = "Admin")]` ou validação customizada. A estrutura do projeto já está adaptada para receber claims/roles, bastando implementar a lógica de atribuição e validação conforme a necessidade.

**Soft Delete:**
Para implementar soft delete nas entidades, basta adicionar um campo como `IsDeleted` (boolean) ou `DeletedAt` (DateTime?) nas entidades do domínio. Ao invés de remover o registro do banco, basta marcar o campo como verdadeiro ou preencher a data de exclusão. Os repositórios e queries devem ser ajustados para filtrar apenas registros não excluídos (`IsDeleted == false`). Essa abordagem preserva o histórico e facilita auditoria, sem perder dados importantes.

**Internacionalização (i18n) com Resources:**
Uma evolução interessante seria implementar suporte a múltiplos idiomas na aplicação, utilizando arquivos de resource (`.resx`) para mensagens, validações e respostas da API. Isso permite adaptar o sistema para diferentes públicos, melhorando a experiência do usuário e tornando o projeto pronto para uso global.

**Uso de Pepper com Argon2:**
Para elevar ainda mais a segurança na criptografia de senhas, recomenda-se utilizar um "pepper" — um valor secreto adicional, armazenado em ambiente seguro (ex: variável de ambiente). O pepper é concatenado à senha antes de aplicar o hash Argon2, dificultando ataques mesmo em caso de vazamento do banco de dados. Essa prática é complementar ao uso de salt e fortalece a proteção das credenciais dos usuários.

---


## Testes com Postman

Para facilitar os testes da API, uma collection do Postman está disponível na pasta `docs` do projeto. Siga os passos abaixo para importar:

1. Abra o Postman em seu computador.
2. Clique em **"Import"** no menu lateral.
3. Selecione **"Upload Files"**.
4. Navegue até a pasta `docs` do projeto e selecione o arquivo da collection (exemplo: `AuthCar.postman_collection.json`).
5. Clique em **"Open"** e depois em **"Import"**.

A collection será adicionada ao seu Postman, permitindo testar todos os endpoints da API de forma prática e rápida.

> **Dica:** A collection já inclui exemplos de requisições para autenticação, cadastro de usuários, operações de veículos e uso do token JWT.

---

## Contato

Para dúvidas, entre em contato pelo maicon.cardozo@hotmail.com

