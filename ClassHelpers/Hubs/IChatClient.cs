namespace ClassHelpers.Hubs
{
    public interface IChatClient
    {
        Task PublicMessage(string sender, string message);
    }
}
