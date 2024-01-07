Here's the structure that my solution goes through:

1. **Domain Layer (`EmploymentSystem.Domain`):**
    - I Define entities representing core domain concepts ( **`User`**, **`Vacancy`**, **`Application`**).
    - Define repository interfaces for each entity (e.g., **`IUserRepository`**, **`IVacancyRepository`**, **`IApplicationRepository`**).
    - Implement a generic repository and unit of work patterns.
2. **Application Layer (`EmploymentSystem.Application`):**
    - Define DTOs (Data Transfer Objects) for communication between my application and the Presentation layer (API).
    - Define service interfaces (e.g., **`IUserService`**, **`IVacancyService`**, **`IApplicationService`**) that encapsulate the application logic.
    - The interfaces defined here are simple and just cover the basic processes of the application
    - Implement service classes that use the repository interfaces from the domain layer.
    - Implement AutoMapper to map between entities and DTOs.
3. **Infrastructure Layer (`EmploymentSystem.Infrastructure`):**
    - Implement concrete repository classes (e.g., **`UserRepository`**, **`VacancyRepository`**, **`ApplicationRepository`**) from the repository interfaces defined in the domain layer.
    - The first commit of the project focuses on the **`GenericRepository`**.
    - Implement the database context class (**`AppDbContext`**) that inherits from **`DbContext`**.
    - after that, I go to the Dependency Injection Container in the API and Register the database context and repository implementations in the dependency injection container.
4. **API Layer (`EmploymentSystem.Api`):**
    - Implement API controllers (e.g., **`UserController`**, **`VacancyController`**, **`ApplicationController`**) that use the service interfaces from the application layer.
    - Configure dependency injection for services in the API project's **` Program.cs`**.
    - Configure Swagger for API documentation. 

First Time working with Clean Architecture so I had some time to explore and learn about clean architecture, I explored and learned from the following courses but I ignored the CQRS and MediatR part on the course 
1 - https://app.pluralsight.com/library/courses/asp-dot-net-core-6-clean-architecture/table-of-contents
2 - https://app.pluralsight.com/library/courses/clean-architecture-patterns-practices-principles/table-of-contents

About Logging and securing Api use Old references for that plus the following course for refreshing knowledge
1- https://app.pluralsight.com/library/courses/asp-dot-net-core-6-web-api-fundamentals/table-of-contents

Articles That I explore also for knowledge and info about clean architecture 
- https://juldhais.net/clean-architecture-in-asp-net-core-web-api-4e5ef0b96f99
- https://positiwise.com/blog/clean-architecture-net-core
- https://ardalis.com/aspnetcore-clean-architecture-template-version-8/
- https://medium.com/@edin.sahbaz/getting-started-with-clean-architecture-in-net-core-fa9151bc5918

**DB used is MS SQL server**
**Simple clean architecture is applied**
**Serilog is used as Logger**
**Added JWt Token-Based Authentication**
**Added Authorization Role of Type RBAC**

After I created the solution and project structure, My Laptop broke and shut (The motherboard of the laptop was Broken)So I continued my work on Mac OS, and that added some hard work me:
- First I need to have SQL Server DB so I configure the Docker Version of SQL Server and use Azure Data Studio on a Mac Device as GUI Interface to work with figure and see the Database.
- Work is Done using Visual Studio Code so the Namespacing is not the same as Visual Studio.
- Most of the work is Done with Cli so that takes some time to learn some more commands that I didn't use before.
- May I take some more time to finish some of the Business Cases at the end of is Demo and open for Improvements and Updates?

To Run the Project:
- Need to use VS Code as it is created and configured on VS Code, Will work on Visual Studio Also but may need some additional minner fixes.
- Use SQL Server or SQL Server on Docker to update the Connection String, and run the database update command to get the newly created database.
- Use Postman as API Documentation or Swagger as it is configured with Swagger UI.
- Additional Apis are created as it as mentioned was the first time for me to create that structure so I wanted to work will some generic Apis first before going into the business need.

I will add some fixes to the project and work on some business requirements in the task but as I told you before.
- Fix Serilog Configurations
- Permission Issue with Update and Delete
- Swagger --> Token Section
- Apply Soft Delete
- Audit Columns on Database
- Add Background services as Hangfire to expire the Vacancy
- Redis 
