using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BladeClient
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var client = new Client();
            } catch (Exception) {
                Console.WriteLine("Start a server before this client. Type something to quit");

                ConsoleKeyInfo input = Console.ReadKey();
                System.Environment.Exit(1);
            }
        }
    }
}
