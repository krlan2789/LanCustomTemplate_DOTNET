# LanCustomTemplate

ASP.NET Project - Custom Template when creating new ASP.NET Core project

## How to use

1. Go to template directory:

    ```shell
    $ cd CustomTemplate.API/
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

    Template Name                        Short Name                Language   Tags
    -----------------------------------  ------------------------  ---------  ---------------------------------------------------------
    .LAN - ASP.NET Core Web API <--New   lanwebapi                 [C#]       Web/API/Web API/ASP.NET Core/C#
    .NET Aspire App Host                 aspire-apphost            [C#]       Common/.NET Aspire/Cloud
    .NET Aspire Empty App                aspire                    [C#]       Common/.NET Aspire/Cloud/Web/Web API/API/Service
    .NET Aspire Service Defaults         aspire-servicedefaults    [C#]       Common/.NET Aspire/Cloud/Web/Web API/API/Service
    .NET Aspire Starter App              aspire-starter            [C#]       Common/.NET Aspire/Blazor/Web/Web API/API/Service/Cloud
    .NET Aspire Test Project (MSTest)    aspire-mstest             [C#]       Common/.NET Aspire/Cloud/Web/Web API/API/Service/Test
    .NET Aspire Test Project (NUnit)     aspire-nunit              [C#]       Common/.NET Aspire/Cloud/Web/Web API/API/Service/Test
    .NET Aspire Test Project (xUnit)     aspire-xunit              [C#]       Common/.NET Aspire/Cloud/Web/Web API/API/Service/Test
    .NET MAUI App                        maui                      [C#]       MAUI/Android/iOS/macOS/Mac Catalyst/Windows/Tizen/Mobile
    ...
    ```
5. Test, creating new project with new template:

    ```shell
    $ dotnet new lanwebapi -o MyNewProject
    ```

## Templates

Directory structure of `LanCustomTemplate/CustomTemplate.API/`:

```shell
~/DOTNET/PROJECTS/LANCUSTOMTEMPLATE/CUSTOMTEMPLATE.API/
│   appsettings.Development.json
│   appsettings.json
│   CustomTemplate.API.csproj
│   CustomTemplate.API.http
│   CustomTemplate.API.sln
│   Program.cs
│
├───.template.config
│       template.json
│
├───.vscode
│       settings.json
│
├───Controllers
│       AuthController.cs
│       UserController.cs
│
├───Data
│       CustomTemplateDatabaseContext.cs
│
├───Dtos
│       LoginUserDto.cs
│       RegisterUserDto.cs
│       ResponseData.cs
│       ResponseDataArray.cs
│       ResponseUserDto.cs
│
├───Entities
│       User.cs
│       UserProfile.cs
│       UserSessionLog.cs
│
├───Helper
│       HashingHelper.cs
│       SlugHelper.cs
│
├───Mapping
│       UserMapping.cs
│
├───Middlewares
│       UserSessionLoggingMiddleware.cs
│
├───Properties
│       launchSettings.json
│
├───Seeders
│       DatabaseSeeder.cs
│       UserProfileSeeder.cs
│       UserSeeder.cs
│
├───Services
|        TokenService.cs
```

## Available Examples

- Custom Entity-DTO Mapping
- Middleware for Logging
- Tokenization Service
- Hashing Helper
- Slug Helper
- Data Seeder in evelopment Environment
