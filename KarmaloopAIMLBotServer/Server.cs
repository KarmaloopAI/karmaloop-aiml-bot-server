using KarmaloopAIMLBot;
using System;
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

        #endregion

        #region Static Methods
        /// <summary>
        /// Starts the server in an infinite loop waiting for connections.
        /// </summary>
        public static void Start()
        {
            AIMLBot myBot = new AIMLBot();
            myBot.LoadSettings();

            BotUser myUser = new BotUser(Guid.NewGuid(), myBot);
            DefaultUser = myUser;

            myBot.IsAcceptingInput = false;
            myBot.LoadAIMLFromFiles();
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
