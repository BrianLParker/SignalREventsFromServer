using Microsoft.AspNetCore.SignalR;
using SignalREventsFromServer.Server.Hubs;

namespace SignalREventsFromServer.Server.Brokers.HubContexts;

public class HubContextBroker : IHubContextBroker
{
    private readonly IHubContext<ChatHub, IChatHubClient> hubContext;

    public HubContextBroker(IHubContext<ChatHub, IChatHubClient> hubContext) =>
        this.hubContext = hubContext;

    public async ValueTask SendMessageToAllClients(string sender, string message) =>
        await this.hubContext.Clients.All.ReceiveMessage(sender, message);
}
