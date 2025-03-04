# 👥 Employee Management System

[![.NET Core](https://img.shields.io/badge/.NET%20Core-8.0-512BD4?style=flat-square&logo=dotnet)](https://dotnet.microsoft.com/)
[![License](https://img.shields.io/badge/License-MIT-blue.svg?style=flat-square)](LICENSE)
[![EF Core](https://img.shields.io/badge/EF%20Core-9.0-purple?style=flat-square&logo=.net)](https://docs.microsoft.com/en-us/ef/core/)

A modern employee management system built with ASP.NET Core MVC where companies can manage employee information in a simple and efficient way



## 📋 Features

- **User management with role-based permissions**
  - Login for administrators
  - Administrator role with full control over the system
  
- **Complete employee information management**
  - Add, edit, view, and delete employees
  - Store detailed information about each employee
  - Profile image management with upload functionality
  
- **Search functions and filtering**
  - Search for employees based on name, department, or position
  - Sort the list by different criteria
  
- **Responsive design**
  - Works well on desktops, tablets, and mobile devices
  
- **Dark/Light theme**
  - Option to switch between dark and light themes

## 🚀 Technologies

- **Backend**
  - ASP.NET Core 8.0 MVC
  - Entity Framework Core 9.0
  - Identity for authentication and authorization
  
- **Frontend**
  - Bootstrap 4.6
  - jQuery
  - Font Awesome icons
  - Animate.css for animations
  
- **Database**
  - Microsoft SQL Server
  
## 📦 Installation and Setup

### Prerequisites
- .NET SDK 8.0 or later
- Visual Studio 2022 or later (recommended)
- SQL Server (LocalDB works too)

### Getting Started

1. **Clone the repository**
   ```
   git clone https://github.com/iVejDev/EmployeeManagementSystem.git
   cd EmployeeManagementSystem
   ```

2. **Restore NuGet packages**
   ```
   dotnet restore
   ```

3. **Update database connection**
   
   Edit the connection string in `appsettings.json` if needed:
   ```json
   "ConnectionStrings": {
     "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=EmployeeManagementSystem;Trusted_Connection=True;MultipleActiveResultSets=true"
   }
   ```

4. **Run database migration**
   ```
   dotnet ef database update
   ```

5. **Start the application**
   ```
   dotnet run
   ```

6. **Default login credentials**
   - Email: admin@example.com
   - Password: Admin123!

## 📐 Project Structure

```
EmployeeManagementSystem/
│
├── Controllers/             # Contains all MVC controllers
│   ├── EmployeesController.cs  # Employee management
│   ├── HomeController.cs       # Home page 
│   ├── ThemeController.cs      # Theme management
│   └── UsersController.cs      # User management
│
├── Data/                    # Database connection and Entity Framework configuration
│   ├── ApplicationDbContext.cs # DbContext for the application
│   └── Migrations/         # EF Core migrations 
│
├── Models/                  # Data models
│   ├── Employee.cs         # Employee model
│   └── ErrorViewModel.cs   # Error handling model
│
├── Services/                # Services for the application
│   └── ThemeService.cs     # Dark/light theme management
│
├── Views/                   # MVC views
│   ├── Employees/          # Views for employee management
│   ├── Home/               # Home page
│   ├── Shared/             # Shared views
│   └── Users/              # User management
│
└── wwwroot/                 # Static files
    ├── css/                # Stylesheets
    ├── js/                 # JavaScript files
    └── images/             # Images
```

## 🔒 Security Features

- ASP.NET Core Identity for authentication and authorization
- Role-based access control (Admins can manage users)
- CSRF protection with antiforgery tokens
- Secure image upload with validations
- XSS protection through MVC's automatic HTML encoding

## 🛠️ Development Process

To contribute to the project:

1. Create a new branch for your feature: `git checkout -b my-new-feature`
2. Make your changes and commit: `git commit -m 'Add new feature'`
3. Push to your branch: `git push origin my-new-feature`
4. Create a pull request

## 📝 License

This project is licensed under the [MIT License](LICENSE).

## 👏 Acknowledgements

- [Bootstrap](https://getbootstrap.com/) - Frontend framework
- [Font Awesome](https://fontawesome.com/) - Icons
- [Animate.css](https://animate.style/) - CSS animations


---

&copy; 2025 I.vej. All rights reserved.
