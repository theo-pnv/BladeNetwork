![Blade Network.NET](Documentation/Readme/blade-network.png "Blade Network.NET")

## Description

This is a simple and lightweight C# asynchronous networking library.
It is named after a famous SF classic, because it is (will be) as strong and reliable as replicants.

### Features

- *Very simple to use !*
  - Just a few instructions, explained in the documentation below.
  - Easy to add as a reference (DLL) for an external project.
- TCP based implementation
- Efficient use of the processor (asynchronous read & write)
- Events raised at each reception of a message, for further processing (Observer pattern)

### TODO
- Serialization of messages
- Unit Tests

## Getting started

### Server
It is very simple to create a server. Either make it a variable, or inherit from the class.

    string ip = "127.0.0.1";
    port = 4242;
    var server = new blade.Server(ip, port, HandleReceive);
    // Server is now ready and starting to asynchronously accept clients

`HandleReceive` being the method where you process all your received commands.
It is a delegate called each time the server received a new message.

    private void HandleReceive(object sender, ClientConnectionEventArgs e)
    {
      // Access the message by :
      string[] substrings = e.clientConnection.Queue.Data.Dequeue();
    }

To send messages, you'll need a `Msg` variable :

    using Msg = Tuple<ClientConnection, string>;
    
    private void Send(Msg msg)
    {
      server.AsyncSend(msg.Item1.Client.GetStream(), msg.Item2);
    }

### Client
You must make the Client class inherit from `blade.AClient` :

    public class HelloWorldClient : blade.AClient
    {
      public HelloWorldClient(string ip, int port) : base(ip, port)
      {
        // Example of sending a message
        Send("Hello from a client !");
      }

      // Mandatory to implement
      public override void ReceiveHandler(object sender, QueueEventArgs e)
      {
        // To access the received message
        string msg = queue.Data.Dequeue();
      }
    }

`ReceiveHandler`, here again, is the method called each time a message is received by the server (Observer pattern). Implement your logic and message processing here.
