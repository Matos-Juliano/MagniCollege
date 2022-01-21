@ECHO OFF
::Install the packges and run the front end application

ECHO Installing packages

cd ux

npm install

npm run ng serve -o

