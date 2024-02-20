#  Ploomes Tech Test
# Project Overview

## Tickets CRUD
## Fluent Validations
## Migrations & Db Versioning

Standard EF Best practices for Db Evolution

## Hexagonal Architecture
 
This projects use most of the concepts from hexagonal architecure, even though it's not strictlly followed as a silver bullet, assuming some couplings aren't necessarily bad. However, all project business rules are contained in Domain, normally via interfaces, that are injected later on by WebAPI DI Container.

# Infra - How to's
## SQL Server - Docker Compose
Docker compose that puts a valid SQLServer container instance (Windows Terminal Format)
```bash
cd .\docker-compose\
docker-compose up
```
### Create migrations
Create a new Db Migration Based on EF model configuration
```bash
cd .\Ploomes-Test.Infrastructure\
```
```bash
dotnet ef migrations add InitialCreate --startup-project ..\Ploomes-Test.WebAPI
```
#### Applying on database
Apply migrations on Database
```bash
dotnet ef database update  --startup-project ..\PloomesTest.WebAPI
```

# Todo
- [ ] Assert Basic Functionality
- [ ] OnModel Configuring to map Offset DateTimes
- [ ] Assert better validation