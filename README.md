![Logo](https://www.karmaloop.ai/wp-content/uploads/2018/01/KarmaloopAILogo-Black.png)


# Karmaloop AIML Bot Server

*Note: This is just a chatbot framework running as a server that uses AIML to build conversations. This is not the ONLY project of Karmaloop AI - on the contrary the real goal for Karmaloop AI is to build a new paradigm of conversational agents with a very unique and innovative approach - focus on "knowledge" and self learning abilities*

A simple AIML bot server written in C# with a dash of extendable "skills"! What this allows you to accomplish is to be able to build a chatbot capable of handling complex conversation dialogs, thanks to Dr. Richard Wallace's AIML, and be capable of running and handling a complex conversation set like that of ALICE.

The add on here is to have the ability of being capable of executing "skills" e.g. asking "What is the weather like in New Orleans?" would result in something like this - "The weather in New Orleans is 52 degrees Farhenheit and the condition is sunny" where the weather information is fetched in real time from an API.

# What can I build using this?

The practical uses are immense, as we will be covering a lot of this on the Karmaloop AI blog at https://www.karmaloop.ai
To start with, you can build a simple Customer Service Representative for your website that can answer all or most of the FAQs or you could go full monty and build a Siri or Alexa clone! No really! And yes we will be building one and publishing on our Github account.

# Getting Started

You can develop or deploy the bot server on any platform using the .NET Framework or Mono Framework. We would prefer using Mono Framework equivalent to .NET Framework 4.5 for running this bot server.
[Download binaries for the latest release](https://github.com/KarmaloopAI/karmaloop-aiml-bot-server/releases/download/v0.1.1/karmaloop-aiml-bot-server-v0.1.1.zip)

## Building a Chatbot
If you want to build just a chat bot then, you don't need any programming abilities, however you will still need to be adept at learning AIML and being able to build a conversation using AIML. Search for "AIML tutorials" and you should find several. You can use your favorite editor to get going on editing the AIML XML files.

The default release contains ALICE bot AIML files for demonstration purposes with some custom skills based AIML files e.g. zweather.aiml.

You can compile from source as described below or you can download the binaries from the link above. You should have two folders named "aiml" and "config" in the directory. Setup the AIML and configuration XML files to give your bot conversational abilities and a personality. Now run the KarmaloopAIMLBotServer.exe application and you should see a message on console saying "Server started".

### Running on Windows
Running on Windows is as simple as double clicking the KarmaloopAIMLBotServer.exe file or running it via command line. With most versions of Windows, you would have the .NET Framework 4.5 preinstalled, but in case it isn't, you must install it to make it work.

### Running on Linux / macOS
You will need to have the Mono Runtime installed.

#### Ubuntu
```
# sudo apt-get install mono-runtime
# mono KarmaloopAIMLBotServer.exe
```

#### macOS
Download and setup Mono for Mac OS X. [Get it here](http://www.mono-project.com/download/#download-mac)
Once you have Mono setup, you should be able to run the following command
```
# mono KarmaloopAIMLBotServer.exe
```

## Compiling from source
If you want to add your own skills to the bot, you will need to code in C# (more language options coming soon) and it certainly will be handy if you know how to build from source. You can build and debug as well using Visual Studio Community edition on Windows, MonoDevelop on Linux and Visual Studio for Mac on macOS.

### Building from source on Windows
To make this easy, there is a build.bat batch script that you can run. This will create a binaries directory with everything placed in appropriately.
```
C:\Users\Abi\Dev\karmaloop-aiml-bot-server\> build.bat
```

### Building from source on Linux / macOS
There is a build.sh script to help prepare the build for you on Linux and macOS. You may need to set the build engine correctly (edit the script to set the build engine - xbuild or msbuild). Also don't forget to give your build.sh file execute permissions.
```
# ./build.sh
```