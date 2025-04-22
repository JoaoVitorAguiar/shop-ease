**ShopEase API**

## Visão Geral

A **ShopEase API** é uma aplicação ASP.NET Core (.NET 8) implementada como um _monolito modular_. Cada módulo (Users, Products, Cart, Orders, Notifications) é dividido em três camadas — **Domain**, **Application** e **Infrastructure** — promovendo separação de responsabilidades e escalabilidade.

## Arquitetura

- **Monolito Modular**: cada módulo reside na mesma solução, mas em projetos independentes para domínio, aplicação e infraestrutura.
- **Minimal APIs / Controllers**: camada de apresentação (`Presentation`) expõe rotas HTTP de forma concisa.
- **Clean Architecture**: cada módulo segue padrões de Camada Limpa, isolando lógica de negócio de detalhes de infraestrutura.
- **Injeção de Dependência**: serviços, repositórios e middlewares registrados no `Program.cs` via extensões.

## Estrutura de Pastas

```
ShopEase.sln
│
├── src/
│   ├── Modules/
│   │   ├── Users/
│   │   │   ├── Users.Domain/
│   │   │   ├── Users.Application/
│   │   │   └── Users.Infrastructure/
│   │   ├── Products/
│   │   ├── Cart/
│   │   ├── Orders/
│   │   └── Notifications/
│   ├── Presentation/    # API (Minimal APIs / Controllers)
│   ├── Shared/          # DTOs, Helpers, Mediator compartilhados
│   └── Database/        # Configurações de contexto, migrations e factories
│
├── tests/
│   ├── UnitTests/       # Testes unitários
│   └── IntegrationTests/# Testes de integração
│
├── Dockerfile
├── docker-compose.yml
└── ShopEase.sln
```

## Módulos

| Módulo         | Domain             | Application           | Infrastructure        |
|----------------|--------------------|-----------------------|-----------------------|
| Users          | Users.Domain       | Users.Application     | Users.Infrastructure  |
| Products       | Products.Domain    | Products.Application  | Products.Infrastructure|
| Cart           | Cart.Domain        | Cart.Application      | Cart.Infrastructure   |
| Orders         | Orders.Domain      | Orders.Application    | Orders.Infrastructure |
| Notifications  | Notifications.Domain| Notifications.Application| Notifications.Infrastructure|

### Funcões de Cada Camada

- **Domain**: entidades, objetos de valor e regras de negócio.
- **Application**: casos de uso (_use cases_), interfaces (ex.: `IUserService`) e DTOs.
- **Infrastructure**: implementações de repositório, EF Core DbContext, configurações de migração, integração com PostgreSQL.

## Configuração do Banco de Dados

O projeto **Database** centraliza:
- **DbContext Factories**: fábricas de tempo de design para criação de contextos nos comandos `dotnet ef`.
- **Migrations**: todas as migrações de schema ficam neste assembly.
- **appsettings.json**: configurações de conexão, normalmente chamada `DefaultConnection` apontando para PostgreSQL.

### 3. Criando uma Migração

Para criar uma nova migração, use o seguinte comando:

```bash
dotnet ef migrations add NomeDaMigracao --context UserDbContext --project src/Database --output-dir "Migrations/UserDbContext" --startup-project src/Presentation
```

#### Parâmetros do Comando
- `NomeDaMigracao`: Nome descritivo da migração (por exemplo, `AddUserTable`).
- `--context UserDbContext`: Especifica o `DbContext` que será usado para a migração.
- `--project src/Database`: Indica o projeto onde as migrações serão armazenadas.
- `--output-dir "Migrations/UserDbContext"`: Define a pasta onde os arquivos de migração serão criados.
- `--startup-project src/Presentation`: Especifica o projeto de inicialização (onde a connection string está configurada).

#### Exemplo
```bash
dotnet ef migrations add AddUserTable --context UserDbContext --project src/Database --output-dir "Migrations/UserDbContext" --startup-project src/Presentation
```

O comando gera:
- Um novo arquivo de migração em `src/Database/Migrations/UserDbContext/`.
- Classes `AddUserTable` e `UserDbContextModelSnapshot`.

### 4. Aplicando a Migração no Banco de Dados

Para aplicar a migração criada ao banco de dados, execute:

```bash
dotnet ef database update --context UserDbContext --project src/Database --startup-project src/Presentation
```

#### Parâmetros do Comando
- `--context UserDbContext`: Especifica o `DbContext` usado para aplicar a migração.
- `--project src/Database`: Projeto onde estão as migrações.
- `--startup-project src/Presentation`: Projeto de inicialização com a connection string.

Executar esse comando atualiza o schema do banco conforme definido na migração.

## Inicialização e Injeção de Dependência (`Program.cs`) (`Program.cs`)

```csharp
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpContextAccessor();
builder.Services.AddMediator();
builder.Services.AddDatabase(builder.Configuration);
builder.Services.AddUsersModule(builder.Configuration);
builder.Services.AddProductModule(builder.Configuration);
builder.Services.AddCartModule(builder.Configuration);
builder.Services.AddOrderModule(builder.Configuration);
builder.Services.AddNotificationModule(builder.Configuration);
builder.Services.AddAuthenticationModule(builder.Configuration);
builder.Services.AddExceptionHandling();

builder.Services.AddAuthorization();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseExceptionHandler();
app.UseAuthentication();
app.UseAuthorization();

app.MapUserRoutes();
app.MapProductRoutes();
app.MapCartRoutes();
app.MapOrdersRoutes();
app.MapNotificationRoutes();

app.Run();
```

- Cada método de extensão `AddXModule` configura serviços específicos do módulo.
- Middlewares de autenticação, autorização e tratamento de exceções estão centralizados.
- Rotas mapeadas via `app.Map{Module}Routes()`.

## Testes

- **UnitTests**: testam regras de negócio isoladas.
- **IntegrationTests**: validam fluxo completo contra banco (in-memory ou contêiner).

## Containerização

- **Dockerfile**: define build e runtime da API.
- **docker-compose.yml**: orquestra API e container PostgreSQL.

## Como Executar

1. Clonar repositório:
   ```bash
   git clone https://github.com/JoaoVitorAguiar/shop-ease.git
   cd shop-ease
   ```
2. Atualizar `appsettings.json` com `DefaultConnection`.
3. Restaurar e compilar:
   ```bash
   dotnet restore
   dotnet build
   ```
4. Aplicar migrations:
   ```bash
   dotnet ef database update --project src/Database/Database.csproj
   ```
5. Executar API:
   ```bash
   dotnet run --project src/Presentation/Presentation.csproj
   ```
6. Executar com Docker:
   ```bash
   docker-compose up --build
   ```


