﻿using SignalREventsFromServer.Server.Brokers.HubContexts;

namespace SignalREventsFromServer.Server.Services.Events;

public class EventsService : IEventsService
{
    private readonly IHubContextBroker hubContextBroker;

    public EventsService(IHubContextBroker hubContextBroker) => this.hubContextBroker = hubContextBroker;

    public async ValueTask JoinGroupAsync(string connectionId, string groupName) =>
        await this.hubContextBroker.JoinGroupAsync(connectionId, groupName);
    public async ValueTask SendMessageToAllClientsAsync(string sender, string message) =>
        await this.hubContextBroker.SendMessageToAllClientsAsync(sender, message);

    public async ValueTask SendMessageToGroupAsync(string groupName, string sender, string message) =>
        await this.hubContextBroker.SendMessageToGroupAsync(groupName, sender, message);

    public async ValueTask NotifyGetRequestAsync(string controller)
    {
        var groupName = "APIMonitor";
        var sender = $"{controller} Controller";
        var message = "Get";

        await this.hubContextBroker.SendMessageToGroupAsync(groupName, sender, message);
    }
}