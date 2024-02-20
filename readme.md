#  Ploomes Tech Test
## Project Overview
### Docker Compose
```bash
cd Docker-Compose
```
```bash
docker-compose up
```
### Use / Create migrations
```bash
cd Ploomes-Test.Infrastructure
```
```bash
dotnet ef migrations add InitialCreate --startup-project ..\Ploomes-Test.WebAPI
```
```bash
dotnet ef database update  --startup-project ../PloomesTest.WebAPI
```

### Todo
Onmodel Configuring to map Offset DateTimes