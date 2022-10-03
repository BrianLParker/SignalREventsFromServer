using Microsoft.AspNetCore.SignalR;

namespace SignalREventsFromServer.Server.Hubs;

public class ChatHub : Hub<IChatHubClient>
{
    public async Task SendMessage(string user, string message)
    {
        await Clients.All.ReceiveMessage(user, message);
    }
}
