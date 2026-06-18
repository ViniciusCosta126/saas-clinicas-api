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
   - Cria uma nova clínica e usuário
   - Retorna JWT token

2. **Login**: `POST /api/auth/login`
   - Autentica usuário com email e senha
   - Retorna JWT token

3. **Autorização**: Endpoints protegidos com `[Authorize]`
   - Requer Bearer token no header

### Configuração JWT

```json
{
  "Jwt": {
    "Key": "sua-chave-secreta-aqui",
    "Issuer": "SaasClinicas",
    "Audience": "SaasClinicasUsers"
  }
}
```

---

## 🔑 Recursos Principais

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

## 🔒 Segurança

### Implementações de Segurança

1. **Autenticação JWT**
   - Tokens com expiração de 2 horas
   - Validação de issuer, audience e lifetime

2. **Hash de Senhas**
   - BCrypt com salt automático
   - Senhas nunca são expostas em responses

3. **Soft Delete**
   - Registros não são deletados, apenas marcados como deletados
   - Campo `DeletedAt` em todas as entidades

4. **Validações de Entrada**
   - Data Annotations em DTOs
   - Validação de email, CPF, telefone

5. **Autorização**
   - `[Authorize]` em endpoints protegidos
   - Controle de acesso por role

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
- [ ] Tratamento global de erros (Middleware)
- [ ] Validações de negócio (FluentValidation)
- [ ] Índices e constraints no banco
- [ ] Repository Pattern
- [ ] Logging centralizado
- [ ] Rate limiting
- [ ] CORS configurado

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

**Última atualização**: Junho 2026
