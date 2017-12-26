using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using blade;

namespace BladeClient
{
    class Client : blade.AClient
    {
        public Client() :
            base("127.0.0.1", 4242)
        {
            Send("CONNECT");
            DoSomething();
        }

        public override void MsgHandler(object sender, QueueEventArgs e)
        {
            string receivedMsg = queue.Data.Dequeue();

            Console.WriteLine("Client received: {0}", receivedMsg);
        }

        private void DoSomething()
        {
            bool quit = false;

            while (!quit)
            {
                Console.WriteLine("Send a sentence to server or type \"QUIT\" to exit :");

                string input = Console.ReadLine();

                if (input.Equals("QUIT"))
                {
                    System.Environment.Exit(0);
                }
                else
                {
                    Send(input);
                    Console.WriteLine("Client sent \"{0}\" to server", input);
                }
            }
        }
    }
}
