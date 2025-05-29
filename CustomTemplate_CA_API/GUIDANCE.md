# Getting Started

1. **Enter to Project Directory**
	
    - Enter to project directory in your local machine:
    	```bash
		cd CustomTemplate_CA_API
		```

2. **Understand the Project Structure**
	
    - Familiarize yourself with the folder structure outlined in the documentation.
	- Focus on the `/CustomTemplate_CA_API` directory, which contains all domain modules and shared components.

3. **Set Up the Development Environment**
	
    - Install the required tools and dependencies:
		- A database system (e.g., SQL Server, PostgreSQL).
		- Redis (if caching is enabled).
    - Run the following commands to restore dependencies and build the project:
    	```bash
		dotnet restore CustomTemplate_CA_API.csproj
		dotnet build
		```

4. **Run the Application**
	
    - Start the application locally:
    	```bash
		dotnet watch --project CustomTemplate_CA_API.csproj
		```
    - Access the API documentation at `http://localhost:5044/api/docs/v1`.

5. **Database Migration**
    
    - Create database migration files:
        ```shell
        dotnet ef migrations add InitialCreate --output-dir Infrastructure/Persistence/Migrations --project CustomTemplate_CA_API.csproj
        ```
    - Remove database migration file:
        ```shell
        dotnet ef migrations remove --project CustomTemplate_CA_API.csproj
        ```

6. **Create Database**
	
    - Apply database migrations to set up the schema:
        ```bash
		dotnet ef database update --project CustomTemplate_CA_API.csproj
		```
