#  Ploomes Tech Test

# Deployment on Azure

# Project Overview

Fairly simple Tickets CRUD for [Ploomes](https://www.ploomes.com) test, basic business rules, (mostly) hexagonal with a model that doesn't  really depends on anything else besides the small `FluentResult` library, other dependencies are abstracted into the model itself by using the ports, which are not named this way for personal preference.

Business rules are found in the rich domain model itself. e.g: Completed tickets cannot be assigned, cannot cancel a completed ticket, et cetera.

Returning errors are of type ProblemDetail, defined in RFC 7808. ModelState errors are mapped directly in ProblemDetailsModelStateFilter, other are mapped from Result.Fail to ProblemDetails.

Docker-Compose contained for infra.

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
