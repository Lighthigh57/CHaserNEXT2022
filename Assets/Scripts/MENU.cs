using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using DG.Tweening;
using UnityEngine;

public class MENU : MonoBehaviour
{
    private RectTransform rtf;

    [SerializeField] private int ScaleFrom;
    [SerializeField] private int ScaleTo;
    // Start is called before the first frame update
    void Start()
    {
        rtf = GetComponent<RectTransform>();
    }

    // Update is called once per frame


    public void Enter()
    {
        rtf.DOSizeDelta(new Vector2(ScaleTo,rtf.sizeDelta.y), 0.2f);
    }

    public void Exit()
    {
        rtf.DOSizeDelta(new Vector2(ScaleFrom,rtf.sizeDelta.y), 0.2f);
    }
}
