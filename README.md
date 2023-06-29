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
| 4   | Set up logging, use const                                |      |        |         |
| 5   | serilog, health-check, http client                       | 1    | 2      | 100%    |
|     | cors, gzip, validator                                    | 1    | 2      | 100%    |

- Details:
  1. Enhance codebase Program.cs, set up docker
     1. Enhance codebase: Program.cs
        1. separate configuration settings in Program.cs
     2. Set up docker: add docker file and docker compose. Connect to postgresql.
        1. Issue: when connecting to postgresql - Solutions: add environment variable, migrate same time docker compose.
  2. Enhance code (fluentAPI, migration), research Splunk log
     1. Enhance code (fluentAPI, migration, health check)
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
  5. log, http client, cors
     1. set up serilog, cant use elk stack ram pc do not enough.
     2. http client: create api base to send request.
     3. CORS: set up 2 options allow and specific
     4. gZip: compress file responses
     5. validator

- Week 2: 26 - 30/6/2023
| id  | task                                   | days | points | Process |
| --- | -------------------------------------- | ---- | ------ | ------- |
| 1   | jwt, retry, test project               |      |        |         |
| 2   | remove libs,                           |      |        |         |
| 3   | research new relic and minimal api     |      |        |         |
| 4   | research redis and integrate new relic |      |        |         |
| 5   |                                        |      |        |         |

  1. jwt, retry, test project, ec,
     1. jwt
     2. retry: install polly lib and add retry for http client
     3. test project use nUnit test
  2. remove libs
     1. cors, retry, identity

- On Ready to go
  - Week 3:
    - enhance
      - validate for register, login request. Seeding data for user table
      - move authentication to middleware
    - add hangfire
    - add ci/cd
    - redis cache container
    - proxy
      - ngnix
    - host to azure
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
- Nlog/Splunk : fail
- redis
- newrelics
- Nginx
- Hangfire                  == separate demo
- NetMQ/SignalR (optional)  == separate demo

## Dependencies

- validator & mapping & json
  - FluentValidation  
  - FluentValidation.AspNetCore  
  - Newtonsoft.Json  
  - AutoMapper.Extensions.Microsoft.DependencyInjection  

- ORM
  - dotnet add package Microsoft.EntityFrameworkCore.Design  
  - dotnet add package Microsoft.EntityFrameworkCore.InMemory  
  - dotnet add package Microsoft.EntityFrameworkCore.SqlServer  
  - dotnet add package Microsoft.EntityFrameworkCore.Tools  
  - dotnet add package Npgsql.EntityFrameworkCore.PostgreSQL

- Authenticate & Author
  - Microsoft.AspNetCore.Authentication.JwtBearer  
  - Microsoft.AspNetCore.Identity.EntityFrameworkCore

- swagger
  - Microsoft.AspNetCore.OpenApi  
  - Swashbuckle.AspNetCore  

<!-- - splunk stack
  - Nlog
  - Nlog.Extensions.Logging
  - Nlog.Targets.Splunk
  - Microsoft.Extensions.Logging -->

- elk stack + log
  - Serilog.AspNetCore
  - Serilog.Enrichers.Environment
  - Serilog.Exceptions
  - Serilog.Sinks.Console
  - Serilog.Sinks.Debug
  <!-- - Serilog.Sinks.Elasticsearch -->
- retry
  <!-- - RefreshMicrosoft.Extensions.Http.Polly -->

- new relic
  - NewRelic.Agent.Api

- redis caching

  -
