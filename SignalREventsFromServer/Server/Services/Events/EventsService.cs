using SignalREventsFromServer.Server.Brokers.HubContexts;

namespace SignalREventsFromServer.Server.Services.Events;

public class EventsService : IEventsService
{
    private readonly IHubContextBroker hubContextBroker;

    public EventsService(IHubContextBroker hubContextBroker) => this.hubContextBroker = hubContextBroker;

    public async ValueTask SubcribeToChannelAsync(string connectionId, string channel) =>
        await this.hubContextBroker.JoinGroupAsync(connectionId, channel);

    public async ValueTask SendMessageToAllClientsAsync(string sender, string message) =>
        await this.hubContextBroker.SendMessageToAllClientsAsync(sender, message);

    public async ValueTask SendMessageToGroupAsync(string groupName, string sender, string message) =>
        await this.hubContextBroker.SendMessageToGroupAsync(groupName, sender, message);

    public async ValueTask NotifyControllerActionAsync(string controller, string action)
    {
        var groupName = controller;
        var sender = $"Controller [{controller}]";
        var message = "Action [Get]";

        await this.hubContextBroker.SendMessageToGroupAsync(groupName, sender, message);
    }
}
