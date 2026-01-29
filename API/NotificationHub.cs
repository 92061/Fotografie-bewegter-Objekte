using System.Device.Gpio;
using Microsoft.AspNetCore.SignalR;

namespace Project;

public class NotificationHub : Hub
{
    public async Task SendPicture(byte[] data) => await Clients.Others.SendAsync("picture", data);
    public async Task SendTriggered(PinEventTypes data) => await Clients.Others.SendAsync("trigger", data);
}