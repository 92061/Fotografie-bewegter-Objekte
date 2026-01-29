using System.Net.WebSockets;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using PhotographyOfMovingObjects;

namespace Project.Controllers;

[ApiController]
[Route("[controller]")]
public class ActionController : ControllerBase
{
    /// <summary>
    /// Gets the latest captured Image
    /// </summary>
    /// <response code="200">Latest photo</response>
    [HttpPost("LatestPhoto")]
    [ProducesResponseType<FileContentHttpResult>(StatusCodes.Status200OK)]
    public FileContentHttpResult LatestImage()
    {
        return TypedResults.File(Photography.LatestPicture, "image/png");
    }

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