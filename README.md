# SignalR Events From Server (POC)

This shows how to send events via SignalR from the server. 

I start from the MSDN SignalR tutorial. I create a broker that encapsulates the HubContext. 

```cs
public class ChatHub : Hub<IChatHubClient> { ... }
```

```cs
public interface IChatHubClient
{
    Task ReceiveMessage(string user, string message);
}
```

```cs
public class HubContextBroker : IHubContextBroker
{
    private readonly IHubContext<ChatHub, IChatHubClient> hubContext;

    public HubContextBroker(IHubContext<ChatHub, IChatHubClient> hubContext) =>
        this.hubContext = hubContext;

    public async ValueTask SendMessageToAllClients(string sender, string message) =>
        await this.hubContext.Clients.All.ReceiveMessage(sender, message);
}

```
I included the use of a strongly typed client in the demonstration however it is not required:
```cs
public class ChatHub : Hub { ... }
```

```cs
public class HubContextBroker : IHubContextBroker
{
    private readonly IHubContext<ChatHub> hubContext;

    public HubContextBroker(IHubContext<ChatHub> hubContext) =>
        this.hubContext = hubContext;

    public async ValueTask SendMessageToAllClients(string sender, string message) =>
        await this.hubContext.Clients.All.SendAsync("ReceiveMessage", sender, message);
}
