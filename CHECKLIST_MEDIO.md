# 🟡 CHECKLIST - FASE MÉDIA (Semana 3)

## Status: Pendente

---

## 11. Implementar Repository Pattern
- [x] Criar interface `IRepository<T>`
- [x] Implementar `Repository<T>` genérico
- [x] Remover acesso direto ao `DbContext` dos controllers
- [x] Injetar `IRepository<T>` nos controllers
- [x] Facilitar testes unitários

**Esforço**: 2-3 horas
**Impacto**: MÉDIO - Testabilidade e manutenção

---

## 12. Adicionar Logging
- [ ] Implementar Serilog ou `Microsoft.Extensions.Logging`
- [ ] Logs estruturados
- [ ] Rastreamento de operações críticas (Create, Update, Delete)
- [ ] Logs de erro com contexto
- [ ] Configurar níveis de log por ambiente

**Esforço**: 1-2 horas
**Impacto**: ALTO - Debugging em produção

---

## 13. Implementar Testes
- [ ] Criar projeto de testes (xUnit ou NUnit)
- [ ] Testes unitários para validações
- [ ] Testes de integração para endpoints
- [ ] Cobertura mínima de 70%
- [ ] Testes para soft delete

**Esforço**: 3-4 horas
**Impacto**: ALTO - Confiabilidade

---

## 14. Adicionar Soft Delete Global
- [ ] Criar extension method `IncludeDeleted<T>()`
- [ ] Usar em todos os Gets
- [ ] Centralizar filtro em base repository
- [ ] Garantir consistência

**Esforço**: 30 min
**Impacto**: MÉDIO - Consistência

---

## 15. Refatorar Controllers
- [ ] Remover duplicação de código
- [ ] Centralizar soft delete filtering
- [ ] Usar padrão consistente em todos
- [ ] Adicionar logging em operações críticas

**Esforço**: 1-2 horas
**Impacto**: MÉDIO - Manutenibilidade

---

## Resumo da Fase Média
- **Total de Tarefas**: 5
- **Tarefas Concluídas**: 0
- **Progresso**: 0%
- **Tempo Estimado**: 8-10 horas
- **Prioridade**: 🟡 MÉDIA
