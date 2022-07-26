namespace ClassHelpers.Hubs
{
    public interface IChatClient
    {
        Task PublicMessage(string sender, string message);
        Task PrivateMessage(string sender, string message);
        Task PrivateFileMessage(string sender, string filename, string base64);
        Task PublicFileMessage(string sender, string filename, string base64);
        Task UserJoinedChatroom(string user);
        Task UserLeftChatroom(string user);
    }
}
