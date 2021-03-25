# CursusAdministratie
## Inleiding
Het Info Support Kenniscentrum wil graag haar cursus- en cursistenadministratie geïntegreerd automatiseren. Dit project is een eerste stap in die richting.
## User Stories

- US01 – Cursusoverzicht bekijken  
Als medewerker van het secretariaat  
wil ik zien welke cursussen er deze en komende weken gegeven worden  
zodat ik daar voorbereidingen voor kan treffen en in de week zelf een lijst kan ophangen in de ontvangsthal 

- US02 – Cursussen invoeren  
Als coördinator opleidingen  
wil ik de geplande cursusinstanties eenvoudig in kunnen voeren in het systeem  
zodat het secretariaat er voorbereidingen voor kan treffen en het secretariaat er cursisten voor in kan schrijven

- US03 – Cursist inschrijven  
Als medewerker van het secretariaat  
wil ik een cursist voor een cursusinstantie in kunnen schrijven  
zodat we bij kunnen houden welke cursisten wanneer cursus komen volgen

- US04 – Cursusinschrijvingen bekijken  
Als medewerker van het secretariaat  
wil ik zien voor welke cursussen een cursist zich heeft ingeschreven  
zodat ik dat kan gebruiken in communicatie naar een cursist 

## To Start the project
### Install
- Visual Studio 2019 Preview - Version 16.10.0 Preview 1.0
- dotnet 6.0.100-preview.2.21155.3
### In Visual Studio
On the Package manager console run the following command
```
Update-Database -Project CursusAdministratie2021.Server.Infrastructure -StartupProject CursusAdministratie2021.Server
```
### Run the project by pressing F5
---

Examples of correct files to import can be found in the following folder
```
FilesWithCourses\Correct
```
Examples of incorrect files to import can be found in the following folder
```
FilesWithCourses\Incorrect
```
Docs with the description of the assignment can be found in the following folder
```
Docs
```