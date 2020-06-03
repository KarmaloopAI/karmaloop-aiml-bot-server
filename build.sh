#!/bin/bash
#Karmaloop AIML Bot Server build script for Ubuntu/Linux

echo "Karmaloop AIML Bot Server"
echo "Prerequisites:"
echo "mono-runtime and mono-xbuild (or msbuild on newer versions of mono)"
echo "------------------------"
echo "You will need to ensure you have the dependencies installed before continuing."
echo "On Ubuntu you can run 'sudo apt-get install mono-runtime nuget mono-xbuild' to install the packages."
echo
echo 

#Initialize configuration variables
binariesDir=./binaries
serverDir=./KarmaloopAIMLBotServer
solutionName=KarmaloopAIMLBotServer.sln
configDir=./$serverDir/config
aimlDir=./$serverDir/aiml
wwwDir=./$serverDir/www
skillScriptsDir=./$serverDir/SkillScripts
buildEngine=xbuild 	#change the build engine to msbuild for newer versions of mono

#Check for pre-requisites
command -v mono >/dev/null 2>&1 || { echo >&2 "I require mono but it's not installed. Install the package mono-runtime.  Aborting."; exit 1; }

command -v $buildEngine >/dev/null 2>&1 || { echo >&2 "I require a build engine like xbuild or msbuild but it's not installed. Install the package mono-xbuild. Aborting."; exit 1; }

echo "Dependencies found."

#Fix up directories, clean up and create what is needed
rm -r $binariesDir
rm -r $serverDir/bin/Release
mkdir $binariesDir
mkdir $binariesDir/config
mkdir $binariesDir/aiml
mkdir $binariesDir/www
mkdir $binariesDir/SkillScripts

#Restore nuget packages
mono nuget.exe restore

#Start the build process
echo "Starting build process..."
$buildEngine $solutionName /p:Configuration=Release

#Copy the freshly built binaries to the binaries directory and then copy the config and aiml files. These files are needed for the server to initialize.
cp -R $serverDir/bin/Release/*.* $binariesDir/
cp -R $configDir $binariesDir/
cp -R $aimlDir $binariesDir/
cp -R $wwwDir $binariesDir/
cp -R $skillScriptsDir $binariesDir/

echo "If you did not get the binaries built, please check dependencies."
