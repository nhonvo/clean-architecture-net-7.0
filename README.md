# Robusta template

- [Robusta template](#robusta-template)
  - [Architecture](#architecture)
  - [Technologies](#technologies)
  - [Dependencies](#dependencies)

Project start day: Wednesday, 14/6/2023

- Week 1: 14 - 16/6/2023 - 3 days - 5 points

| id  | task                                                     | days | points | Process |
| --- | -------------------------------------------------------- | ---- | ------ | ------- |
| 1   | Clear requirements and all technologies needed.          | 1    | 1      | 100%    |
| 2   | build skeleton project: connect postgresql, rest api etc | 1    | 2      | 100%    |
| 3   | Enhance codebase: separate services, refactor code.      | 0.5  | 1      | 100%    |
| 4   | research and set up docker                               | 0.5  | 1      | 100%    |

- Details:
  1. Write documentation [Architect](#architecture), [Technologies](#technologies),
   [Dependencies](#dependencies)
  2. set up postgreSQL, rest api, seed data etc
  3. Enhance codebase:
     1. separate to read and write services.
     2. refactor code(remove unused using, move file to correct location)
  4. research docker and build sample project.

- Week 2: 19 - 23/6/2023
| id  | task                                                     | days | points | Process |
| --- | -------------------------------------------------------- | ---- | ------ | ------- |
| 1   | Enhance codebase Program.cs, set up docker               | 1    | 2      | 100%    |
| 2   | Enhance code (fluentAPI, migration), research Splunk log | 1    | 2      | 100%    |
| 3   | set up log, use const,                                   | 1    | 2      | 100%    |

- Details:
  1. Enhance codebase Program.cs, set up docker
     1. Enhance codebase: Program.cs
        1. separate configuration settings in Program.cs
     2. Set up docker: add docker file and docker compose. Connect to postgresql.
        1. Issue: when connecting to postgresql - Solutions: add environment variable, migrate same time docker compose.
  2. Enhance code (fluentAPI, migration), research Splunk log
     1. Enhance code (fluentAPI, migration)
        1. add Persistence for fluentAPI configuration
        2. and ApplicationDbContextInitialize migration when application run
     2. research Splunk log
        1. implement slunk log
        2. Implement Splunk to C# project
        3. Free lic for dev here: <https://dev.splunk.com/enterprise/dev_license>
  3. Enhance splunk configuration
     1. Move configuration program to web api configuration
     2. fix bug server spunk can not get data
     3. use serilog instead of logging
  4. Set up logging, use const
     1. use Nlog and Splunk.
     2. fix hard code(message, ...)

- On Ready to go
  - Week 3:
    - add test project.
    - service test and unit tests
    - add identity user
      - jwt token
      - policy base.
  - Week 4:
    - review code
    - release

## Architecture

- [Image architect full hd](.././Application/MicrosoftTeams-image.png)

```markdown
application
|__api
|   |__ ApplicationLogic
|   |   |__Repositories
|   |   |__ Services
|   |       |__WriteService
|   |       |__ReadService
|   |__Core
|   |   |__ Configuration
|   |   |__Constants
|   |   |__ Entities
|   |   |__Enum
|   |   |__ Extensions
|   |   |__Interfaces
|   |   |__ Messaging
|   |   |__Retry
|   |   |__ Utilities
|   |__Infrastructure
|   |   |__ Postgres
|   |   |__SNS
|   |__ Presentation
|   |   |__Authentication
|   |   |__ Constants
|   |   |__Controllers
|   |   |__ Filters
|   |   |__Mappers
|   |   |__ Validation
|   |__appsettings.json
|   |__ Program.cs
|__Migrations
|__ Models
```

## Technologies

- net v7.0
- postgresql
- docker
- Nlog/Splunk
- redis
- newrelics
- Nginx
- Hangfire                  == separate demo
- NetMQ/SignalR (optional)  == separate demo

## Dependencies

- AutoMapper.Extensions.Microsoft.DependencyInjection  
- FluentValidation  
- FluentValidation.AspNetCore  
- Microsoft.AspNetCore.Authentication.JwtBearer  
- Microsoft.AspNetCore.OpenApi  
- Microsoft.EntityFrameworkCore.Design  
- Microsoft.EntityFrameworkCore.InMemory  
- Microsoft.EntityFrameworkCore.SqlServer  
- Microsoft.EntityFrameworkCore.Tools  
- Newtonsoft.Json  
- Serilog.AspNetCore  
- Swashbuckle.AspNetCore  
- Npgsql.EntityFrameworkCore.PostgreSQL
- Microsoft.AspNetCore.Identity.EntityFrameworkCore
- Nlog
- Nlog.Extensions.Logging
- Nlog.Targets.Splunk
- Microsoft.Extensions.Logging
