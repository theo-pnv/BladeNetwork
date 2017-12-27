![Blade Network.NET](Documentation/Readme/blade-network.png "Blade Network.NET")

## Description

This is a simple and lightweight C# asynchronous networking library.
It is named after a famous SF classic, because it is as strong and reliable as replicants.

### Features

- *Very simple to use !*
  - Just a few instructions, explained in the documentation below.
  - Easy to add as a reference (DLL) for an external project.
- TCP based implementation
- Efficiency (asynchronous read & write)
- Events raised at each reception of a message, for further processing (Observer pattern)

### TODO
- Serialization of messages
- Unit Tests

## Getting started

There is a complete & well-documented example available in the `Examples` directory.
It shows a very basic implementation of a running server and associated client.

### Server-side [(Example)](Examples/BladeServer/Server.cs)

**Initialization:**

    var server = new blade.Server(ip, port, MsgHandler);

**Send messages:**

    Send(target, "Hello from server !");

**Receive messages:**

Implement the `MsgHandler` function : it will be called each time a message is received by the server.
You can then parse it there.

### Client-side [(Example)](Examples/BladeClient/Client.cs)

**Initialization:**

Make your client class inherit from blade.AClient and send it an ip and port.

**Send messages:**

    Send("Hello from client !");

**Receive messages:**

Implement the `MsgHandler` function : it will be called each time a message is received by the server.
You can then parse it there.

It's as simple as that !