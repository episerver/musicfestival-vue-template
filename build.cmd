@ECHO OFF
SETLOCAL

REM Set msbuild path for CI runs
SET PATH=%msbuild_path%;%PATH%

REM Set Release or Debug configuration. Default to Debug.
IF "%1"=="Release" (set CONFIGURATION=Release) ELSE (set CONFIGURATION=Debug)
ECHO Building in %CONFIGURATION%

REM Build Server
CALL msbuild MusicFestival.Vue.Template.sln /m /property:Configuration=%CONFIGURATION%
IF %errorlevel% NEQ 0 EXIT /B %errorlevel%

REM Build Client
IF "%CONFIGURATION%"=="Release" ( CALL npm run build ) ELSE ( CALL npm run build:dev )
IF %errorlevel% NEQ 0 EXIT /B %errorlevel%

EXIT /B %ERRORLEVEL%
