using Microsoft.AspNetCore.SignalR;

namespace ClassHelpers.Hubs
{
    public class ChatHub : Hub<IChatClient>
    {
        public async Task SendPublicMessage(string sender, string message)
        {
            await Clients.All.PublicMessage(sender, message);
        }
    }
}
