# ABPOSSOLUTIONS Technical Test

Panel to handle sellers, accountants, products and users using .NET MVC.

To test the project you need:
- .NET 8
- Visual Studio 2022
- SQL Server
- Browser
- Git

To install the project you need:
- Clone the following repository
  ```bash
  git clone https://github.com/josuecorea5/abpossolutions
  ```
- Open the project

- Restore Nuget packages
  - Right-click in Solution Explorer and select Restore Nuget Packages.  

- Set up your database connection strings in the **appsettings.json** file.

- Open the Package manager console and run the migrations
  ```bash
  Update-Database
  ```

- Execute users seeder.
  ```bash
  dotnet run startseed
  ```

- Execute the project
