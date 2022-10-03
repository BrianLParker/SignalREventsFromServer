using Microsoft.AspNetCore.SignalR;
using SignalREventsFromServer.Server.Brokers.HubContexts;

namespace SignalREventsFromServer.Server.Hubs;

public class ChatHub : Hub<IChatHubClient>
{
    private readonly IHubContextBroker hubContextBroker;

    public ChatHub(IHubContextBroker hubContextBroker) =>
        this.hubContextBroker = hubContextBroker;

    public async Task SendMessage(string user, string message) =>
        await hubContextBroker.SendMessageToAllClientsAsync(user, message);

    public async Task SendMessageToGroup(string groupName, string user, string message) =>
        await hubContextBroker.SendMessageToGroupAsync(groupName, user, message);

    public async Task JoinGroup(string groupName) =>
        await hubContextBroker.JoinGroupAsync(Context.ConnectionId, groupName);
}
