@ECHO OFF
::This batch file will create a database, update it with the latest migrations, and populate it with some test data, then it will run the back end application.

ECHO Creating Database

sqllocaldb create "MagniCollege"

cd Api

cd MagniCollegeMigrate

dotnet run

cd ../MagniCollege

dotnet run


