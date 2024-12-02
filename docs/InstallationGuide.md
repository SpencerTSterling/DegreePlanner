# DegreePlanner Installation Guide

This guide provides step-by-step instructions for installing the **DegreePlanner** application on your local machine. It covers prerequisites, cloning the repository, setting up the database, resolving common errors, and testing the installation.

## Prerequisites

These are the necessary software and tools required to install and run the DegreePlanner application. Ensure you have everything listed before proceeding with the installation.

- **Visual Studio 2022** with ASP.NET and web development tools enabled.
- **SQL Server Management Studio** (SSMS) with a valid SQL Server instance.
- **.NET 6.0 SDK** or later.

## Clone the Degree Planner Repository

This section provides step-by-step instructions for cloning the DegreePlanner repository from GitHub.

1. Go to [DegreePlanner repository on GitHub.](https://github.com/SpencerTSterling/DegreePlanner)
2. Copy the HTTPS link:
    - Click on the "Code" button and copy the HTTPS clone link for the `master` branch.
3. Launch **Visual Studio** and click **“Clone a repository”** on the start page.
4. Enter the Repository URL:
    - Paste the HTTPS link into the **Repository URL** field.
    - In the Path field, choose or create a folder to store the repository on your local machine.
5. Click **“Clone”**. Visual Studio will begin cloning the repository to your selected local folder.
6. Verify Cloning:
    - If the cloning process completes successfully, you should see a progress bar. If any issues arise, try the following:
      - Double-click the folder icon at the top of the **Object Explorer** to navigate to the project folders nested inside.
      - Locate the cloned files in your **File Explorer** to see if the project files are there.
7. Open the Project:
    - If you see the folder in **File Explorer** but not in Visual Studio, try refreshing the **Solution Explorer**. Right-click in the **Solution Explorer** and select **"Refresh"**.
    - If the project did not open automatically, you can manually open the solution file:
        - Click on **File** in the top menu.
        - Select **Open > Project/Solution**.
        - Navigate to the folder you cloned the project to and look for the `.sln` (solution file) for the project. Select it and click **Open**.
  ![image](https://github.com/user-attachments/assets/9bed12dc-6210-489c-ae29-65cc028e3abc)
8. Build the Solution:
    - Restore **NuGet** packages by right-clicking the solution in **Solution Explorer** and selecting **Restore NuGet Packages**.
    - Go to **Build > Build Solution** from the top menu to build the entire solution.

Once the solution has been successfully built and all projects load without errors, you can move on to the next section.

## Setting Up the Database

This section provides instructions for configuring the local database connection, applying migrations, and initializing the database for the DegreePlanner application.

### Prerequisites

- **SQL Server Management Studio (SSMS)**: Install this tool to manage and modify the database if necessary.
- **Entity Framework Core**: Ensure that Entity Framework Core is installed and set up within the solution. You can verify this by building the solution and checking for missing references or errors related to EF Core.

### Configure a SQL Server Instance

Begin by configuring a SQL Server Instance for your local database. This is necessary to support and manage the DegreePlanner database. These steps include setting up and connecting to a local instance.

1. Install **SQL Server LocalDB** or **SQL Server Developer Edition** (recommended).
2. Launch **SSMS** and in the **Connect to Server** dialog:
    - **Server Type**: Database Engine
    - **Server Name**: You can use `localhost` or select one from the dropdown menu if there are options presented there (e.g., `NAME\MSSQLSERVER01`, `SQLEXPRESS`).
    - **Authentication**: Windows Authentication or SQL Server Authentication, depending on your setup.
    - If there is a certificate trust error, check **Trust Server Certificate**.
3. Confirm the connection. Once connected, the instance’s name will be at the top of the **Object Explorer** pane.

### Create the Connection String

Now you can configure the database connection in Visual Studio. Use the instance name to create the connection string. This is the format you’ll need to use:
```
Server=YOUR_INSTANCE_NAME;Database=DegreePlannerDB;Trusted_Connection=True;MultipleActiveResultSets=true
```
1. Replace `YOUR_INSTANCE_NAME` with your actual SQL Server Instance name.
2. Replace `DegreePlannerDB` with the name of your database. You can use `DegreePlannerDB` if it hasn’t been created yet (it will be created when you apply migrations).

**Note**: To prevent errors related to trust certificates, consider adding `TrustServerCertificate=True` to the connection string as well.

If you have used **localhost** as the server, you can use this string. 
```
Server=localhost;Database=DegreePlannerCloneDB;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=True
```

### Update `appsettings.json` in Visual Studio

To allow the DegreePlanner application to connect to your local SQL Server database, you’ll need to configure the connection string in Visual Studio.

1. In Visual Studio, open the `appsettings.json` file located in the **DegreePlannerWeb** project.
2. Modify the Connection String under the **“DefaultConnection”** setting with your custom connection string.
![image](https://github.com/user-attachments/assets/36d406d3-4b4b-465e-b7a3-a6e62b464c3d)


### Apply Migrations and Update Database

This step will create the necessary tables, columns, and relationships in the database.

1. Open the **Package Manager Console** in Visual Studio.
    - Go to the top menu and select **Tools > NuGet Package Manager > Package Manager Console**.
    - Make sure the **Default Project** dropdown is set to `DegreePlanner.DataAccess`.
2. Run migrations to update the database.
    - Run the following command in the console:  
    ```bash
    Update-Database
    ```

    **Error Resolution**:  
If you encounter the error: *“A connection was successfully established with the server, but then an error occurred during the login process”*, follow these steps:

- This issue can occur with local instances in SQL Server Developer Edition. To resolve it, add an option to your connection string in `appsettings.json` to trust the certificate:
    - Modify the connection string to include `TrustServerCertificate=True;`. This setting allows SQL Server to bypass the SSL certificate check.
    - Save the changes and retry the `Update-Database` command.
After successfully running the command, you may still encounter errors related to missing columns, such as `UserId` in the **Terms** table or `Discriminator` in the **User** table. These are known issues and can be resolved by following the instructions in the next section.

## Resolving Common Errors

During the setup process, you may encounter errors related to missing columns or conflicts in the database. These are common issues due to certain database fields not being automatically generated.

You can check if the columns and tables were generated correctly by viewing the database in **SQL Server Management Studio**. Expand the database and compare the generated database to the provided class diagram. If there are missing columns, you may need to add them manually.

To avoid permission issues when modifying the database, run **SQL Server Management Studio (SSMS)** as an administrator. This will ensure you have the necessary permissions for making changes, such as altering tables and running SQL scripts. This can be done by right-clicking the application and selecting **Run as administrator**.

**Example: Adding the `UserId` Column to the Terms Table**  

~~~sql
USE DegreePlannerDB -- Replace with the name of your DB
ALTER TABLE dbo.Terms
ADD UserId NVARCHAR(450);

ALTER TABLE dbo.Terms
ADD CONSTRAINT FK_Terms_Users FOREIGN KEY (UserId) REFERENCES AspNetUsers(Id);
~~~

**Remember**: After modifying the database schema, always add migrations and update the database.
