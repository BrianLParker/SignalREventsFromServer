using Microsoft.AspNetCore.SignalR;
using SignalREventsFromServer.Server.Hubs;

namespace SignalREventsFromServer.Server.Brokers.HubContexts;

public class HubContextBroker : IHubContextBroker
{
    private readonly IHubContext<ChatHub, IChatHubClient> hubContext;

    public HubContextBroker(IHubContext<ChatHub, IChatHubClient> hubContext) =>
        this.hubContext = hubContext;

    public async ValueTask JoinGroupAsync(string connectionId, string groupName) =>
        await this.hubContext.Groups.AddToGroupAsync(connectionId, groupName);

    public async ValueTask SendMessageToAllClientsAsync(string sender, string message)
    {
        IChatHubClient client = this.hubContext.Clients.All;
        await client.ReceiveMessage(sender, message);
    }

    public async ValueTask SendMessageToGroupAsync(string groupName, string sender, string message)
    {
        IChatHubClient client = this.hubContext.Clients.Group(groupName);
        await client.ReceiveMessage(sender, message);
    }
}
