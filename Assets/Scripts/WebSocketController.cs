using UnityEngine;
using System.Net;
using System.Collections;
using WebSocketSharp;
using WebSocketSharp.Server;


public class WebSocketController : MonoBehaviour
{
    private WebSocketServer server;
    // Start is called before the first frame update
    public void LaunchServer (int port)
    {
        if (server == null)
        {
            server = new WebSocketServer(port);

            server.AddWebSocketService<Echo>("/");
            server.Start();
        }
        else
        {
            server.Stop();
            server = null;
        }
        

    }

    void OnDestroy()
    {
        if (server != null)
        {
            server.Stop();
            server = null;
        }
    }
}

public class Echo : WebSocketBehavior
{
    protected override void OnMessage (MessageEventArgs e)
    {
        Debug.Log(e.Data);
        Sessions.Broadcast(e.Data);
    }
}