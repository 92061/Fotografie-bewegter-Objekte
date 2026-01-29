using System.Net.WebSockets;
using Microsoft.AspNetCore.Mvc;

namespace Project.Controllers;

[ApiController]
public class WebsocketController : ControllerBase
{
    /// <summary>
    /// Websocket that images get published to
    /// </summary>
    [HttpGet("/ws")]
    public async Task Websocket()
    {
        if (HttpContext.WebSockets.IsWebSocketRequest)
        {
            WebSocket webSocket = await HttpContext.WebSockets.AcceptWebSocketAsync();
            ImageWebsocketHandler.AddSocket(webSocket);
        }
        else
        {
            HttpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
        }
    }
}