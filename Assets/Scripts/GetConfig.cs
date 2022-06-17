using System.Net;
using UnityEngine;

public class GetConfig : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // ホスト名を取得する
        var hostname = Dns.GetHostName();

        // ホスト名からIPアドレスを取得する
        IPAddress[] adrList = Dns.GetHostAddresses(hostname);
        foreach (IPAddress address in adrList)
        {
            Debug.Log(address.ToString());
        }
    }

    // Update is called once per frame
}
