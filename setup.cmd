@ECHO OFF
SETLOCAL

REM Clear node modules before installing
CALL npm ci
IF %errorlevel% NEQ 0 EXIT /B %errorlevel%

REM Restore nuget packages
CALL .\.nuget\nuget.exe restore MusicFestival.Vue.Template.sln
IF %errorlevel% NEQ 0 EXIT /B %errorlevel%

REM Set up database
SET MusicFestival=src\MusicFestival.Vue.Template
IF EXIST %MusicFestival%\App_Data (
    ECHO Remove all files from the app data folder
    DEL %MusicFestival%\App_Data\*.* /F /Q || Exit /B 1
) ELSE (
    MKDIR %MusicFestival%\App_Data || Exit /B 1
)

REM Copy the database files to the site.
XCOPY /y/i build\database\DefaultSiteContent.episerverdata %MusicFestival%\App_Data\ || Exit /B 1
XCOPY /y/i/k build\database\musicfestival.mdf %MusicFestival%\App_Data\ || Exit /B 1
XCOPY /y/i/k build\database\GeoLiteCity.dat %MusicFestival%\App_Data\ || Exit /B 1
XCOPY /y/i build\automation\ProvisionDatabase_MVC.cs %MusicFestival%\App_Code\ || Exit /B 1

EXIT /B %ERRORLEVEL%
