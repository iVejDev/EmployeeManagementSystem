using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EmployeeManagementSystem.Data;
using EmployeeManagementSystem.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using System.IO;

namespace EmployeeManagementSystem.Controllers
{
    // Använd [Authorize]-attributet för att begränsa åtkomst till inloggade användare
    [Authorize(Roles = "Admin")]
    public class EmployeesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        // Konstruktor med dependency injection
        public EmployeesController(ApplicationDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        // GET: Employees - Listar alla anställda
        public async Task<IActionResult> Index(string searchString, string sortOrder)
        {
            ViewData["NameSortParam"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewData["DepartmentSortParam"] = sortOrder == "Department" ? "department_desc" : "Department";
            ViewData["CurrentFilter"] = searchString;

            var employees = from e in _context.Employees
                            select e;

            // Filtrera baserat på söksträngen om den finns
            if (!String.IsNullOrEmpty(searchString))
            {
                employees = employees.Where(e =>
                    e.FirstName.Contains(searchString) ||
                    e.LastName.Contains(searchString) ||
                    e.Department.Contains(searchString) ||
                    e.Position.Contains(searchString));
            }

            // Sortera baserat på sortOrder-parametern
            switch (sortOrder)
            {
                case "name_desc":
                    employees = employees.OrderByDescending(e => e.LastName);
                    break;
                case "Department":
                    employees = employees.OrderBy(e => e.Department);
                    break;
                case "department_desc":
                    employees = employees.OrderByDescending(e => e.Department);
                    break;
                default:
                    employees = employees.OrderBy(e => e.LastName);
                    break;
            }

            return View(await employees.AsNoTracking().ToListAsync());
        }

        // GET: Employees/Details/5 - Visar detaljer för en specifik anställd
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await _context.Employees
                .FirstOrDefaultAsync(m => m.EmployeeId == id);
            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }

        // GET: Employees/Create - Visar formulär för att skapa en ny anställd
        public IActionResult Create()
        {
            // Skapa listor för dropdowns
            PopulateDropDowns();
            return View();
        }

        // POST: Employees/Create - Skapar en ny anställd baserat på formulärdata
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EmployeeId,FirstName,LastName,Gender,Address,MobilePhone,WorkPhone,Email,DateOfBirth,EmploymentType,StartDate,EndDate,Position,Department,WorkingHours,EmploymentRate,Status,Notes,ImageFile")] Employee employee)
        {
            try
            {
                // Ta bort valideringsfel för valfria fält
                if (ModelState.ContainsKey("Notes") && ModelState["Notes"].Errors.Count > 0)
                {
                    ModelState.Remove("Notes");
                }

                if (ModelState.ContainsKey("WorkingHours") && ModelState["WorkingHours"].Errors.Count > 0)
                {
                    ModelState.Remove("WorkingHours");
                }

                if (ModelState.ContainsKey("WorkPhone") && ModelState["WorkPhone"].Errors.Count > 0)
                {
                    ModelState.Remove("WorkPhone");
                }

                // Kontrollera om det finns valideringsfel
                if (!ModelState.IsValid)
                {
                    foreach (var state in ModelState)
                    {
                        if (state.Value.Errors.Count > 0)
                        {
                            Console.WriteLine($"Field: {state.Key}, Errors: {string.Join(", ", state.Value.Errors.Select(e => e.ErrorMessage))}");
                        }
                    }

                    // Återskapa dropdowns
                    PopulateDropDowns();
                    return View(employee);
                }

                // Sätt standardvärden för tomma strängar
                employee.WorkingHours = employee.WorkingHours ?? "";
                employee.Notes = employee.Notes ?? "";
                employee.WorkPhone = employee.WorkPhone ?? "";

                // Hantera bilduppladdning
                if (employee.ImageFile != null && employee.ImageFile.Length > 0)
                {
                    // Skapa sökväg för uppladdningsmappen
                    string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "images", "employees");

                    // Skapa mappen om den inte finns
                    if (!Directory.Exists(uploadsFolder))
                    {
                        Directory.CreateDirectory(uploadsFolder);
                    }

                    // Generera ett unikt filnamn
                    string uniqueFileName = Guid.NewGuid().ToString() + "_" + employee.ImageFile.FileName;
                    string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                    // Spara filen
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await employee.ImageFile.CopyToAsync(fileStream);
                    }

                    // Spara sökvägen i modellen
                    employee.ImagePath = "/images/employees/" + uniqueFileName;
                }
                else
                {
                    // Sätt standardvärde om ingen bild laddas upp
                    employee.ImagePath = "/images/default-avatar.png";
                }

                // Lägg till anställd i databasen
                _context.Add(employee);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                // Logg detaljerade felinformation
                Console.WriteLine($"Exception: {ex.Message}");
                Console.WriteLine($"InnerException: {ex.InnerException?.Message}");
                Console.WriteLine($"StackTrace: {ex.StackTrace}");

                // Lägg till ett användarvänligt felmeddelande i ModelState
                ModelState.AddModelError("", $"Ett fel uppstod: {ex.Message}");
                if (ex.InnerException != null)
                {
                    ModelState.AddModelError("", $"Detaljerad information: {ex.InnerException.Message}");
                }

                // Återskapa dropdowns
                PopulateDropDowns();
                return View(employee);
            }
        }

        // GET: Employees/Edit/5 - Visar formulär för att redigera en befintlig anställd
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await _context.Employees.FindAsync(id);
            if (employee == null)
            {
                return NotFound();
            }

            // Populera dropdowns
            PopulateDropDowns();
            return View(employee);
        }

        // POST: Employees/Edit/5 - Uppdaterar en anställd baserat på formulärdata
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("EmployeeId,FirstName,LastName,Gender,Address,MobilePhone,WorkPhone,Email,DateOfBirth,EmploymentType,StartDate,EndDate,Position,Department,WorkingHours,EmploymentRate,Status,Notes,ImagePath,ImageFile")] Employee employee)
        {
            if (id != employee.EmployeeId)
            {
                return NotFound();
            }

            // Ta bort valideringsfel för valfria fält
            if (ModelState.ContainsKey("Notes") && ModelState["Notes"].Errors.Count > 0)
            {
                ModelState.Remove("Notes");
            }

            if (ModelState.ContainsKey("WorkingHours") && ModelState["WorkingHours"].Errors.Count > 0)
            {
                ModelState.Remove("WorkingHours");
            }

            if (ModelState.ContainsKey("WorkPhone") && ModelState["WorkPhone"].Errors.Count > 0)
            {
                ModelState.Remove("WorkPhone");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    // Sätt standardvärden för tomma strängar
                    employee.WorkingHours = employee.WorkingHours ?? "";
                    employee.Notes = employee.Notes ?? "";
                    employee.WorkPhone = employee.WorkPhone ?? "";

                    // Hantera bilduppladdning
                    if (employee.ImageFile != null && employee.ImageFile.Length > 0)
                    {
                        // Skapa sökväg för uppladdningsmappen
                        string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "images", "employees");

                        // Skapa mappen om den inte finns
                        if (!Directory.Exists(uploadsFolder))
                        {
                            Directory.CreateDirectory(uploadsFolder);
                        }

                        // Ta bort gammal bild om den finns
                        if (!string.IsNullOrEmpty(employee.ImagePath))
                        {
                            string oldImagePath = Path.Combine(_webHostEnvironment.WebRootPath, employee.ImagePath.TrimStart('/'));
                            if (System.IO.File.Exists(oldImagePath))
                            {
                                System.IO.File.Delete(oldImagePath);
                            }
                        }

                        // Generera ett unikt filnamn
                        string uniqueFileName = Guid.NewGuid().ToString() + "_" + employee.ImageFile.FileName;
                        string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                        // Spara filen
                        using (var fileStream = new FileStream(filePath, FileMode.Create))
                        {
                            await employee.ImageFile.CopyToAsync(fileStream);
                        }

                        // Spara sökvägen i modellen
                        employee.ImagePath = "/images/employees/" + uniqueFileName;
                    }

                    _context.Update(employee);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmployeeExists(employee.EmployeeId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                catch (Exception ex)
                {
                    // Logga felet och visa ett meddelande till användaren
                    ModelState.AddModelError("", $"Ett fel uppstod: {ex.Message}");
                    if (ex.InnerException != null)
                    {
                        ModelState.AddModelError("", $"Detaljerad information: {ex.InnerException.Message}");
                    }

                    PopulateDropDowns();
                    return View(employee);
                }
                return RedirectToAction(nameof(Index));
            }

            // Om ModelState inte är giltig, återvänd till formuläret med felmeddelanden
            PopulateDropDowns();
            return View(employee);
        }

        // GET: Employees/Delete/5 - Visar bekräftelsesida för borttagning
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await _context.Employees
                .FirstOrDefaultAsync(m => m.EmployeeId == id);
            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }

        // POST: Employees/Delete/5 - Tar bort en anställd
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                var employee = await _context.Employees.FindAsync(id);
                if (employee != null)
                {
                    // Ta bort profilbilden om den finns
                    if (!string.IsNullOrEmpty(employee.ImagePath))
                    {
                        string imagePath = Path.Combine(_webHostEnvironment.WebRootPath, employee.ImagePath.TrimStart('/'));
                        if (System.IO.File.Exists(imagePath))
                        {
                            System.IO.File.Delete(imagePath);
                        }
                    }

                    _context.Employees.Remove(employee);
                }

                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                // Logga felet och visa ett meddelande till användaren
                ModelState.AddModelError("", $"Ett fel uppstod vid borttagning: {ex.Message}");
                return View(await _context.Employees.FindAsync(id));
            }
        }

        // Hjälpmetod för att kontrollera om en anställd finns
        private bool EmployeeExists(int id)
        {
            return _context.Employees.Any(e => e.EmployeeId == id);
        }

        // Hjälpmetod för att populera dropdown-listor
        private void PopulateDropDowns()
        {
            ViewBag.Genders = new List<SelectListItem>
            {
                new SelectListItem { Text = "Man", Value = "Man" },
                new SelectListItem { Text = "Kvinna", Value = "Kvinna" },
                new SelectListItem { Text = "Annat", Value = "Annat" }
            };

            ViewBag.EmploymentTypes = new List<SelectListItem>
            {
                new SelectListItem { Text = "Heltid", Value = "Heltid" },
                new SelectListItem { Text = "Deltid", Value = "Deltid" },
                new SelectListItem { Text = "Visstidsanställning", Value = "Visstidsanställning" }
            };

            ViewBag.Departments = new List<SelectListItem>
            {
                new SelectListItem { Text = "IT", Value = "IT" },
                new SelectListItem { Text = "HR", Value = "HR" },
                new SelectListItem { Text = "Försäljning", Value = "Försäljning" },
                new SelectListItem { Text = "Marknadsföring", Value = "Marknadsföring" },
                new SelectListItem { Text = "Ekonomi", Value = "Ekonomi" }
            };

            ViewBag.Statuses = new List<SelectListItem>
            {
                new SelectListItem { Text = "Aktiv", Value = "Aktiv" },
                new SelectListItem { Text = "Föräldraledig", Value = "Föräldraledig" },
                new SelectListItem { Text = "Sjukskriven", Value = "Sjukskriven" },
                new SelectListItem { Text = "Semester", Value = "Semester" }
            };
        }
    }
}