using Microsoft.AspNetCore.SignalR;
using SignalREventsFromServer.Server.Brokers.Identity;
using SignalREventsFromServer.Server.Services.Events;

namespace SignalREventsFromServer.Server.Hubs;

public class ChatHub : Hub<IChatHubClient>
{
    private readonly IEventsService eventsService;

    public ChatHub(IEventsService eventsService) =>
        this.eventsService = eventsService;

    public async Task SendMessage(string user, string message) =>
        await eventsService.SendMessageToAllClientsAsync(user, message);

    public async Task SendMessageToGroup(string groupName, string user, string message) =>
        await eventsService.SendMessageToGroupAsync(groupName, user, message);

    public async Task JoinGroup(string groupName) =>
        await eventsService.SubcribeToChannelAsync(IdentityBroker.ConnectionId, groupName);

    public IIdentityBroker IdentityBroker => new IdentityBroker(this.Context);
}
