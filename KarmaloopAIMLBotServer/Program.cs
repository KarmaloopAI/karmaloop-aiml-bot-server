using System;
using System.Threading;
using System.Net.Sockets;
using System.Text;
using KarmaloopAIMLBot;
using KarmaloopAIMLBotServer.Skills;

namespace KarmaloopAIMLBotServer
{
    /// <summary>
    /// Main Program class.
    /// </summary>
	class Program
	{
        /// <summary>
        /// Entry point method. The server is initialized here.
        /// </summary>
        /// <param name="args"></param>
		static void Main(string[] args)
		{
			Console.WriteLine("Initializing Karmaloop AIML Bot Server...");
            var exitEvent = new ManualResetEvent(false);

            Console.CancelKeyPress += (sender, e) => {
                e.Cancel = true;
                Server.ForceStop = true;
                Console.WriteLine("Stopping the server...");
                Environment.Exit(0);
            };

            Server.Start();

            exitEvent.WaitOne();
            Server.ForceStop = true;
        }
        
    }
}
