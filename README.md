# AI Planning Lab

AI Planning Lab is a modular framework for experimenting with planning algorithms and state representations.

The project focuses on:

- Planning-first design
- Clean and lightweight architecture
- Cross-platform execution
- Extensible representations
- Readability and experimentation

---

## Goals

- Implement classical planning algorithms
- Support deterministic and nondeterministic planning
- Explore symbolic planning
- Compare state representations
- Serve as a research and experimentation environment

---

## Architecture

```text
Domain
→ rules and models

Application
→ algorithms

Infrastructure
→ parser and utilities

Implementations
→ concrete state representations
```

Architecture style:

- Lightweight DDD
- Ports & Adapters
- Strategy
- SOLID without overengineering

---

## Current Representations

Planned:

- Graph
- Explicit State
- BDD
- SAT

---

## Setup

Restore packages:

```bash
dotnet restore
```

Build:

```bash
dotnet build
```

Run:

```bash
dotnet run --project src/AIPlanningLab.Cli
```

Test:

```bash
dotnet test
```

---

## Roadmap

### Phase 1
- Core abstractions
- State model
- Search algorithms

### Phase 2
- Parser
- Classical planning

### Phase 3
- Symbolic planning

### Phase 4
- Benchmarking
- IPC support

---

## License

MIT