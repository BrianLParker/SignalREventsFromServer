namespace SignalREventsFromServer.Server.Hubs;

public interface IChatHubClient
{
    Task ReceiveMessage(string user, string message);
}
