# LanCustomTemplate

ASP.NET Project - Custom Template when creating new ASP.NET Core project

## How to use

1. Go to template directory :

    ```shell
    $ cd CustomTemplate.API/
    ```

2. Install template :

    ```shell
    $ dotnet new install .
    ```

3. Uninstall template :

    ```shell
    $ dotnet new uninstall .
    ```

4. Check if the template is installed :

    ```shell
    $ dotnet new --list

    # Results
    ...
    These templates matched your input: 

    Template Name                        Short Name                Language   Tags
    -----------------------------------  ------------------------  ---------  ---------------------------------------------------------
    .LAN - ASP.NET Core Web API <-New    lanwebapi                 [C#]       Web/API/Web API/ASP.NET Core/C#
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
5. Test, creating new project with new template :

    ```shell
    $ dotnet new lanwebapi -o MyNewProject
    ```