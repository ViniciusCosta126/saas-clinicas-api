# 🔶 CHECKLIST - FASE ALTA (Semana 2)

## Status: Pendente

---

## 6. Criar Controllers para Patient e Professional
- [ ] Implementar `Controllers/Patient.cs` com CRUD completo
- [ ] Implementar `Controllers/Professional.cs` com CRUD completo
- [ ] Seguir padrão do `ClinicController`
- [ ] Usar DTOs criados na fase crítica
- [ ] Implementar soft delete
- [ ] Adicionar AutoMapper profiles

**Esforço**: 2-3 horas
**Impacto**: ALTO - Funcionalidade essencial

---

## 7. Implementar Global Exception Handler
- [ ] Criar middleware de tratamento de erros
- [ ] Retornar respostas padronizadas em JSON
- [ ] Logar exceções
- [ ] Não expor stack traces em produção

**Arquivo**: `Middleware/ExceptionHandlingMiddleware.cs`
**Esforço**: 1 hora
**Impacto**: ALTO - Robustez

---

## 8. Adicionar Validação de Negócio
- [ ] Implementar FluentValidation
- [ ] Validar CPF único
- [ ] Validar Email único
- [ ] Validar relacionamentos
- [ ] Remover validações duplicadas dos DTOs

**Esforço**: 1-2 horas
**Impacto**: ALTO - Integridade de dados

---

## 9. Remover Validação Duplicada
- [ ] Centralizar validações em Models ou FluentValidation
- [ ] Remover `[Required]`, `[StringLength]` dos DTOs
- [ ] Manter apenas em Models

**Esforço**: 30 min
**Impacto**: MÉDIO - DRY principle

---

## 10. Adicionar Índices e Constraints
- [ ] Email único em `User`
- [ ] Email único em `Patient`
- [ ] Email único em `Professional`
- [ ] CPF único em `User`
- [ ] CPF único em `Patient`
- [ ] Criar nova migration

**Esforço**: 30 min
**Impacto**: ALTO - Integridade de dados

---

## Resumo da Fase Alta
- **Total de Tarefas**: 5
- **Tarefas Concluídas**: 0
- **Progresso**: 0%
- **Tempo Estimado**: 5-6 horas
- **Prioridade**: 🟠 ALTA
