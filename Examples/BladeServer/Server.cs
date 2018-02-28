using System;

namespace BladeServer
{

    /// <summary>
    /// Your business class, acting as the server
    /// </summary>
    class Server
    {
        // Variable holding the blade Server
        blade.Server _server;
        // Var holding the client connected. You can of course register clients in a list
        blade.ClientConnection _clientConnection; 

        public Server()
        {
            string ip = "127.0.0.1";
            int port = 1971;

            // Instantiating the blade server. DO NOT forget the MsgHandler
            _server = new blade.Server(ip, port, MsgHandler);

            DoSomething();
        }

        /// <summary>
        /// Will be called each time a message is received over the network.
        /// Add your parsing logic here.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MsgHandler(object sender, blade.ClientConnectionEventArgs e)
        {
            // Access to the message
            string receivedMsg = e.clientConnection.Queue.Data.Dequeue();

            Console.WriteLine("Server received: {0}", receivedMsg);

            if (receivedMsg.Equals("CONNECT")) {
                // Access to the client that sent the message
                _clientConnection = e.clientConnection;
            }

            if (receivedMsg.Equals("DISCONNECT")) {
                _clientConnection = null;
            }
        }

        /// <summary>
        /// Main thread and process.
        /// We can send messages from here.
        /// </summary>
        private void DoSomething()
        {
            bool quit = false;

            while (!quit) {
                Console.WriteLine("Send a sentence to client or type \"QUIT\" to exit :");

                string input = Console.ReadLine();

                if (input.Equals("QUIT")) {
                    System.Environment.Exit(0);
                } else {
                    if (_clientConnection == null) {
                        Console.WriteLine("Please start a client");
                    } else {
                        // Call Send() to send a message to a specific client
                        _server.Send(_clientConnection, input);
                        Console.WriteLine("Server sent \"{0}\" to client", input);
                    }

                }
            }
        }
    }
}
