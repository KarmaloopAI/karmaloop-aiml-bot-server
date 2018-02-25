using KarmaloopAIMLBot;
using KarmaloopAIMLBotServer.API;
using Microsoft.Owin.Hosting;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net;
using System.Net.Sockets;

namespace KarmaloopAIMLBotServer
{
    /// <summary>
    /// Server class to start the TCP server
    /// </summary>
    public class Server
    {
        #region Static Properties
        /// <summary>
        /// Flag to force stop the server
        /// </summary>
        public static bool ForceStop = false;

        /// <summary>
        /// Active Bot object.
        /// </summary>
        public static AIMLBot ActiveBot { get; set; }

        /// <summary>
        /// Default user to initialize the bot with. TODO: This needs to be unique per client request.
        /// </summary>
        public static BotUser DefaultUser { get; set; }

        /// <summary>
        /// Hash lookup for bot users
        /// </summary>
        public static Dictionary<string, BotUser> Users { get; set; }

        /// <summary>
        /// TCP/IP Port to listen on
        /// </summary>
        public static int Port
        {
            get
            {
                int port = 8888;
                if (ConfigurationManager.AppSettings["port"] != null)
                    port = Convert.ToInt32(ConfigurationManager.AppSettings["port"]);

                return port;
            }
        }

        /// <summary>
        /// IP Address to bind to. Default is any ip.
        /// </summary>
        public static IPAddress IPAddress
        {
            get
            {
                IPAddress ipAddress = IPAddress.Any;
                if (ConfigurationManager.AppSettings["ip"] != null && ConfigurationManager.AppSettings["ip"].ToString().Trim() != "0.0.0.0")
                    ipAddress = IPAddress.Parse(ConfigurationManager.AppSettings["ip"].ToString());

                return ipAddress;
            }
        }

        /// <summary>
        /// The base URL where the API endpoint will be accessible
        /// </summary>
        public static string ApiBaseUrl
        {
            get
            {
                string baseUrl = "http://*:8880/";
                if (ConfigurationManager.AppSettings["apiBaseUrl"] != null)
                    baseUrl = ConfigurationManager.AppSettings["apiBaseUrl"].ToString().ToLower();

                return baseUrl;
            }
        }

        #endregion

        #region Static Methods
        /// <summary>
        /// Starts the server in an infinite loop waiting for connections.
        /// </summary>
        public static void Start()
        {
            //1. Start the API endpoint.
            try
            {
                WebApp.Start<Startup>(url: ApiBaseUrl);
                Console.WriteLine("API Server endpoint started.");
            }
            catch(Exception ex)
            {
                Console.WriteLine(string.Concat("ERROR: Could not start the API server endpoint. Possibly a permissions issue."
                    , Environment.NewLine, ex.Message, Environment.NewLine
                    , "If you are on a Windows machine, try running this command as an Administrator:", Environment.NewLine
                    , "netsh http add urlacl url=http://*:8880/ user=Everyone listen=yes", Environment.NewLine
                    , "Don't forget to change the port number (8880 above) as per your App.config settings."));
            }

            //2. Setup the Bot engine and start the TCP server
            AIMLBot myBot = new AIMLBot();
            try
            {
                myBot.LoadSettings();
                myBot.LoadAIMLFromFiles();
            }
            catch (Exception ex)
            {
                Console.WriteLine(string.Concat(ex.Message, Environment.NewLine,
                    "Could not load the bot settings and/or AIML files. ",
                    "Please ensure that config and aiml folders exist in the same directory as your .exe file or the directory as per your configuration."
                    ));

                Environment.Exit(0);
            }

            Users = new Dictionary<string, BotUser>();
            Guid defaultId = Guid.NewGuid();
            BotUser myUser = new BotUser(defaultId, myBot);
            DefaultUser = myUser;

            Users[defaultId.ToString()] = myUser;

            myBot.IsAcceptingInput = false;
            myBot.IsAcceptingInput = true;
            ActiveBot = myBot;

            TcpListener serverSocket = new TcpListener(IPAddress, Port);
            TcpClient clientSocket = default(TcpClient);
            int counter = 0;

            serverSocket.Start();
            Console.WriteLine(" >> " + "Server Started");

            counter = 0;
            while (!ForceStop)
            {
                counter += 1;
                clientSocket = serverSocket.AcceptTcpClient();
                Console.WriteLine(" >> " + "Client No:" + Convert.ToString(counter) + " started!");
                KarmaloopClient client = new KarmaloopClient();
                client.StartClient(clientSocket, Convert.ToString(counter));
            }
        }
        #endregion
    }
}
