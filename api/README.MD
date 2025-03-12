## **Documentação do Processo de Migração**

Este guia explica como criar e aplicar migrações no projeto, utilizando o **Entity Framework Core (EF Core)**. O processo é dividido em duas etapas principais:
1. **Criar uma Migração**.
2. **Aplicar a Migração no Banco de Dados**.

Os comandos devem ser executados no diretório raiz do projeto (`/api`), onde a solução (`sln`) e os projetos estão localizados.

---

### **1. Estrutura do Projeto**

Aqui está a estrutura do projeto relevante para o processo de migração:

```
api/
├── src/
│   ├── Presentation/          # Projeto de inicialização (API, MVC, etc.)
│   ├── Database/              # Projeto de banco de dados (contém as migrações)
│   ├── Users/                 # Módulo de usuários
│   │   └── Infrastructure/    # Contém o UserDbContext
│   └── Products/              # Módulo de produtos
└── ...
```

---

### **2. Pré-requisitos**

Antes de executar os comandos de migração, certifique-se de que:
1. O **EF Core CLI** está instalado globalmente:
   ```bash
   dotnet tool install --global dotnet-ef
   ```
2. Os pacotes necessários estão instalados no projeto `Database`:
   - `Microsoft.EntityFrameworkCore.Design`
   - `Microsoft.EntityFrameworkCore.Tools`
   - `Npgsql.EntityFrameworkCore.PostgreSQL` (ou o provedor do banco de dados que você está usando).

3. O arquivo `appsettings.json` no projeto `Presentation` contém a connection string correta para o banco de dados.

---

### **3. Criando uma Migração**

Para criar uma nova migração, use o seguinte comando:

```bash
dotnet ef migrations add NomeDaMigracao --context UserDbContext --project src/Database --output-dir "Migrations/UserDbContext" --startup-project src/Presentation
```

#### **Parâmetros do Comando**
- **`NomeDaMigracao`**: Nome descritivo da migração (por exemplo, `AddUserTable`).
- **`--context UserDbContext`**: Especifica o `DbContext` que será usado para a migração.
- **`--project src/Database`**: Indica o projeto onde as migrações serão armazenadas.
- **`--output-dir "Migrations/UserDbContext"`**: Define a pasta onde os arquivos de migração serão criados.
- **`--startup-project src/Presentation`**: Especifica o projeto de inicialização (onde a connection string está configurada).

#### **Exemplo**
```bash
dotnet ef migrations add AddUserTable --context UserDbContext --project src/Database --output-dir "Migrations/UserDbContext" --startup-project src/Presentation
```

#### **O que Acontece?**
- Um novo arquivo de migração será criado na pasta `src/Database/Migrations/UserDbContext/`.
- O arquivo contém duas classes:
  - `NomeDaMigracao`: Contém as alterações no esquema do banco de dados.
  - `UserDbContextModelSnapshot`: Um snapshot do modelo atual do banco de dados.

---

### **4. Aplicando a Migração no Banco de Dados**

Após criar a migração, você precisa aplicá-la ao banco de dados. Use o seguinte comando:

```bash
dotnet ef database update --context UserDbContext --project src/Database --startup-project src/Presentation
```

#### **Parâmetros do Comando**
- **`--context UserDbContext`**: Especifica o `DbContext` que será usado para aplicar a migração.
- **`--project src/Database`**: Indica o projeto onde as migrações estão armazenadas.
- **`--startup-project src/Presentation`**: Especifica o projeto de inicialização (onde a connection string está configurada).

#### **O que Acontece?**
- O EF Core aplica as migrações pendentes ao banco de dados.
- As tabelas e colunas são criadas ou alteradas conforme definido na migração.

---

### **5. Verificando o Banco de Dados**

Após aplicar a migração, verifique se as alterações foram aplicadas corretamente no banco de dados. Você pode usar uma ferramenta como **pgAdmin** (para PostgreSQL) ou executar consultas SQL diretamente.

#### **Exemplo de Consulta**
```sql
SELECT * FROM users;
```

---

### **6. Fluxo Completo de Migração**

Aqui está um exemplo completo do fluxo de migração:

1. **Criar a Migração**:
   ```bash
   dotnet ef migrations add AddUserTable --context UserDbContext --project src/Database --output-dir "Migrations/UserDbContext" --startup-project src/Presentation
   ```

2. **Aplicar a Migração**:
   ```bash
   dotnet ef database update --context UserDbContext --project src/Database --startup-project src/Presentation
   ```

3. **Verificar o Banco de Dados**:
   - Use uma ferramenta de banco de dados para confirmar que as tabelas e colunas foram criadas corretamente.

---

### **7. Dicas e Boas Práticas**

- **Nomes Descritivos**: Use nomes descritivos para as migrações (por exemplo, `AddUserTable`, `UpdateProductPrice`).
- **Testes**: Sempre teste as migrações em um ambiente de desenvolvimento antes de aplicá-las em produção.
- **Backup**: Faça backup do banco de dados antes de aplicar migrações em produção.
- **Documentação**: Documente as migrações e suas alterações para facilitar a manutenção.

---

### **8. Problemas Comuns e Soluções**

#### **Erro: "No database provider has been configured"**
- **Causa**: O `DbContext` não está configurado para usar um provedor de banco de dados.
- **Solução**: Certifique-se de que o `UseNpgsql` (ou outro provedor) está configurado no `OnConfiguring` ou no `Program.cs`.

#### **Erro: "Table 'users' already exists"**
- **Causa**: A tabela já foi criada em uma migração anterior.
- **Solução**: Exclua a tabela manualmente ou crie uma nova migração para atualizar o esquema.

#### **Erro: "Cannot resolve symbol 'ToTable'"**
- **Causa**: Falta de referência ao pacote `Microsoft.EntityFrameworkCore`.
- **Solução**: Adicione a referência ao pacote no `.csproj`.
