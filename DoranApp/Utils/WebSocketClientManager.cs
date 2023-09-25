using System;
using System.Collections.Generic;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace DoranApp.Utils;

public class WebSocketClientManager
{
    private CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
    private ClientWebSocket clientWebSocket = new ClientWebSocket();
    private Dictionary<string, Action<dynamic>> eventHandlers = new Dictionary<string, Action<dynamic>>();
    private Uri uri = new Uri("wss://localhost:44376/api/ws");

    public WebSocketClientManager()
    {
        ConnectAsync();
    }

    private async Task ConnectAsync()
    {
        await clientWebSocket.ConnectAsync(uri, cancellationTokenSource.Token);
        if (clientWebSocket.State == WebSocketState.Open)
        {
            await StartListening();
        }
    }

    public async Task StartListening()
    {
        try
        {
            while (clientWebSocket.State == WebSocketState.Open)
            {
                ArraySegment<byte> buffer = new ArraySegment<byte>(new byte[1024]);
                WebSocketReceiveResult result =
                    await clientWebSocket.ReceiveAsync(buffer, cancellationTokenSource.Token);

                if (result.MessageType == WebSocketMessageType.Text)
                {
                    string message = Encoding.UTF8.GetString(buffer.Array, 0, result.Count);
                    HandleMessage(message);
                }
            }
        }
        catch (Exception ex)
        {
            ConsoleDump.Extensions.Dump(ex);
        }
    }

    public async Task SendAsync(string message)
    {
        ArraySegment<byte> buffer = new ArraySegment<byte>(Encoding.UTF8.GetBytes(message));
        await clientWebSocket.SendAsync(buffer, WebSocketMessageType.Text, true, cancellationTokenSource.Token);
    }

    public void Subscribe(string eventName, Action<dynamic> handler)
    {
        eventHandlers[eventName] = handler;
    }

    private void HandleMessage(string message)
    {
        foreach (var eventName in eventHandlers.Keys)
        {
            dynamic eventData = ExtractEventData(message, eventName);
            if (eventData != null)
            {
                eventHandlers[eventName](eventData);
            }
        }
    }

    private dynamic ExtractEventData(string message, string eventName)
    {
        if (message == null)
        {
            return null;
        }

        try
        {
            var data = JObject.Parse(message);

            ConsoleDump.Extensions.Dump(data);
            if (data == null)
            {
                return null;
            }

            if ((string)data["EventName"] == eventName)
            {
                return data["Data"];
            }

            return null;
        }
        catch (Exception ex)
        {
            ConsoleDump.Extensions.Dump(ex);
            return null;
        }
    }
}