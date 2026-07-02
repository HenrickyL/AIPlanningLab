# Code

## start
`dotnet new sln -n AIPlanningLab`

## projects 
`dotnet new classlib -n AIPlanningLab.Domain -o src/AIPlanningLab.Domain`

`dotnet new classlib -n AIPlanningLab.Application -o src/AIPlanningLab.Application`

`dotnet new classlib -n AIPlanningLab.Implementations -o src/AIPlanningLab.Implementations`

`dotnet new classlib -n AIPlanningLab.Infrastructure -o src/AIPlanningLab.Infrastructure`

`dotnet new console -n AIPlanningLab.Cli -o src/AIPlanningLab.Cli`

`dotnet new xunit -n AIPlanningLab.Tests -o tests/AIPlanningLab.Tests`

## Add to solution

```
dotnet sln add src/AIPlanningLab.Domain

dotnet sln add src/AIPlanningLab.Application

dotnet sln add src/AIPlanningLab.Implementations

dotnet sln add src/AIPlanningLab.Infrastructure

dotnet sln add src/AIPlanningLab.Cli

dotnet sln add tests/AIPlanningLab.Tests
```

## refs

```
dotnet add src/AIPlanningLab.Application reference src/AIPlanningLab.Domain

dotnet add src/AIPlanningLab.Implementations reference src/AIPlanningLab.Domain

dotnet add src/AIPlanningLab.Implementations reference src/AIPlanningLab.Application

dotnet add src/AIPlanningLab.Implementations reference src/AIPlanningLab.Infrastructure

dotnet add src/AIPlanningLab.Infrastructure reference src/AIPlanningLab.Domain

dotnet add src/AIPlanningLab.Cli reference src/AIPlanningLab.Application

dotnet add src/AIPlanningLab.Cli reference src/AIPlanningLab.Infrastructure

dotnet add src/AIPlanningLab.Cli reference src/AIPlanningLab.Implementations

dotnet add tests/AIPlanningLab.Tests reference src/AIPlanningLab.Application
```

## Generate gitignore

`dotnet new gitignore`

## First Run

`dotnet restore`
`dotnet build`
`dotnet run --project src/AIPlanningLab.Cli`