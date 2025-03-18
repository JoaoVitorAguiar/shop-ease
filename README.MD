```shell
dotnet sln ECommerce.sln add src/Modules/Users/Users.Domain/Users.Domain.csproj
dotnet sln ECommerce.sln add src/Modules/Users/Users.Application/Users.Application.csproj
dotnet sln ECommerce.sln add src/Modules/Users/Users.Infrastructure/Users.Infrastructure.csproj

dotnet sln ECommerce.sln add src/Modules/Products/Products.Domain/Products.Domain.csproj
dotnet sln ECommerce.sln add src/Modules/Products/Products.Application/Products.Application.csproj
dotnet sln ECommerce.sln add src/Modules/Products/Products.Infrastructure/Products.Infrastructure.csproj

dotnet sln ECommerce.sln add src/Modules/Cart/Cart.Domain/Cart.Domain.csproj
dotnet sln ECommerce.sln add src/Modules/Cart/Cart.Application/Cart.Application.csproj
dotnet sln ECommerce.sln add src/Modules/Cart/Cart.Infrastructure/Cart.Infrastructure.csproj

dotnet sln ECommerce.sln add src/Modules/Orders/Orders.Domain/Orders.Domain.csproj
dotnet sln ECommerce.sln add src/Modules/Orders/Orders.Application/Orders.Application.csproj
dotnet sln ECommerce.sln add src/Modules/Orders/Orders.Infrastructure/Orders.Infrastructure.csproj

dotnet sln ECommerce.sln add src/Modules/Notifications/Notifications.Domain/Notifications.Domain.csproj
dotnet sln ECommerce.sln add src/Modules/Notifications/Notifications.Application/Notifications.Application.csproj
dotnet sln ECommerce.sln add src/Modules/Notifications/Notifications.Infrastructure/Notifications.Infrastructure.csproj

dotnet sln ECommerce.sln add src/Presentation/Presentation.csproj
dotnet sln ECommerce.sln add src/Shared/Shared.csproj
dotnet sln ECommerce.sln add src/Database/Database.csproj

dotnet sln ECommerce.sln add tests/UnitTests/UnitTests.csproj
dotnet sln ECommerce.sln add tests/IntegrationTests/IntegrationTests.csproj
```


```
api/
|
├── src/
│   ├── Modules/
│   │   ├── Users/
│   │   │   ├── Users.Domain/
│   │   │   ├── Users.Application/
│   │   │   └── Users.Infrastructure/
│   │   ├── Products/
│   │   │   ├── Products.Domain/
│   │   │   ├── Products.Application/
│   │   │   └── Products.Infrastructure/
│   │   ├── Cart/
│   │   │   ├── Cart.Domain/
│   │   │   ├── Cart.Application/
│   │   │   └── Cart.Infrastructure/
│   │   ├── Orders/
│   │   │   ├── Orders.Domain/
│   │   │   ├── Orders.Application/
│   │   │   └── Orders.Infrastructure/
│   │   └── Notifications/
│   │       ├── Notifications.Domain/
│   │       ├── Notifications.Application/
│   │       └── Notifications.Infrastructure/
│   ├── Presentation/         # API (Minimal APIs ou Controllers)
│   ├── Shared/               # Recursos compartilhados (DTOs, Helpers, etc.)
│   └── Database/             # Configurações do banco de dados
|
├── tests/
│   ├── UnitTests/            # Testes unitários
│   └── IntegrationTests/     # Testes de integração
|
├── Dockerfile                # Configuração do Docker para o back-end
├── docker-compose.yml        # Configuração do Docker Compose para o back-end
└── ECommerce.sln             # Arquivo de solução (.sln)
```