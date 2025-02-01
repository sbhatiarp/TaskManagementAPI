# TaskManagementAPI
Task Management Application is a solution designed to allow users to efficiently manage tasks through a backend API. Users can add, view, and delete tasks, providing them with a simple and intuitive way to track their daily tasks.

# Technologies Used
 - .NET Core API (Backend)
 - Entity Framework Core (Data Management)
 - SQL Server (Database)
 - Swagger (API Documentation)

# Features

 - Add Task: Users can create new tasks with descriptions.
 - View Task: Retrieve and display a list of tasks.
 - Delete Task: Remove tasks from the list.
 - Error Handling: Global error handling middleware is implemented.

# How to setup the project

Step 1: Configure the Database Connection
Update your connection string in the `appsettings.json` file by modifying the `DefaultConnection` key.

Step 2: Database Deployment
You can either restore the database in your SQL Server instance (attached in the email) or follow these steps to create a new database: 

1. Open the solution in Visual Studio and go to Package Manager Console.
2. Run the following command to create a migration:
   `Add-Migration InitializeDatabase`
3. Apply the migration to create the database:
   `Update-Database`

Step 3: Run the API
Start the `TaskManagement.API` project in Visual Studio.


