using Microsoft.AspNetCore.SignalR;
using SignalREventsFromServer.Server.Hubs;

namespace SignalREventsFromServer.Server.Brokers.HubContexts;

public class HubContextBroker : IHubContextBroker
{
    private readonly IHubContext<ChatHub> hubContext;

    public HubContextBroker(IHubContext<ChatHub> hubContext) =>
        this.hubContext = hubContext;

    public async ValueTask SendMessageToAllClients(string sender, string message) =>
        await this.hubContext.Clients.All.SendAsync .ReceiveMessage(sender, message);
}
