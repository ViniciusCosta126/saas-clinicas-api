# SaasClinicas API

Uma API REST robusta e escalável para gerenciamento de clínicas, desenvolvida com **ASP.NET Core 8.0** e **Entity Framework Core**.

## 📋 Visão Geral

SaasClinicas é uma plataforma SaaS (Software as a Service) que permite o gerenciamento completo de clínicas, incluindo:

- **Gestão de Clínicas**: Cadastro e administração de múltiplas clínicas
- **Gerenciamento de Usuários**: Controle de acesso com autenticação JWT
- **Cadastro de Pacientes**: Registro de pacientes por clínica
- **Gestão de Profissionais**: Cadastro de profissionais de saúde
- **Autenticação Segura**: JWT com hash de senhas (BCrypt)

---

## 🚀 Tecnologias Utilizadas

| Tecnologia | Versão | Propósito |
|-----------|--------|----------|
| ASP.NET Core | 8.0 | Framework web |
| Entity Framework Core | 8.0.17 | ORM para banco de dados |
| SQLite | - | Banco de dados |
| AutoMapper | 13.0.1 | Mapeamento de objetos |
| BCrypt.Net | 0.1.0 | Hash de senhas |
| JWT (System.IdentityModel.Tokens.Jwt) | - | Autenticação |
| FluentValidation | 11.9.2 | Validação de dados |
| Swashbuckle | 6.4.0 | Documentação Swagger |

---

## 📁 Estrutura do Projeto

```
SaasClinicas.Api/
├── Controllers/          # Endpoints da API
│   ├── AuthController.cs
│   ├── ClinicController.cs
│   ├── UserController.cs
│   ├── PatientController.cs
│   └── ProfessionalController.cs
├── Models/              # Modelos de domínio
│   ├── Base/
│   │   └── BaseEntity.cs
│   ├── Clinic.cs
│   ├── User.cs
│   ├── Patient.cs
│   └── Professional.cs
├── Dtos/                # Data Transfer Objects
│   ├── Auth/
│   ├── Clinics/
│   ├── Users/
│   ├── Patients/
│   └── Professionals/
├── Services/            # Serviços de negócio
│   ├── IPasswordHashService.cs
│   ├── PasswordHashService.cs
│   ├── ITokenService.cs
│   └── TokenService.cs
├── Mappings/            # Perfis do AutoMapper
│   ├── ClinicProfile.cs
│   ├── UserProfile.cs
│   ├── PatientProfile.cs
│   └── ProfessionalProfile.cs
├── Data/                # Contexto do banco
│   └── ApplicationDbContext.cs
├── Repositories/        # Repository Pattern
│   ├── IRepository.cs
│   └── Repository.cs
├── Validators/          # FluentValidation
│   ├── Auth/
│   ├── Clinics/
│   ├── Users/
│   ├── Patients/
│   └── Professionals/
├── Middleware/          # Middlewares customizados
│   └── ExceptionMiddleware.cs
├── Migrations/          # Migrações do EF Core
├── Enums/               # Enumerações
│   └── UserRoles.cs
├── Program.cs           # Configuração da aplicação
├── appsettings.json     # Configurações
└── README.md            # Este arquivo
```

---

## 🔐 Autenticação e Autorização

### Fluxo de Autenticação

1. **Registro**: `POST /api/auth/register`
   - Cria uma nova clínica e usuário em uma transação
   - Retorna JWT token com IDs da clínica e usuário

2. **Login**: `POST /api/auth/login`
   - Autentica usuário com email e senha
   - Verifica hash de senha com BCrypt
   - Retorna JWT token

3. **Autorização**: Endpoints protegidos com `[Authorize]`
   - Requer Bearer token no header
   - Token válido por 2 horas

### Configuração JWT

```json
{
  "Jwt": {
    "Key": "sua-chave-secreta-aqui-minimo-32-caracteres",
    "Issuer": "SaasClinicas",
    "Audience": "SaasClinicasUsers"
  }
}
```

### Usando Bearer Token no Swagger

1. Clique no botão **"Authorize"** no canto superior direito
2. Cole o token JWT recebido no login/registro
3. Formato: `Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...`
4. Clique em "Authorize"
5. Agora todos os endpoints protegidos funcionarão no Swagger

---

## �️ Tratamento de Erros Global

A API implementa um middleware centralizado para tratamento de exceções:

### Resposta de Erro Padrão

```json
{
  "success": false,
  "message": "Ocorreu um erro no servidor.",
  "error": "Attempted to divide by zero.",
  "timeStamp": "2026-06-19T10:49:00Z",
  "traceId": "0HN1GKDQNKQ5V:00000001"
}
```

### Comportamento por Ambiente

- **Development**: Expõe mensagem de erro detalhada
- **Production**: Oculta detalhes técnicos (apenas mensagem genérica)

### Códigos HTTP Retornados

| Exceção | Status HTTP |
|---------|------------|
| `UnauthorizedAccessException` | 401 Unauthorized |
| `KeyNotFoundException` | 404 Not Found |
| `ArgumentException` | 400 Bad Request |
| `InvalidOperationException` | 400 Bad Request |
| Outras exceções | 500 Internal Server Error |

---

## � Recursos Principais

### 1. Autenticação
- **POST** `/api/auth/login` - Login de usuário
- **POST** `/api/auth/register` - Registro de nova clínica + usuário

### 2. Clínicas
- **GET** `/api/clinics` - Listar clínicas
- **GET** `/api/clinics/{id}` - Obter clínica por ID
- **POST** `/api/clinics` - Criar clínica
- **PUT** `/api/clinics/{id}` - Atualizar clínica
- **DELETE** `/api/clinics/{id}` - Deletar clínica (soft delete)

### 3. Usuários
- **GET** `/api/users` - Listar usuários
- **GET** `/api/users/{id}` - Obter usuário por ID
- **POST** `/api/users` - Criar usuário
- **PUT** `/api/users/{id}` - Atualizar usuário
- **DELETE** `/api/users/{id}` - Deletar usuário (soft delete)

### 4. Pacientes
- **GET** `/api/patients` - Listar pacientes
- **GET** `/api/patients/{id}` - Obter paciente por ID
- **POST** `/api/patients` - Criar paciente
- **PUT** `/api/patients/{id}` - Atualizar paciente
- **DELETE** `/api/patients/{id}` - Deletar paciente (soft delete)

### 5. Profissionais
- **GET** `/api/professionals` - Listar profissionais
- **GET** `/api/professionals/{id}` - Obter profissional por ID
- **POST** `/api/professionals` - Criar profissional
- **PUT** `/api/professionals/{id}` - Atualizar profissional
- **DELETE** `/api/professionals/{id}` - Deletar profissional (soft delete)

---

## 🛠️ Instalação e Configuração

### Pré-requisitos
- .NET 8.0 SDK
- SQLite (incluído no projeto)

### Passos

1. **Clone o repositório**
```bash
git clone <url-do-repositorio>
cd SaasClinicas.Api
```

2. **Restaure as dependências**
```bash
dotnet restore
```

3. **Configure o banco de dados**
```bash
dotnet ef database update
```

4. **Execute a aplicação**
```bash
dotnet run
```

5. **Acesse o Swagger**
```
https://localhost:7000/swagger
```

---

## 📝 Exemplos de Uso

### 1. Registrar Nova Clínica + Usuário

```bash
POST /api/auth/register
Content-Type: application/json

{
  "clinic": {
    "clinicName": "Clínica XYZ",
    "responsibleName": "João Silva",
    "email": "clinica@email.com",
    "phone": "11999999999"
  },
  "user": {
    "name": "João Silva",
    "email": "joao@email.com",
    "password": "Senha123!",
    "phone": "11999999999",
    "cpf": "12345678901",
    "role": 1
  }
}
```

**Resposta:**
```json
{
  "token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...",
  "clinicId": 1,
  "userId": 5
}
```

### 2. Login

```bash
POST /api/auth/login
Content-Type: application/json

{
  "email": "joao@email.com",
  "password": "Senha123!"
}
```

**Resposta:**
```json
{
  "token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9..."
}
```

### 3. Criar Paciente (Requer Autenticação)

```bash
POST /api/patients
Authorization: Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...
Content-Type: application/json

{
  "name": "Maria Santos",
  "email": "maria@email.com",
  "phone": "11988888888",
  "birthday": "1990-05-15",
  "cpf": "98765432101",
  "clinicId": 1
}
```

---

## ✅ Validações com FluentValidation

A API implementa validações robustas e centralizadas usando FluentValidation:

### Validadores Implementados

1. **UserCreateValidator / UserUpdateValidator**
   - Name: 4-255 caracteres
   - Email: Formato válido e único
   - Phone: 10-11 dígitos
   - CPF: 11 dígitos e único
   - Password: 6-255 caracteres
   - ClinicId: Deve existir no banco

2. **PatientCreateValidator / PatientUpdateValidator**
   - Name: 4-255 caracteres
   - Email: Formato válido e único
   - Phone: 10-11 dígitos
   - CPF: 11 dígitos e único
   - Birthday: Data obrigatória

3. **ProfessionalCreateValidator / ProfessionalUpdateValidator**
   - Name: 4-255 caracteres
   - Email: Formato válido e único
   - SessionPrice: Maior que 0
   - ClinicId: Deve existir no banco

4. **ClinicCreateValidator / ClinicUpdateValidator**
   - ClinicName: 4-255 caracteres
   - ResponsibleName: 4-255 caracteres
   - Email: Formato válido e único
   - Phone: 10-11 dígitos

### Características

- ✅ Validações assíncronas (email/CPF únicos)
- ✅ Validação de relacionamentos
- ✅ Mensagens de erro customizadas
- ✅ Exclusão automática do próprio registro em Updates
- ✅ Registro automático no DI container

---

## 🔒 Segurança

### Implementações de Segurança

1. **Autenticação JWT**
   - Tokens com expiração de 2 horas
   - Validação de issuer, audience e lifetime
   - Suporte a Bearer token no Swagger

2. **Hash de Senhas**
   - BCrypt com salt automático
   - Senhas nunca são expostas em responses
   - Verificação segura com `BCrypt.Verify()`

3. **Soft Delete**
   - Registros não são deletados, apenas marcados como deletados
   - Campo `DeletedAt` em todas as entidades
   - Preserva auditoria de dados
   - Implementado no Repository Pattern

4. **Validações de Entrada**
   - FluentValidation centralizado
   - Validação de email, CPF, telefone
   - Regex para formatos específicos
   - Validações assíncronas

5. **Autorização**
   - `[Authorize]` em endpoints protegidos
   - Controle de acesso por role (Admin, Professional, Receptionist, Financial)
   - Transações para operações críticas

6. **Tratamento de Erros Seguro**
   - Middleware centralizado
   - Não expõe stack traces em produção
   - TraceId para rastreamento
   - Índices únicos no banco de dados

### Checklist de Segurança

- [x] Autenticação JWT implementada
- [x] Senhas com hash BCrypt
- [x] Endpoints protegidos com `[Authorize]`
- [x] Soft delete implementado
- [x] Validações com FluentValidation
- [x] Tratamento de erros seguro
- [x] CPF/Email únicos validados
- [x] Índices únicos no banco de dados
- [ ] HTTPS obrigatório
- [ ] Rate limiting
- [ ] CORS configurado

---

## 📊 Modelos de Dados

### Clinic
```csharp
public class Clinic : BaseEntity
{
    public int Id { get; set; }
    public string ClinicName { get; set; }
    public string ResponsibleName { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    
    // Relacionamentos
    public ICollection<User> Users { get; set; }
    public ICollection<Professional> Professionals { get; set; }
    public ICollection<ClinicPatient> ClinicPatients { get; set; }
}
```

### User
```csharp
public class User : BaseEntity
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public string Cpf { get; set; }
    public string Password { get; set; } // Hash BCrypt
    public UserRole Role { get; set; }
    public int ClinicId { get; set; }
    
    // Relacionamentos
    public Clinic Clinic { get; set; }
}
```

### Patient
```csharp
public class Patient : BaseEntity
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public DateOnly Birthday { get; set; }
    public string Cpf { get; set; }
    
    // Relacionamentos
    public ICollection<ClinicPatient> ClinicPatients { get; set; }
}
```

### Professional
```csharp
public class Professional : BaseEntity
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string Specialty { get; set; }
    public decimal SessionPrice { get; set; }
    public int ClinicId { get; set; }
    
    // Relacionamentos
    public Clinic Clinic { get; set; }
}
```

---

## 🏗️ Arquitetura e Padrões

### Padrões Implementados

1. **DTO Pattern**
   - Separação entre modelos de domínio e API
   - Validações centralizadas com FluentValidation
   - Segurança (não expõe senhas)

2. **Repository Pattern**
   - Interface genérica `IRepository<T>`
   - Implementação `Repository<T>` centralizada
   - Facilita testes unitários
   - Abstração do acesso a dados

3. **AutoMapper**
   - Mapeamento automático entre entidades e DTOs
   - Profiles centralizados
   - Reduz código boilerplate

4. **Dependency Injection**
   - Serviços registrados em Program.cs
   - Loose coupling entre componentes
   - Facilita testes

5. **FluentValidation**
   - Validações centralizadas fora dos DTOs
   - Validações assíncronas (email/CPF únicos)
   - Validação de relacionamentos
   - Mensagens customizadas

6. **Soft Delete**
   - Registros não são deletados, apenas marcados
   - Campo `DeletedAt` em BaseEntity
   - Preserva histórico de dados
   - Implementado no Repository

7. **Middleware de Exceção**
   - Tratamento centralizado de erros
   - Respostas padronizadas
   - Logging automático
   - TraceId para rastreamento

### Camadas da Aplicação

```
┌─────────────────────────────┐
│   Controllers (API)         │
├─────────────────────────────┤
│   Services (Negócio)        │
├─────────────────────────────┤
│   Data Access (EF Core)     │
├─────────────────────────────┤
│   Database (SQLite)         │
└─────────────────────────────┘
```

---

## 🧪 Testes

Testes unitários podem ser adicionados usando xUnit ou NUnit:

```csharp
[Fact]
public async Task Login_WithValidCredentials_ReturnsToken()
{
    // Arrange
    var loginDto = new LoginDto 
    { 
        Email = "test@test.com", 
        Password = "123456" 
    };
    
    // Act
    var result = await _authController.Login(loginDto);
    
    // Assert
    Assert.NotNull(result);
}
```

---

## 📈 Próximas Melhorias

- [ ] Testes unitários (xUnit)
- [x] Tratamento global de erros (Middleware)
- [x] Validações de negócio (CPF/Email únicos)
- [x] Índices e constraints no banco
- [x] Repository Pattern
- [x] FluentValidation para validações complexas
- [ ] Logging centralizado (Serilog)
- [ ] Rate limiting
- [ ] CORS configurado
- [ ] API versioning

---

## 📄 Licença

Este projeto é fornecido como está, sem garantias.

---

## 👨‍💻 Autor

Desenvolvido como projeto de aprendizado em ASP.NET Core.

---

## 📞 Suporte

Para dúvidas ou sugestões, abra uma issue no repositório.

---

---

## 📊 Status do Projeto

### Fase Crítica ✅ (100% Completa)
- [x] DbContext com todos os DbSets
- [x] Controllers CRUD (User, Patient, Professional, Clinic)
- [x] DTOs para todas as entidades
- [x] Autenticação JWT com Login/Register
- [x] Namespaces padronizados

### Fase Alta ✅ (100% Completa)
- [x] Controllers Patient e Professional
- [x] Global Exception Handler
- [x] Validações de negócio (CPF/Email únicos)
- [x] FluentValidation implementado
- [x] Remover validações duplicadas
- [x] Índices e constraints no banco

### Fase Média 🟡 (20% Completa)
- [x] Repository Pattern
- [ ] Logging centralizado
- [ ] Testes unitários
- [ ] Global soft delete
- [ ] Refatoração de controllers

### Fase Baixa 🟢 (0% Completa)
- [ ] API versioning
- [ ] CORS e segurança avançada
- [ ] Seed data e migrations
- [ ] Documentação completa
- [ ] Performance optimizations

---

**Última atualização**: Junho 2026
