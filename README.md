# SocketLibrary
Simple library for socket communication

# Usage
This is a basic example of client server communication. It made up of 3 seperate projects. All of the project will need SocketLibrary added as a reference.
You can find the full code in the example folder of the solution.

### Shared code
The first one is a library project that is going to contain code that will be shared by the server and the client.
Here we are going to create classes implementing IPacket. These classes will be able to be sent from client to server and vice versa.
One example class only containing text:
```cs
[Serializable]
[PacketDescription("Text Packet", "Simple Packet transferring text")]
public class Text : IPacket
{
  public string Content;

  public Text(string content)
  {
    Content = content;
  }
}
```
The PacketDescription tag is optional. If set it provides the additional information in the console when debugging is enabled.

### Client code
The client is a simple console project. It is going to create a new instance of the text class and will send it to the server:
```cs
static void Main(string[] args)
{
  var connection = new UnsecuredUdpConnection(51001);
  var serverAddress = IPAddress.Parse("127.0.0.1");
  var recipient = new IPEndPoint(serverAddress, 51000);

  connection.Send(new Text("Test test test 123"), recipient);
  Console.ReadKey();
}
```

### Server code
The server is a simple console project as well.
The server will subscribe to the MessageReceived function of the SocketClient and handle the result.
The server needs a handle implementation for every object the client could send (every object implementing IPacket)
```cs
static void Main(string[] args)
{
  var connection = new UnsecuredUdpConnection(51000);
  connection.OnMessageReceived += HandleResult;
  Console.ReadKey();
}

static void HandleResult(object sender, SocketEventArgs e)
{
  Handle(e.Packet);
}

static void Handle(Text text)
{
  Console.WriteLine("Received: " + text.Content);
}
```

This architecture allows for easy socket communication as well as handling of responses without huge rows of if/else statements.

---
> [JKamue.de](https://www.jkamue.de) &nbsp;&middot;&nbsp;
> Twitter [@JKamue_dev](https://twitter.com/JKamue_dev)
