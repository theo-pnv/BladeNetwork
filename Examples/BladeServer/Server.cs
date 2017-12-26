using System;

namespace BladeServer
{
    using Msg = Tuple<blade.ClientConnection, string>;

    class Server
    {

        blade.Server _server;
        blade.ClientConnection _clientConnection;

        public Server()
        {
            string ip = "127.0.0.1";
            int port = 4242;

            _server = new blade.Server(ip, port, HandleReceive);

            DoSomething();
        }

        private void HandleReceive(object sender, blade.ClientConnectionEventArgs e)
        {
            string receivedMsg = e.clientConnection.Queue.Data.Dequeue();

            Console.WriteLine("Server received: {0}", receivedMsg);

            if (receivedMsg.Equals("CONNECT")) {
                _clientConnection = e.clientConnection;
            }
        }

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
                        Msg msg = new Msg(_clientConnection, input);

                        _server.AsyncSend(msg.Item1.Client.GetStream(), msg.Item2);
                        Console.WriteLine("Server sent \"{0}\" to client");
                    }

                }
            }
        }
    }
}
