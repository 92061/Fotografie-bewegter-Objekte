using System.Collections.Concurrent;
using System.Net.WebSockets;
using PhotographyOfMovingObjects;

namespace Project;

public static class ImageWebsocketHandler
{
    private static readonly ConcurrentBag<WebSocket> Sockets = new();

    public static void AddSocket(WebSocket socket) => Sockets.Add(socket);

    static ImageWebsocketHandler()
    {
        Camera.PictureTaken += data =>
        {
            foreach (WebSocket webSocket in Sockets)
            {
                if (webSocket.CloseStatus is null)
                    webSocket.SendAsync(data, WebSocketMessageType.Binary, WebSocketMessageFlags.None, CancellationToken.None);
            }
        };
    }
}