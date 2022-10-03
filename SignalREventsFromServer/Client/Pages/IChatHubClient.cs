namespace SignalREventsFromServer.Client.Pages;

public interface IChatHubClient
{
    Task ReceiveMessage(string user, string message);
}
