using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using blade;

namespace BladeClient
{
    /// <summary>
    /// Your business class, acting as the client
    /// </summary>
    class Client : blade.AClient
    {
        public Client() :
            base("127.0.0.1", 4242)
        {
            // Send a ping to the server allows it to register this client and to send it messages.
            Send("CONNECT");
            DoSomething();
        }

        /// <summary>
        /// Will be called each time a message is received from the server.
        /// Add your parsing logic here.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public override void MsgHandler(object sender, QueueEventArgs e)
        {
            string receivedMsg = queue.Data.Dequeue();

            Console.WriteLine("Client received: {0}", receivedMsg);
        }

        /// <summary>
        /// Main thread and process.
        /// We can send messages to the server from here.
        /// </summary>
        private void DoSomething()
        {
            bool quit = false;

            while (!quit)
            {
                Console.WriteLine("Send a sentence to server or type \"QUIT\" to exit :");

                string input = Console.ReadLine();

                if (input.Equals("QUIT"))
                {
                    // Let the server know you're off
                    Send("DISCONNECT");
                    System.Environment.Exit(0);
                }
                else
                {
                    // Will send a message directly to the server
                    Send(input);
                    Console.WriteLine("Client sent \"{0}\" to server", input);
                }
            }
        }
    }
}
