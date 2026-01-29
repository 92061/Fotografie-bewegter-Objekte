using Microsoft.AspNetCore.SignalR;

namespace Project;

public class NotificationHub : Hub
{
    public async Task SendPicture(byte[] data) => await Clients.Others.SendAsync("picture", data);
}