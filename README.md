# DegreePlanner #
[The live site status: STOPPED](https://degreeplanner-gye5a8fwhhcefacr.westus-01.azurewebsites.net/)


![image](https://github.com/user-attachments/assets/e21aeb3b-0f45-4b07-a823-a6827f8f37dd)



### Current Status
DegreePlanner is no longer actively maintained, and the live site has been sunset. The repository remains a showcase of the project's architecture, implementation, and functionality.

## Overview 
DegreePlanner was designed as an academic planning tool to help students visually organize terms and courses to track their progress toward their goals. 
- **Terms**: Represent semesters, quarters, programs, or academic periods.  
- **Courses**: Represent a subject, class, or course within a term.   
- **Course Item**: Represent a task within a course, such as assignments, assessments, or to-do items.


![image](https://github.com/user-attachments/assets/4f49493a-f5f4-4ac8-b5ce-5c93b397427b)


## Project Structure  
```plaintext
DegreePlanner/
├── DegreePlanner.DataAccess/   # Database context and repositories
├── DegreePlanner.Models/       # Models and view models
├── DegreePlanner.Tests/        # Unit tests
├── DegreePlanner.Utility/      # Helper utilities
└── DegreePlannerWeb/           # Web application logic
```
- **DegreePlanner.DataAccess:** Handles interaction with the database using Entity Framework Core.
- **DegreePlanner.Models:** Defines domain models and view models for data representation.
- **DegreePlanner.Tests:** Includes xUnit tests to ensure application reliability and performance.
- **DegreePlanner.Utility:** Includes reusable helper utilities. 
- **DegreePlannerWeb:** Implements the MVC pattern using Razor Views and ASP.NET Core to provide a dynamic, server-side web application.

## Key Features  
- **Term and Course Management**:  
  - Create terms and add multiple courses per term.  
  - Edit or remove terms and courses as needed.
 
- **Assignment Tracking**:  
  - Add assignments to specific courses.  
  - View upcoming tasks on the dashboard.

- **Global Search**:  
  - Quickly find terms, courses, or assignments from across your entire account.
 
- **User Authentication**:  
  - Secure login system using ASP.NET Identity.

## Acknowledgments
This project was built with these tools and frameworks: 
- ASP.NET Core
- Entity Framework Core
- Azure App Services
- xUnit for testing
