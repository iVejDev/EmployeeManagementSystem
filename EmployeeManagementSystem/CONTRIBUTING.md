# Bidra till Personalhanteringssystemet

Tack för att du är intresserad av att bidra till vårt personalhanteringssystem! Här är några riktlinjer för att hjälpa dig bidra på bästa sätt.

## Utvecklingsmiljö

Innan du börjar, se till att du har följande installerat:

- .NET SDK 8.0 eller senare
- Visual Studio 2022 eller liknande IDE
- SQL Server (LocalDB fungerar bra för utveckling)
- Git

## Arbetsflöde

1. **Skapa en Issue**
   - Innan du börjar arbeta på en förändring, skapa en issue som beskriver vad du planerar att göra.
   - Vänta på feedback från projektunderhållarna.

2. **Klona och förgrena**
   - Forka repot till ditt eget GitHub-konto.
   - Klona din fork till din lokala maskin: `git clone https://github.com/iVejDev/EmployeeManagementSystem.git
   - Skapa en feature-branch: `git checkout -b feature/min-nya-funktion`

3. **Utveckla**
   - Implementera din ändring eller funktion.
   - Följ kodningsriktlinjerna (se nedan).
   - Håll ändringar fokuserade på att lösa en specifik uppgift.

4. **Testa**
   - Se till att all existerande funktionalitet fortfarande fungerar.
   - Lägg till tester för ny funktionalitet om möjligt.
   - Kontrollera att koden kompilerar utan varningar.

5. **Commita**
   - Använd meningsfulla commit-meddelanden som förklarar vad ändringen gör.
   - Format: `Kategori: Kort beskrivning av ändringen`
   - Exempel: `Feature: Lägg till exportfunktion för anställdas data`

6. **Pusha och skapa PR**
   - Pusha din branch till din fork: `git push origin feature/min-nya-funktion`
   - Skapa en Pull Request mot huvudrepot.
   - Referera till den Issue du skapade i PR-beskrivningen.

7. **Kodgranskning**
   - Vänta på feedback från projektunderhållarna.
   - Gör ändringar baserat på feedback om det behövs.

## Kodningsriktlinjer

- Följ C# kodningsstandarder och namnkonventioner.
- Använd svensk namngivning för gränssnittselement (vyer, knappar etc.) och engelska för tekniska komponenter (klasser, metoder).
- Kommentera kod där det behövs, särskilt för komplexa logikdelar.
- Håll metoder korta och fokuserade på en specifik uppgift.
- Använd beskrivande, meningsfulla variabel- och metodnamn.

## Specifika riktlinjer för detta projekt

### Filstruktur

- Håll controller-actions korta och läsbara.
- Placera databasrelaterad kod i Data-mappen.
- Lägg View-specific kod i Views-mappen.

### UI/UX-riktlinjer

- Följ designmönstret som redan finns i projektet.
- Se till att alla gränssnittselement är responsive och fungerar på mobila enheter.
- Använd Bootstrap-klasser konsekvent för layout och styling.
- Stöd både ljust och mörkt tema för alla nya funktioner.

### Språk

- Använd svenska i användargränssnittet.
- Använd engelska för kodkommentarer och dokumentation.

## Pull Requests

För att din Pull Request ska bli godkänd, se till att:

- Den löser ett specifikt problem eller implementerar en efterfrågad funktion.
- Koden följer projektets riktlinjer och standarder.
- Alla tester passerar.
- Dokumentation har uppdaterats om det behövs.

Tack för ditt bidrag till att göra detta projekt bättre!