# AI USAGE

## Objetivo

Explicar como ferramentas de IA foram utilizadas durante o desenvolvimento do projeto.

Todo código, arquitetura e decisões finais foram revisados e adaptados manualmente antes de serem incorporados.

A IA foi utilizada como ferramenta de apoio para brainstorming, revisão e prototipação.

---

## Uso da IA

### Arquitetura

Auxílio na definição da arquitetura inicial do projeto.

Incluiu discussões sobre:

* organização da Solution
* separação em Domain/Application/Infrastructure/Implementations
* aplicação de SOLID
* Ports & Adapters
* inversão de dependência
* responsabilidades de interfaces

Todo o desenho foi revisado e modificado manualmente.

---

### Modelagem do domínio

Discussão sobre abstrações do planejamento clássico.

Incluiu:

* IAction
* IState
* IDomain
* IPlanningProblem
* IPlanningOperator
* Registry
* BitSet
* representação de estados

O foco foi encontrar uma API independente da implementação (Explicit, BDD, etc.).

---

### Conversão de código legado

Partes do projeto Java ([github](https://github.com/HenrickyL/AI-Planner-BDD)) original foram utilizadas como referência.

A IA auxiliou na:

* interpretação do código
* identificação das responsabilidades
* proposta de refatoração
* conversão Java → C#

Todo o código convertido foi revisado manualmente.

---

### Parser

Auxílio na implementação do parser do formato utilizado pelo projeto original.

Incluiu:

* leitura do arquivo
* separação das seções
* construção do PlanningProblem
* geração das ações
* construção dos estados

---

### Estruturas de dados

Discussão e implementação de estruturas auxiliares.

Incluiu:

* BitSet
* PropositionRegistry
* operações bitwise
* representação explícita de estados

---

### Documentação

Auxílio na produção de documentação técnica.

Incluiu:

* README
* comentários XML
* organização de diretórios
* exemplos de uso

---

### Revisão técnica

A IA também foi utilizada para:

* revisão de código
* sugestões de refatoração
* identificação de simplificações
* discussão de alternativas de implementação
* comparação entre diferentes abordagens

---

## Responsabilidade

As respostas fornecidas pela IA foram tratadas como sugestões.

Todo código incorporado ao projeto foi:

* revisado
* adaptado
* testado

A responsabilidade pelas decisões arquiteturais e pela implementação final é exclusivamente do autor do projeto.

---

## Ferramentas

* GPT-5.5 (OpenAI)
* Claude Sonnet 5 (Anthropic )
