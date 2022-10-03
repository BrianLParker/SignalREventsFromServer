namespace SignalREventsFromServer.Server.Services.Events;

public interface IEventsService
{
    ValueTask SubcribeToChannelAsync(string connectionId, string channel);
    ValueTask SendMessageToGroupAsync(string groupName, string sender, string message);
    ValueTask SendMessageToAllClientsAsync(string sender, string message);
    ValueTask NotifyControllerActionAsync(string controller, string action);
}
