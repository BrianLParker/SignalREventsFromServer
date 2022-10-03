namespace SignalREventsFromServer.Server.Brokers.HubContexts;

public interface IHubContextBroker
{
    ValueTask SendMessageToAllClients(string sender, string message);
}
