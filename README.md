![Logo](https://www.karmaloop.ai/wp-content/uploads/2018/01/KarmaloopAILogo-Black.png)


# Karmaloop AIML Bot Server

A simple AIML bot server written in C# with a dash of extendable "skills"! What this allows you to accomplish is to be able to build a chatbot capable of handling complex conversation dialogs, thanks to Dr. Richard Wallace's AIML, and be capable of running and handling a complex conversation set like that of ALICE.

The add on here is to have the ability of being capable of executing "skills" e.g. asking "What is the weather like in New Orleans?" would result in something like this - "The weather in New Orleans is 52 degrees Farhenheit and the condition is sunny" where the weather information is fetched in real time from an API.

*Note: This is just a chatbot framework running as a server that uses AIML to build conversations. This is not the ONLY project of Karmaloop AI - on the contrary the real goal for Karmaloop AI is to build a new paradigm of conversational agents with a very unique and innovative approach - focus on "knowledge" and self learning abilities*

# What can I build using this?

The practical uses are immense, as we will be covering a lot of this on the Karmaloop AI blog at https://www.karmaloop.ai
To start with, you can build a simple Customer Service Representative for your website that can answer all or most of the FAQs or you could go full monty and build a Siri or Alexa clone! No really! And yes we will be building one and publishing on our Github account.

# Getting Started

It is super easy to start with [Karmaloop AI Open Source Chat Bot Framework](https://www.karmaloop.ai). All you need to do is download the binaries, make sure you have the .NET Framework or Mono installed, and using it to fire up the KarmaloopAIMLBotServer.exe binary. Platform specific instructions are discussed below.

You can develop or deploy the bot server on any platform using the .NET Framework or Mono Framework. We would prefer using Mono Framework equivalent to .NET Framework 4.5 for running this bot server.

[Download binaries for the latest release](https://github.com/KarmaloopAI/karmaloop-aiml-bot-server/releases/download/v0.1.2/karmaloop-aiml-bot-server-binaries.zip)

[Read the release notes or download release source code](https://github.com/KarmaloopAI/karmaloop-aiml-bot-server/releases/tag/v0.1.2)

## Running on Windows

Running on Windows is as simple as double clicking the KarmaloopAIMLBotServer.exe file or running it via command line. With most versions of Windows, you would have the .NET Framework 4.5 preinstalled, but in case it isn't, you must install it to make it work.

To run on Windows (any edition)
1. Goto the folder you have extracted the binaries to
2. Double click the KarmaloopAIMLBotServer.exe application (make sure you are not accidentally selecting the KarmaloopAIMLBotServer.exe.config file which is the configuration file for your server)
3. Check the port numbers and API endpoint URL. You may need to run the netsh command to ensure the API can start listening on the designated port. Below is an example.

```
netsh http add urlacl url=http://*:8880/ user=Everyone listen=yes
```

Now you can run the below command from the command line or you can start by double clicking the KarmaloopAIMLBotServer.exe
```
C:\Your\Extract\Directory\> KarmaloopAIMLBotServer.exe
```

See the output section to verify if you are seeing something similar.

## Running on Linux / macOS
You will need to have the Mono Runtime installed. I have not tested it with .NET Core yet, but would be happy to hear from someone who has tried to do that.

### Ubuntu
On Ubuntu, it's as easy as it is to run on Windows, possibly easier! Just get the mono-runtime package installed and you should be good to go.
1. Goto the folder you have extracted the binaries to
2. Right-click and say Open in Terminal
3. Verify you have mono installed by typing the command "mono --version"
4. If mono-runtime is not installed, you need to get that as described below
5. Run the exe
```
# sudo apt-get install mono-runtime
# mono KarmaloopAIMLBotServer.exe
```

See the output section to verify if you are seeing something similar.

### macOS
Download and setup Mono for Mac OS X. [Get it here](http://www.mono-project.com/download/#download-mac)
Once you have Mono setup, you should be able to run the following command from the directory where you have extracted the binaries
```
# mono KarmaloopAIMLBotServer.exe
```

See the output section to verify if you are seeing something similar.

## Output (on the Terminal window)
Once you successfully run the KarmaloopAIMLBotServer.exe application, you should see something similar:

```
Initializing Karmaloop AIML Bot Server...
  API Server endpoint started on URL http://*:8880/
  Karmaloop AIML Bot Server Started on port 8888
----------------------------------------------------------------  

Take the bot for a spin! Try hitting the URL http://localhost:8880/api/ChatUi/index.html from your local browser!

```

## Building a Chatbot
If you want to build just a chat bot then, you don't need any programming abilities, however you will still need to be adept at learning AIML and being able to build a conversation using AIML. Search for "AIML tutorials" and you should find several. You can use your favorite editor to get going on editing the AIML XML files.

The default release contains ALICE bot AIML files for demonstration purposes with some custom skills based AIML files e.g. zweather.aiml.

You can compile from source as described below or you can download the binaries from the link above. You should have two folders named "aiml" and "config" in the directory. Setup the AIML and configuration XML files to give your bot conversational abilities and a personality. Now run the KarmaloopAIMLBotServer.exe application and you should see messages on the terminal window similar to the output shown in the Output section above.

### Talking to the Chatbot
Once the server is started, there are three ways you can communicate with the chat bot, each way depending on the one below it. More explained below.

#### The Simple built-in Chat UI
With the latest release, we added a built-in chat UI that you can use to instantly gratify yourself by talking to the chat bot. If you haven't touched any of the configuration options then you should be able to access the default URL.

1. Open your favorite browser (which we hope you have updated not in the distant past, or else Material Design/AngularJS will fail to render)
2. Open the URL http://localhost:8880/api/ChatUi/index.html (yes you must specify index.html at the end, at least for now)
3. Happy chatting!

![Chatting with Karmaloop AI](https://www.karmaloop.ai/wp-content/uploads/2018/03/Chatting-with-KarmaloopAI.jpg)

#### Web API endpoint
In most likelihood you will be setting up the chat bot to work from your website or mobile app so that it looks and feels like part of your solution. The API endpoints are extremely easy to use and consume in your own app. It's mostly two simple API calls that you need to use, as explained below.

##### **NEW USER**
The [Chatbot framework](https://www.karmaloop.ai) needs to distinguish between users who are chatting with it, and for this purpose it lets you generate a unique ID which you can use to subsequently post chat messages. It does not do any user management whatsoever apart from simply generating a GUID which acts as a handle to remember the user and the conversation context.

```
GET http://localhost:8880/api/Conversation/new
```

This should generate a response similar to the following

```
{
    "Code": "SUCCESS",
    "State": "NORMAL",
    "UserID": "578eced6-490c-4e6d-a2c8-ac84a5bb8630"
}
```

##### **POSTING A CHAT MESSAGE**
To send a chat message to the bot, you simple post in the following format

```
POST http://localhost:8880/api/Conversation/578eced6-490c-4e6d-a2c8-ac84a5bb8630

Data that is posted (application/json)

{
  Sentence: 'Hello!'
}
```

This should generate a response similar to the one below

```
{
    "ResponseText": "Hi there!",
    "Code": "SUCCESS",
    "State": "NORMAL",
    "UserID": "578eced6-490c-4e6d-a2c8-ac84a5bb8630"
}
```

Yes, that's it! The ResponseText is what you will be most concerned about in your application.

#### TCP/IP Socket Communication
This method of communication is just as simple as using the API. You will need to generate a unique ID or GUID on your own however. Currently it does not support returning one for you.

By default the server listens on the port number 8888. You can verify if the server is listening by doing a
```
telnet localhost 8888
```
If it connects, you are good.

You can input a JSON string as follows to the connection, and should get a response back.
```
{
  "question": "How are you?",
  "userid": "578eced6-490c-4e6d-a2c8-ac84a5bb8630"
}
```
While this method is the lowest level of communication possible with the bot, and definitely means the fastest way as well, using the REST API is the preferred approach owing to its simplicity and widespread applicability.

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