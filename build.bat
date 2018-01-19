@echo off
REM Karmaloop AI (https://www.karmaloop.ai)
REM This batch script lets you build the Karmaloop AIML Bot Server binaries and place the config and aiml files in the right directories

ECHO Karmaloop AIML Bot Server
ECHO Prerequisites:
ECHO .NET Framework 4.5 and 4.0

REM Initialize configuration variables.
SET binariesDir=.\binaries
SET serverDir=.\KarmaloopAIMLBotServer
SET solutionName=KarmaloopAIMLBotServer.sln
SET configDir=.\%serverDir%\config
SET aimlDir=.\%serverDir%\aiml
SET msBuildDir=%WINDIR%\Microsoft.NET\Framework\v4.0.30319

REM Fix up directories, clean up and create what is needed
RD %binariesDir% /S /Q
RD %serverDir%\Bin\Release  /S /Q
MD %binariesDir%
MD %binariesDir%\config
MD %binariesDir%\aiml

REM Start the build process
ECHO Starting build process...
CALL %msBuildDir%\msbuild.exe  %solutionName% /p:Configuration=Release /l:FileLogger,Microsoft.Build.Engine;logfile=Manual_MSBuild_ReleaseVersion_LOG.log

REM Copy the freshly built binaries to the binaries directory and then copy the config and aiml files. These files are needed for the server to initialize.
XCOPY .\%serverDir%\Bin\Release\*.* %binariesDir%\
XCOPY %configDir%\*.* %binariesDir%\config\
XCOPY %aimlDir%\*.* %binariesDir%\aiml\

ECHO
ECHO
ECHO If you did not get the binaries built into the binaries folder, or if there was an error, please check you have the right .NET Framework installed.
