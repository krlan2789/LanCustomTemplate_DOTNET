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

    Template Name                                            Short Name                             Language    Tags
    -------------------------------------------------------  -------------------------------------  ----------  ------------------------------------------------------------------------------------
    .LAN - ASP.NET Core Web API (Clean Architecture) <--New  lanwebapi-ca                           [C#]        Web/API/Web API/ASP.NET Core/C#/Clean Architecture
    .NET Aspire App Host                                     aspire-apphost                         [C#]        Common/.NET Aspire/Cloud
    .NET Aspire Empty App                                    aspire                                 [C#]        Common/.NET Aspire/Cloud/Web/Web API/API/Service
    .NET Aspire Service Defaults                             aspire-servicedefaults                 [C#]        Common/.NET Aspire/Cloud/Web/Web API/API/Service
    .NET Aspire Starter App                                  aspire-starter                         [C#]        Common/.NET Aspire/Blazor/Web/Web API/API/Service/Cloud
    .NET Aspire Test Project (MSTest)                        aspire-mstest                          [C#]        Common/.NET Aspire/Cloud/Web/Web API/API/Service/Test
    .NET Aspire Test Project (NUnit)                         aspire-nunit                           [C#]        Common/.NET Aspire/Cloud/Web/Web API/API/Service/Test
    .NET Aspire Test Project (xUnit)                         aspire-xunit                           [C#]        Common/.NET Aspire/Cloud/Web/Web API/API/Service/Test
    .NET MAUI App                                            maui                                   [C#]        MAUI/Android/iOS/macOS/Mac Catalyst/Windows/Tizen/Mobile
    .NET MAUI Blazor Hybrid App                              maui-blazor                            [C#]        MAUI/Android/iOS/macOS/Mac Catalyst/Windows/Tizen/Blazor/Blazor Hybrid/Mobile
    ...
    ```
5. Test, creating new project with new template:

    ```shell
    $ dotnet new lanwebapi-ca -o MyNewProject
    ```

## Templates

Directory structure of `LanCustomTemplate-DOTNET/CustomTemplate_CA_API/`:

```shell
~/DOTNET/Projects/LanCustomTemplate-DOTNET/CustomTemplate_CA_API/
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
    │   ├───Configurations
    │   │       JwtTokenSettings.cs
    │   │       
    │   ├───Dtos
    │   │       CreateUserDto.cs
    │   │       LoginUserDto.cs
    │   │       RegisterUserDto.cs
    │   │       ResponseData.cs
    │   │       ResponseDataArray.cs
    │   │       ResponseError.cs
    │   │       ResponseErrorArray.cs
    │   │       UpdateUserProfileDto.cs
    │   │       UserDto.cs
    │   │       UserProfileDto.cs
    │   │
    │   ├───Extensions
    │   │       UserMappingExtensions.cs
    │   │       UserProfileMappingExtension.cs
    │   │
    │   ├───Interfaces
    │   │   ├───Repositories
    │   │   │       IUserRepository.cs
    │   │   │
    │   │   └───Services
    │   │           IAuthService.cs
    │   │           ITokenService.cs
    │   │           IUserService.cs
    │   │
    │   └───Services
    │           AuthService.cs
    │           JwtTokenService.cs
    │           UserService.cs
    │
    ├───Core
    │   ├───Entities
    │   │       UserEntity.cs
    │   │       UserProfileEntity.cs
    │   │       UserSessionLogEntity.cs
    │   │
    │   └───Helper
    │           HashingHelper.cs
    │           SlugHelper.cs
    │
    ├───Infrastructure
    │   ├───Persistence
    │   │   │   AppDatabaseContext.cs
    │   │   │
    │   │   └───Repositories
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
    │           UserSessionLoggingMiddleware.cs
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
