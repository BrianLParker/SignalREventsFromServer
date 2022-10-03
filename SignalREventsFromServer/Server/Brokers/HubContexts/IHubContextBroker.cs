using Microsoft.AspNetCore.SignalR;

namespace SignalREventsFromServer.Server.Brokers.HubContexts;

public interface IHubContextBroker
{
    ValueTask JoinGroupAsync(string connectionId, string groupName);
    ValueTask SendMessageToGroupAsync(string groupName, string sender, string message);
    ValueTask SendMessageToAllClientsAsync(string sender, string message);
}
