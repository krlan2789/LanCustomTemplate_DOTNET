# LanCustomTemplate_DOTNET

ASP.NET Project - Custom Template when creating new ASP.NET Core project

## About The Template

This template demonstrates a practical application of Clean Architecture, Domain-Driven Design (DDD), and Command Query Responsibility Segregation (CQRS) patterns in a ASP.NET Core project. It provides a solid foundation for building maintainable and testable applications by organizing code into clear layers and separating concerns.

- Follows Clean Architecture principles
- Follows Domain-Driven Design principles
- Follows Command Query Responsibility Segregation principles
- Custom Command/Query-DTO Mapping
- Middleware for Logging
- Tokenization Service (JWT)
- Hashing Helper
- Slug Helper
- Database Context Config (SQLite/SQL Server/PostgreSQL)
- Data Seeder in Development Environment
- Setup OpenAPI Documentation with Scalar

## How to use

1. Go to template directory:

    ```shell
    $ cd CustomTemplate_CA_API/
    ```

2. Install template:

    ```shell
    $ dotnet new install .
    ```

3. Uninstall template:

    ```shell
    $ dotnet new uninstall .
    ```

4. Check if the template is installed:

    ```shell
    $ dotnet new --list

    # Results
    ...
    These templates matched your input:

    Template Name                           Short Name                             Language    Tags
    --------------------------------------  -------------------------------------  ----------  ------------------------------------------------------------------------------------
    .Laness - ASP.NET Core Web API <--New   laness-webapi                          [C#]        Web/API/Web API/ASP.NET Core/C#/Clean Architecture
    .NET Aspire App Host                    aspire-apphost                         [C#]        Common/.NET Aspire/Cloud
    .NET Aspire Empty App                   aspire                                 [C#]        Common/.NET Aspire/Cloud/Web/Web API/API/Service
    .NET Aspire Service Defaults            aspire-servicedefaults                 [C#]        Common/.NET Aspire/Cloud/Web/Web API/API/Service
    .NET Aspire Starter App                 aspire-starter                         [C#]        Common/.NET Aspire/Blazor/Web/Web API/API/Service/Cloud
    .NET Aspire Test Project (MSTest)       aspire-mstest                          [C#]        Common/.NET Aspire/Cloud/Web/Web API/API/Service/Test
    .NET Aspire Test Project (NUnit)        aspire-nunit                           [C#]        Common/.NET Aspire/Cloud/Web/Web API/API/Service/Test
    .NET Aspire Test Project (xUnit)        aspire-xunit                           [C#]        Common/.NET Aspire/Cloud/Web/Web API/API/Service/Test
    .NET MAUI App                           maui                                   [C#]        MAUI/Android/iOS/macOS/Mac Catalyst/Windows/Tizen/Mobile
    .NET MAUI Blazor Hybrid App             maui-blazor                            [C#]        MAUI/Android/iOS/macOS/Mac Catalyst/Windows/Tizen/Blazor/Blazor Hybrid/Mobile
    ...
    ```
5. Test, creating new project with new template:

    ```shell
    $ dotnet new laness-webapi -o MyNewProject
    ```
