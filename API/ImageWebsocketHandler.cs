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
        Camera.PictureTaken += stream =>
        {
            byte[] buffer = new byte[stream.Length];
            stream.ReadExactly(buffer);
            foreach (WebSocket webSocket in Sockets)
            {
                if (webSocket.CloseStatus is null)
                    webSocket.SendAsync(buffer, WebSocketMessageType.Binary, WebSocketMessageFlags.None, CancellationToken.None);
            }
        };
    }
}