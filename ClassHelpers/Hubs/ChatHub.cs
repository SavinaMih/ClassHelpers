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
    }
}
