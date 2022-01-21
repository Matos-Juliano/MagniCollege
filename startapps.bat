@ECHO OFF
::This batch file just run the other 2 batch files

start cmd /k call Front.bat 4444

start cmd /k call back.bat 5555

PAUSE

