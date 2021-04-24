# HorekoFoodApi

Technologies
•	DotNet Core Api (.Net Core 3.1)
•	Microsoft.EntityFrameworkCore.Sqlite Version=5.0.5
•	Microsoft.EntityFrameworkCore.Sqlite Version=5.0.5
•	Microsoft.EntityFrameworkCore Version=5.0.5


-This project is worked in Visual Studio Code for working with SQLite.

-For starting with this project, you need to Download the repositority or clone it and open it with Visual Studio Code.
-When the repository is downloaded, and opened in vs code first run the application (For this part you need to open terminal and in WebApi project write the command:
"dotnet watch run" for starting the application.

-Seeding the database
  -For this part in this project is used SQLite. You need to install SQLite. When all is installed do strl+shift+P, the dropdown is oppened, select SQLite:Open Database and on the
  left in menu tab Explorer and submenu SQLite explorer you can see the database horeko.db. 
  
  -Because database is filled with data, i have written seed methods for seeding from json files. To check the functionality of those methods, you need to empty databases and 
  run the Seed methodes in SeedData Controller.
  
  For checking the functionality of assignment the Swagger is installed and is runned immediately when we run the command in WebAPI project:"dotnet watch run".
  
