using Microsoft.AspNetCore.SignalR;

namespace SignalREventsFromServer.Server.Brokers.Identity;

public class IdentityBroker : IIdentityBroker
{
    private readonly HubCallerContext hubCallerContext;

    public IdentityBroker(HubCallerContext hubCallerContext)
    {
        this.hubCallerContext = hubCallerContext;
    }

    public string ConnectionId => hubCallerContext.ConnectionId;
}
