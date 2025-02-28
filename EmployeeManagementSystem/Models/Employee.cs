using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http;

namespace EmployeeManagementSystem.Models
{
    public class Employee
    {
        // Primärnyckel, anställningsnumret är ett unikt ID
        [Key]
        [Display(Name = "Anställningsnummer")]
        [Range(10000, 99999, ErrorMessage = "Anställningsnumret måste vara 5 siffror.")]
        // Ta bort DatabaseGenerated-attributet så att användaren kan ange egna anställningsnummer
        public int EmployeeId { get; set; }

        // Grundläggande personuppgifter
        [Required(ErrorMessage = "Förnamn är obligatoriskt.")]
        [Display(Name = "Förnamn")]
        [StringLength(50)]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Efternamn är obligatoriskt.")]
        [Display(Name = "Efternamn")]
        [StringLength(50)]
        public string LastName { get; set; }

        [Display(Name = "Fullständigt namn")]
        public string FullName => $"{FirstName} {LastName}";

        [Display(Name = "Kön")]
        public string Gender { get; set; }

        [Required(ErrorMessage = "Adress är obligatorisk.")]
        [Display(Name = "Adress")]
        [StringLength(100)]
        public string Address { get; set; }

        [Required(ErrorMessage = "Mobilnummer är obligatoriskt.")]
        [Display(Name = "Mobilnummer")]
        [Phone(ErrorMessage = "Ogiltig telefonnummerformat.")]
        [StringLength(20)]
        public string MobilePhone { get; set; }

        [Display(Name = "Arbetsnummer")]
        [Phone(ErrorMessage = "Ogiltig telefonnummerformat.")]
        [StringLength(20)]
        public string WorkPhone { get; set; } = ""; // Ge ett tomt standardvärde

        [Required(ErrorMessage = "E-postadress är obligatorisk.")]
        [Display(Name = "E-postadress")]
        [EmailAddress(ErrorMessage = "Ogiltig e-postadress.")]
        [StringLength(100)]
        public string Email { get; set; }

        [Required(ErrorMessage = "Födelsedatum är obligatoriskt.")]
        [Display(Name = "Födelsedatum")]
        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }

        [Display(Name = "Ålder")]
        public int Age => CalculateAge(DateOfBirth);

        // Anställningsinformation
        [Required(ErrorMessage = "Anställningstyp är obligatorisk.")]
        [Display(Name = "Anställningstyp")]
        public string EmploymentType { get; set; } // Heltid, deltid, visstid

        [Required(ErrorMessage = "Startdatum är obligatoriskt.")]
        [Display(Name = "Startdatum")]
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }

        [Display(Name = "Slutdatum")]
        [DataType(DataType.Date)]
        public DateTime? EndDate { get; set; } // Nullable för tillsvidareanställningar

        [Required(ErrorMessage = "Position är obligatorisk.")]
        [Display(Name = "Position/Titel")]
        [StringLength(50)]
        public string Position { get; set; }

        [Required(ErrorMessage = "Avdelning är obligatorisk.")]
        [Display(Name = "Avdelning")]
        [StringLength(50)]
        public string Department { get; set; }

        [Display(Name = "Arbetstider")]
        [StringLength(100)]
        public string WorkingHours { get; set; } = "";

        [Required(ErrorMessage = "Anställningsgrad är obligatorisk.")]
        [Display(Name = "Anställningsgrad")]
        [Range(1, 100, ErrorMessage = "Anställningsgrad måste vara mellan 1 och 100.")]
        public int EmploymentRate { get; set; } // Procent

        [Display(Name = "Status")]
        public string Status { get; set; } // T.ex. aktiv, föräldraledig, sjukskriven

        [Display(Name = "Noteringar")]
        [StringLength(500)]
        public string Notes { get; set; } = ""; // Ge ett tomt standardvärde

        // Bild
        [Display(Name = "Profilbild")]
        [StringLength(200, ErrorMessage = "Sökvägen får inte vara längre än 200 tecken.")]
        public string ImagePath { get; set; } = "/images/default-avatar.png"; // Ge ett standardvärde

        [NotMapped] // Detta fält sparas inte i databasen
        [Display(Name = "Ladda upp bild")]
        public IFormFile ImageFile { get; set; }

        // Hjälpmetod för att beräkna ålder baserat på födelsedatum
        private int CalculateAge(DateTime dateOfBirth)
        {
            var today = DateTime.Today;
            var age = today.Year - dateOfBirth.Year;
            if (dateOfBirth.Date > today.AddYears(-age)) age--;
            return age;
        }
    }
}