@ECHO OFF
::Install the packges and run the front end application

ECHO Installing packages

cd ux

START npm install

npm run ng serve -o

PAUSE

