using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using SFB;

public class ServerSetter : MonoBehaviour
{
    [FormerlySerializedAs("SettingText")] [SerializeField] private TMP_InputField[] settingText;

    [FormerlySerializedAs("Maximum")] [SerializeField] private Toggle maximum;
    // Start is called before the first frame update

    private void Start()
    {
        if (PlayerPrefs.HasKey("Path"))
        {
            settingText[0].text = PlayerPrefs.GetString("Path");
        }
        if (PlayerPrefs.HasKey("Time"))
        {
            settingText[1].text = PlayerPrefs.GetString("Time");
        }
        if (PlayerPrefs.HasKey("Speed"))
        {
            settingText[2].text = PlayerPrefs.GetString("Speed");
        }

        if (PlayerPrefs.HasKey("Maximum"))
        {
            maximum.isOn = PlayerPrefs.GetInt("Maximum") == 1;
        }
    }

    public void ChangePath()
    {
        StandaloneFileBrowser.OpenFolderPanelAsync("Choose a folder", "", false, (string[] path) =>
        {
            if (path.Length > 0)
            {
                settingText[0].text = path[0];
            }   
        });
    }

    private void OnDestroy()
    {
        if (maximum.isOn)
        {
            PlayerPrefs.SetInt("Maximum", 1);
        }
        else
        {
            PlayerPrefs.SetInt("Maximum", 0);
        }
        PlayerPrefs.SetString("Path", settingText[0].text);
        if (int.TryParse(settingText[1].text, out var time) && int.TryParse(settingText[2].text, out var score))
        {
            PlayerPrefs.SetInt("Time", time);
            PlayerPrefs.SetInt("Score", score);
        }
        else
        {
            PlayerPrefs.SetInt("Time", 0);
            PlayerPrefs.SetInt("Score", 0);
        }
        {
            
        }
    }
}