using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI coolName, hotName, coolItem, hotItem, turnRemain;
    [SerializeField] private Slider turnVisual;

    private int maxTurn;

    // Start is called before the first frame update
    private void Start()
    {
        maxTurn = GameObject.Find("GameManager").GetComponent<GameManager>().turnNum;
        Debug.Log($"MaxTurn:{maxTurn}");
    }

    // Update is called once per frame

    public void SetTurn(int turnNum)
    {
        turnRemain.text = $"Turn:{turnNum}";
        turnVisual.value = turnNum * 1.0f / maxTurn;
    }
}
