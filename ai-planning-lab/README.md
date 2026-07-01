# AI Planning Lab

Framework para experimentação e pesquisa em Planejamento Automatizado.

O objetivo do projeto é fornecer uma API simples, extensível e desacoplada para representar problemas de planejamento e executar algoritmos clássicos e simbólicos sem depender de uma representação específica de estado.

A arquitetura separa:

- Modelo do problema
- Algoritmos de planejamento
- Representação dos estados
- Entrada/Saída e integração

Isso permite trocar entre:

- Estados explícitos
- BDDs
- Grafos
- Representações futuras

sem alterar o núcleo do planejamento.

---

# Objetivos

- Arquitetura limpa e extensível
- Foco em SOLID sem excesso de abstração
- Separação entre teoria e implementação
- Compatível com Windows e Linux
- Fácil prototipação para pesquisa
- Permitir comparação entre abordagens

Exemplos:

- Planejamento clássico
- Busca cega
- Busca heurística
- Planejamento simbólico
- Planejamento determinístico
- Planejamento não determinístico

---

# Conceitos

Um problema de planejamento é definido por:

Problem = (P, A, I, G)

Onde:

P → proposições

A → ações

I → estado inicial

G → objetivo

Um estado é representado como um conjunto de proposições verdadeiras.

Uma ação modifica estados através de:

- pré-condições
- efeitos positivos
- efeitos negativos

A busca pode ocorrer:

- para frente (progressão)
- para trás (regressão)

---

# Arquitetura

src/

AIPlanningLab.Domain  
→ Modelo do problema

AIPlanningLab.Application  
→ Algoritmos

AIPlanningLab.Infrastructure  
→ Parser e execução

AIPlanningLab.Implementations  
→ Representações concretas

AIPlanningLab.Cli  
→ Interface de execução

---

# Estrutura

src/

Domain/

Application/

Infrastructure/

Implementations/

Cli/

tests/

---

# Exemplo

```csharp
var parser =
new PddlParser();

var problem =
parser.Parse(file);

var planner =
new ForwardPlanner(
    new AStar(),
    new BddPlanningOperator()
);

var result =
planner.Solve(problem);