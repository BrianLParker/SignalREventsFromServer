using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.SignalR.Client;

namespace SignalREventsFromServer.Client.Pages;

public partial class Index : ComponentBase, IChatHubClient
{
    private HubConnection? hubConnection;
    private List<string> messages = new List<string>();
    private string? userInput;
    private string? messageInput;
    private bool monitorDisabled = false;

    protected override async Task OnInitializedAsync()
    {
        hubConnection = new HubConnectionBuilder().WithUrl(Navigation.ToAbsoluteUri(relativeUri: "/chathub")).Build();
        hubConnection.On<string, string>(methodName: "ReceiveMessage", ReceiveMessage);
        await hubConnection.StartAsync();
    }

    public Task ReceiveMessage(string user, string message)
    {
        var encodedMsg = $"{user}: {message}";
        messages.Add(encodedMsg);
        StateHasChanged();
        return Task.CompletedTask;
    }

    private async Task Send()
    {
        if (hubConnection is not null)
        {
            await hubConnection.SendAsync("SendMessage", userInput, messageInput);
        }
    }

    private async Task MonitorApiClicked()
    {
        if (hubConnection is not null)
        {
            await hubConnection.SendAsync("JoinGroup", "APIMonitor");
            monitorDisabled = true;
        }
    }

    public bool IsConnected => hubConnection?.State == HubConnectionState.Connected;
    public async ValueTask DisposeAsync()
    {
        if (hubConnection is not null)
        {
            await hubConnection.DisposeAsync();
        }
    }

}
