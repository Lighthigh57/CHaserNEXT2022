using UnityEngine;

public class Counter : MonoBehaviour
{
    private int _pt;
    private TextMesh _tx;

    private void Start()
    {
        DontDestroyOnLoad(this.gameObject);
        _tx = GetComponent<TextMesh>();
    }

    public void Point()
    {
        _pt++;
        _tx.text = _pt.ToString();
    }

    public int GetPt()
    {
        return _pt;
    }
}
