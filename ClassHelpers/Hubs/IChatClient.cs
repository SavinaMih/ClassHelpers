﻿namespace ClassHelpers.Hubs
{
    public interface IChatClient
    {
        Task PublicMessage(string sender, string message);
        Task PrivateMessage(string sender, string message);
        Task PrivateFileMessage(string sender, string filename, string base64);
    }
}
