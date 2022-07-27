using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;

namespace ClassHelpers.Hubs
{
    public class ChatHub : Hub<IChatClient>
    {
        private readonly UserManager<IdentityUser> userManager;

        public ChatHub(UserManager<IdentityUser> userManager)
        {
            this.userManager = userManager;
        }

        public async Task SendPublicMessage(string sender, string message)
        {
            await Clients.All.PublicMessage(sender, message);
        }

        public async Task SendPrivateMessage(string sender, string recipient, string message)
        {
            await Clients.User(userManager.FindByNameAsync(recipient).Result.Id).PrivateMessage(sender, message);
        }

        public async Task SendPrivateFileMessage(string sender, string recipient, string filename, string base64)
        {
            await Clients.User(userManager.FindByNameAsync(recipient).Result.Id).PrivateFileMessage(sender, filename, base64);
        }

        public async Task SendPublicFileMessage(string sender, string filename, string base64)
        {
            await Clients.All.PublicFileMessage(sender, filename, base64);
        }

        public async Task JoinChatroom(string user)
        {
            await Clients.All.UserJoinedChatroom(user);
        }

        public async Task LeaveChatroom(string user)
        {
            await Clients.All.UserLeftChatroom(user);
        }
    }
}
