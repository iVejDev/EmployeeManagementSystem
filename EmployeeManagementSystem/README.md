# 👥 Personalhanteringssystem (Employee Management System)

[![.NET Core](https://img.shields.io/badge/.NET%20Core-8.0-512BD4?style=flat-square&logo=dotnet)](https://dotnet.microsoft.com/)
[![License](https://img.shields.io/badge/License-MIT-blue.svg?style=flat-square)](LICENSE)
[![EF Core](https://img.shields.io/badge/EF%20Core-9.0-purple?style=flat-square&logo=.net)](https://docs.microsoft.com/en-us/ef/core/)

Ett modernt personalhanteringssystem byggt med ASP.NET Core MVC där företag kan hantera anställdas information på ett enkelt och effektivt sätt.



## 📋 Funktioner

- **Användarhantering med rollbaserad behörighet**
  - Inloggning för administratörer
  - Administratörsroll med fullständig kontroll över systemet
  
- **Komplett hantering av anställdainformation**
  - Lägg till, redigera, visa och ta bort anställda
  - Lagra detaljerad information om varje anställd
  - Profilbildshantering med uppladdning
  
- **Sökfunktioner och filtrering**
  - Sök efter anställda baserat på namn, avdelning eller position
  - Sortera listan efter olika kriterier
  
- **Responsiv design**
  - Fungerar bra på både datorer, surfplattor och mobiler
  
- **Mörkt/Ljust tema**
  - Möjlighet att byta mellan mörkt och ljust tema

## 🚀 Teknologier

- **Backend**
  - ASP.NET Core 8.0 MVC
  - Entity Framework Core 9.0
  - Identity för autentisering och auktorisering
  
- **Frontend**
  - Bootstrap 4.6
  - jQuery
  - Font Awesome ikoner
  - Animate.css för animationer
  
- **Databas**
  - Microsoft SQL Server
  
## 📦 Installation och körning

### Förutsättningar
- .NET SDK 8.0 eller senare
- Visual Studio 2022 eller senare (rekommenderas)
- SQL Server (LocalDB fungerar också)

### Steg för att komma igång

1. **Klona repot**
   ```
   git clone https://github.com/iVejDev/EmployeeManagementSystem.git
   cd EmployeeManagementSystem
   ```

2. **Återställ NuGet-paket**
   ```
   dotnet restore
   ```

3. **Uppdatera databaskoppling**
   
   Redigera anslutningssträngen i `appsettings.json` om det behövs:
   ```json
   "ConnectionStrings": {
     "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=EmployeeManagementSystem;Trusted_Connection=True;MultipleActiveResultSets=true"
   }
   ```

4. **Kör databasmigrering**
   ```
   dotnet ef database update
   ```

5. **Starta applikationen**
   ```
   dotnet run
   ```

6. **Standard inloggningsuppgifter**
   - E-post: admin@example.com
   - Lösenord: Admin123!

## 📐 Projektstruktur

```
EmployeeManagementSystem/
│
├── Controllers/             # Innehåller alla MVC-controllers
│   ├── EmployeesController.cs  # Hantering av anställda
│   ├── HomeController.cs       # Startsida 
│   ├── ThemeController.cs      # Temahantering
│   └── UsersController.cs      # Användarhantering
│
├── Data/                    # Databaskoppling och Entity Framework-konfiguration
│   ├── ApplicationDbContext.cs # DbContext för applikationen
│   └── Migrations/         # EF Core-migreringar 
│
├── Models/                  # Datamodeller
│   ├── Employee.cs         # Anställd-modell
│   └── ErrorViewModel.cs   # Modell för felhantering
│
├── Services/                # Tjänster för applikationen
│   └── ThemeService.cs     # Hantering av mörkt/ljust tema
│
├── Views/                   # MVC-vyer
│   ├── Employees/          # Vyer för anställda-hantering
│   ├── Home/               # Startsida
│   ├── Shared/             # Delade vyer
│   └── Users/              # Användarhantering
│
└── wwwroot/                 # Statiska filer
    ├── css/                # Stilmallar
    ├── js/                 # JavaScript-filer
    └── images/             # Bilder
```

## 🔒 Säkerhetsfeatures

- ASP.NET Core Identity för autentisering och auktorisering
- Rollbaserad åtkomstkontroll (Admins kan hantera användare)
- CSRF-skydd med antiforgery-tokens
- Säker bilduppladdning med valideringar
- XSS-skydd genom MVC:s automatiska HTML-encodning

## 🛠️ Utvecklingsprocess

För att bidra till projektet:

1. Skapa en ny branch för din feature: `git checkout -b min-nya-feature`
2. Gör dina ändringar och commita: `git commit -m 'Lägg till ny feature'`
3. Pusha till din branch: `git push origin min-nya-feature`
4. Skapa en pull request

## 📝 Licens

Detta projekt är licensierat under [MIT-licensen](LICENSE).

## 👏 Erkännanden

- [Bootstrap](https://getbootstrap.com/) - Frontend-ramverk
- [Font Awesome](https://fontawesome.com/) - Ikoner
- [Animate.css](https://animate.style/) - CSS-animationer


---

&copy; 2025 I.vej. Alla rättigheter förbehållna.