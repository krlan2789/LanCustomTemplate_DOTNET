# LanCustomTemplate_DOTNET

.NET Project - Custom Template when creating new .NET Core project

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

## Templates

Directory structure of `LanCustomTemplate_DOTNET/CustomTemplate_CA_API/`:

```shell
~/DOTNET/PROJECTS/LanCustomTemplate_DOTNET/CustomTemplate_CA_API/
    │   CustomTemplate.sln
    │
    └───CustomTemplate_CA_API
        │   appsettings.Development.json
        │   appsettings.json
        │   CustomTemplate_CA_API.csproj
        │   CustomTemplate_CA_API.csproj.user
        │   CustomTemplate_CA_API.http
        │   Program.cs
        │
        ├───Application
        │   ├───Common
        │   │   └───Dtos
        │   │           ResponseData.cs
        │   │           ResponseDataArray.cs
        │   │           ResponseError.cs
        │   │           ResponseErrorArray.cs
        │   │
        │   ├───CredentialDomain
        │   │   ├───Configurations
        │   │   │       JwtTokenSettings.cs
        │   │   │       
        │   │   ├───Interfaces
        │   │   │       ICredentialService.cs
        │   │   │       ITokenService.cs
        │   │   │       
        │   │   └───Services
        │   │           CredentialService.cs
        │   │           JwtTokenService.cs
        │   │
        │   ├───SessionLogDomain
        │   │   ├───Commands
        │   │   │       CreateSessionLogCommand.cs
        │   │   │       
        │   │   ├───Dtos
        │   │   │       SessionLogDto.cs
        │   │   │       
        │   │   ├───Interfaces
        │   │   │       ISessionLogRepository.cs
        │   │   │       ISessionLogService.cs
        │   │   │       
        │   │   ├───Mapping
        │   │   │       SessionLogMapping.cs
        │   │   │
        │   │   ├───Queries
        │   │   │       SessionLogsByUsernameQuery.cs
        │   │   │
        │   │   └───Services
        │   │           SessionLogService.cs
        │   │
        │   └───UserDomain
        │       ├───Commands
        │       │       LoginUserCommand.cs
        │       │       RegisterUserCommand.cs
        │       │       UpdateUserProfileCommand.cs
        │       │
        │       ├───Dtos
        │       │       UserDto.cs
        │       │       UserProfileDto.cs
        │       │
        │       ├───Interfaces
        │       │       IUserRepository.cs
        │       │       IUserService.cs
        │       │
        │       ├───Mapping
        │       │       UserMapping.cs
        │       │       UserProfileMapping.cs
        │       │
        │       ├───Queries
        │       │       UserProfileByUsernameQuery.cs
        │       │
        │       └───Services
        │               UserService.cs
        │
        ├───Core
        │   ├───Entities
        │   │       SessionLogEntity.cs
        │   │       UserEntity.cs
        │   │       UserProfileEntity.cs
        │   │
        │   ├───Helper
        │   │       HashingHelper.cs
        │   │       SlugHelper.cs
        │   │
        │   └───Repositories
        │           IBaseRepository.cs
        │
        ├───Infrastructure
        │   ├───Persistence
        │   │   │   AppDatabaseContext.cs
        │   │   │
        │   │   └───Repositories
        │   │           BaseRepository.cs
        │   │           SessionLogRepository.cs
        │   │           UserRepository.cs
        │   │
        │   └───Seeders
        │           DatabaseSeeder.cs
        │           UserProfileSeeder.cs
        │           UserSeeder.cs
        │
        ├───Presentation
        │   ├───Controllers
        │   │       AuthController.cs
        │   │       UserController.cs
        │   │
        │   └───Middlewares
        │           AuthMiddleware.cs
        │           SessionLoggingMiddleware.cs
        │
        └───Properties
                launchSettings.json

```

## Available Examples

- Follows Clean Architecture principles
- Custom Entity-DTO Mapping
- Simple Middleware for Logging
- Tokenization Service (JWT)
- Hashing Helper
- Slug Helper
- Data Seeder in Development Environment
- Setup OpenAPI Documentation with Scalar
