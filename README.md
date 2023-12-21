Here's the structure that my solution Go Through:

1. **Domain Layer (`EmploymentSystem.Domain`):**
    - I Define entities representing core domain concepts ( **`User`**, **`Vacancy`**, **`Application`**).
    - Define repository interfaces for each entity (e.g., **`IUserRepository`**, **`IVacancyRepository`**, **`IApplicationRepository`**).
    - Implement a generic repository and unit of work patterns.
2. **Application Layer (`EmploymentSystem.Application`):**
    - Define DTOs (Data Transfer Objects) for communication between my application and the Presentation layer (API).
    - Define service interfaces (e.g., **`IUserService`**, **`IVacancyService`**, **`IApplicationService`**) that encapsulate the application logic.
    - The interfaces defined here are simple and just cover basic process-related to the application
    - Implement service classes that use the repository interfaces from the domain layer.
    - Implement AutoMapper to map between entities and DTOs.
3. **Infrastructure Layer (`EmploymentSystem.Infrastructure`):**
    - Implement concrete repository classes (e.g., **`UserRepository`**, **`VacancyRepository`**, **`ApplicationRepository`**) from the repository interfaces defined in the domain layer.
    - The first commit of the project focuses on the **`GenericRepository`**.
    - Implement the database context class (**`AppDbContext`**) that inherits from **`DbContext`**.
    - after that, I go to the Dependency Injection Container in the API and Register the database context and repository implementations in the dependency injection container.
4. **API Layer (`EmploymentSystem.Api`):**
    - Implement API controllers (e.g., **`UserController`**, **`VacancyController`**, **`ApplicationController`**) that use the service interfaces from the application layer.
    - Configure dependency injection for services in the API project's **`Program.cs`**.
    - Configure Swagger for API documentation. 

First Time to work with Clean Architecture so I have some time for explore and learn about the clean architure, I explore and learn from the following courses but I ignore the CQRS and MediatR part on the course 
1 - https://app.pluralsight.com/library/courses/asp-dot-net-core-6-clean-architecture/table-of-contents
2 - https://app.pluralsight.com/library/courses/clean-architecture-patterns-practices-principles/table-of-contents

About Logging and secure Api use Old reference for that plus the following course for refresh knowledge
1- https://app.pluralsight.com/library/courses/asp-dot-net-core-6-web-api-fundamentals/table-of-contents

Articles That I explore also for knowledge and info about clean architecture 
- https://juldhais.net/clean-architecture-in-asp-net-core-web-api-4e5ef0b96f99
- https://positiwise.com/blog/clean-architecture-net-core
- https://ardalis.com/aspnetcore-clean-architecture-template-version-8/
- https://medium.com/@edin.sahbaz/getting-started-with-clean-architecture-in-net-core-fa9151bc5918

**DB used is MS SQL server**
**Simple clean architecture is applied**
**Serilog is used as Logger**

After I create the solution and projects structure, My Laptop is Broken and shutdown (Motherboard of the laptob is Broken)So I continue my work on Mac OS and that add some hard work on me:
- First I need to have SQL Server DB so I configure the Docker Version of SQL Server and use Azure Data Studio on Mac Device as GUI Interface to work with figure and see the Database.
- Work is Done using Visual Studio Code so the Namespacing is not totoally the same as Visual Studio.
- Most of work is Done with Cli so that take some time from to learn some more commands that I dodn't use before.
- May I take some more time to finish some of Business Cases but at the end is Demo and open for Improvements and Updates.

To Run the Project:
1- Need to use VS Code as it created and configured on VS Code, Will work on Visual Studio Also but may need some additional minner fixes.
2- Use SQL Server or SQl Serve on Docker and update the Conncection String,run database update command to get new created database.
3- Use Postman as API Documentation or Swagger as it configured with Swagger UI.
4- Additional Apis is created as it as mentioned was first time for me to create that structure so I was want to work will some generic Apis first before go into the business need.

I will add some fixes on the project and work on some business requirement in the task but I as I told you before.
