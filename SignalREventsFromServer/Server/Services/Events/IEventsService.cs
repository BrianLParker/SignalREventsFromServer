namespace SignalREventsFromServer.Server.Services.Events;

public interface IEventsService
{
    ValueTask JoinGroupAsync(string connectionId, string groupName);
    ValueTask SendMessageToGroupAsync(string groupName, string sender, string message);
    ValueTask SendMessageToAllClientsAsync(string sender, string message);
    ValueTask NotifyControllerActionAsync(string controller, string action);
}
