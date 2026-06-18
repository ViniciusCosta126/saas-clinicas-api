# ✅ CHECKLIST - FASE CRÍTICA (Semana 1)

## Status: Em Progresso

---

## 1. Corrigir DbContext
- [x] Adicionar `DbSet<User>` em `ApplicationDbContext.cs`
- [x] Adicionar `DbSet<Professional>` em `ApplicationDbContext.cs`
- [x] Configurar relacionamentos em `OnModelCreating`
- [x] Testar se queries funcionam

**Arquivo**: `Data/ApplicationDbContext.cs`
**Esforço**: 10 min
**Impacto**: CRÍTICO - Bloqueante para outras tarefas

---

## 2. Implementar Controller User Completo
- [x] Criar DTOs: `UserCreateDto`, `UserResponseDto`, `UserUpdateDto`
- [x] Reescrever `Controllers/User.cs` com CRUD completo
- [x] Remover lista estática
- [x] Integrar com `ApplicationDbContext`
- [x] Corrigir rota para `[Route("api/users")]`
- [x] Implementar soft delete

**Arquivo**: `Controllers/User.cs`
**Esforço**: 1-2 horas
**Impacto**: CRÍTICO - Funcionalidade essencial

---

## 3. Criar DTOs para User, Patient, Professional
- [x] Criar pasta `Dtos/Users/` com Create, Response, Update
- [x] Criar pasta `Dtos/Patients/` com Create, Response, Update
- [x] Criar pasta `Dtos/Professionals/` com Create, Response, Update
- [x] Não expor senhas em responses
- [x] Validações básicas nos DTOs

**Esforço**: 1 hora
**Impacto**: ALTO - Segurança e consistência

---

## 4. Implementar Autenticação Básica
- [x] Adicionar hash de senhas (BCrypt)
- [x] Criar endpoint `/api/auth/login`
- [x] Criar endpoint `/api/auth/register`
- [x] Implementar JWT ou Identity
- [x] Proteger endpoints com `[Authorize]`

**Esforço**: 2-3 horas
**Impacto**: CRÍTICO - Segurança

---

## 5. Corrigir Namespaces
- [x] Padronizar todos para `SaasClinicas.Api` (sem "P" maiúsculo)
- [x] Atualizar `Program.cs`
- [x] Atualizar todos os arquivos

**Esforço**: 30 min
**Impacto**: ALTO - Evita confusão futura

---

## Resumo da Fase Crítica
- **Total de Tarefas**: 5
- **Tarefas Concluídas**: 5
- **Progresso**: 100%
- **Tempo Estimado**: 4-5 horas
- **Prioridade**: 🔴 MÁXIMA
