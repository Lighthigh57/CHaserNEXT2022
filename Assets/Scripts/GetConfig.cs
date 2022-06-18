using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

public class GetConfig : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI hostConfig;

    private string hostIP, hostName;
    private SynchronizationContext _mainContext;

    // Start is called before the first frame update
    private void Start()
    {
        _mainContext = SynchronizationContext.Current;
        
        Task.Run(Get);
    }

    private void Get()
    {
        var host = Dns.GetHostEntry(Dns.GetHostName());
        foreach (var ip in host.AddressList)
        {
            
            if (ip.AddressFamily == AddressFamily.InterNetwork)
            {
                hostIP = ip.ToString();
                hostName = Dns.GetHostEntry(ip).HostName;
                break;
            }
        }
        _mainContext.Post(_ => hostConfig.text = $"ホストIPアドレス:{hostIP}\nホスト名:{hostName}", null);
    }
    
}
