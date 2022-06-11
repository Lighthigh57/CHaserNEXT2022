using DG.Tweening;
using UnityEngine;
using UnityEngine.Serialization;

public class Menu : MonoBehaviour
{
    private RectTransform rtf;

    [FormerlySerializedAs("ScaleFrom")] [SerializeField] private int scaleFrom;
    [FormerlySerializedAs("ScaleTo")] [SerializeField] private int scaleTo;


    // Start is called before the first frame update
    private void Start()
    {
        rtf = GetComponent<RectTransform>();
    }

    // Update is called once per frame


    public void Enter()
    {
        rtf.DOSizeDelta(new Vector2(scaleTo,rtf.sizeDelta.y), 0.2f);
    }

    public void Exit()
    {
        rtf.DOSizeDelta(new Vector2(scaleFrom,rtf.sizeDelta.y), 0.2f);
    }
}
