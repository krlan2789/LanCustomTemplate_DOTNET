# Documentation

## Directory Structure

Directory structure of `LanCustomTemplate_DOTNET/CustomTemplate_CA_API/`:

```shell
~/DOTNET/PROJECTS/LanCustomTemplate_DOTNET/CustomTemplate_CA_API/
    │   CustomTemplate.sln
    │
    └───CustomTemplate_CA_API
        │   appsettings.Development.json
        │   appsettings.json
        │   CustomTemplate_CA_API.csproj
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
