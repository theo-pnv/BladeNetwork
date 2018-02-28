![Blade Network.NET](Documentation/Readme/blade-network.png "Blade Network.NET")

[![Build status](https://ci.appveyor.com/api/projects/status/4a3b1ytxff7bf7q1?svg=true)](https://ci.appveyor.com/project/theo-pnv/bladenetwork)
![NuGet](https://img.shields.io/nuget/v/Nuget.Core.svg)
![license](https://img.shields.io/github/license/mashape/apistatus.svg)

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

It works very well with Netwonsoft (https://www.newtonsoft.com/json). With this package installed, you are able to pass complex objects between server and client.

## Getting started

A Nuget package is available : https://www.nuget.org/packages/BladeNetwork/

There is a complete & well-documented example available in the `Examples` directory.
It shows a very basic implementation of a running server and associated client.

### Server-side [(Example)](Examples/BladeServer/Server.cs)

**Initialization:**

    var server = new blade.Server(ip, port, MsgHandler);

**Send messages:**

    server.Send(target, "Hello from server !");

**Receive messages:**

Implement the `MsgHandler` function : it will be called each time a message is received by the server.
You can then parse it there.

### Client-side [(Example)](Examples/BladeClient/Client.cs)

**Initialization:**

    var client = new blade.Client(ip, port, MsgHandler);

**Send messages:**

    client.Send("Hello from client !");

**Receive messages:**

Implement the `MsgHandler` function : it will be called each time a message is received by the server.
You can then parse it there.

It's as simple as that !