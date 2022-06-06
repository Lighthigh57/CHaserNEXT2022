using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public int posX, posY;

    public GameManager gm;

    private readonly int[] _up    = {  0, -1 };
    private readonly int[] _down  = {  0,  1 };
    private readonly int[] _left  = { -1,  0 };
    private readonly int[] _right = {  1,  0 };

    public int[] around;

    private bool _motionFlag;

    public void Init(int setX, int setY)
    {
        posX = setX;
        posY = setY;
        //Debug.Log(name + ": x = " + posX + ", y = " + posY);
    }

    private void Start()
    {
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        transform.Find("default");
    }

    protected int[] GetReady()
    {
        var temp = gm.GetReady(posX, posY);
        WriteLog(temp, "GetReady");
        return temp;
    }

    protected void Walk(int[] dir) //dir = up: , left: , right: , down;
    {
        WriteLog(dir, "Walk");
        gm.Walk(posX, posY, dir);
        //transform.Translate(dir[0], 0, -dir[1]);
        StartCoroutine(WalkMotion(dir));
        posX += dir[0];
        posY += dir[1];
    }

    private IEnumerator WalkMotion(IReadOnlyList<int> dir)
    {
        while(_motionFlag) yield return null;
        transform.DOLocalMove(new Vector3(dir[0], 0, -dir[1]), 0.5f)
            .SetRelative(true);
    }

    protected void Put(int[] dir)
    {
        WriteLog(dir, "Put");
        gm.Put(posX, posY, dir);
    }

    protected int[] Look(int[] dir)
    {
        WriteLog(dir, "Look");
        var temp = gm.Look(posX, posY, dir);
        WriteLog(temp, "Look");
        return temp;
    }

    public int[] Search(int[] dir)
    {
        WriteLog(dir, "Search");
        var temp = gm.Search(posX, posY, dir);
        WriteLog(temp, "Search");
        return temp;
    }

    private void WriteLog(int[] array, string com)
    {
        var text = name + ": " + com + ": [ ";
        text = array.Aggregate(text, (current, i) => current + (i + " "));
        text += "]";

        Debug.Log(text);
    }

    private void OnTriggerEnter(Collider other)
    {
        //Destroy(other.gameObject);

        other.GetComponent<ItemController>().Point();

    }

    protected int[] SetDir(int dir)
    {
        switch (dir)
        {
            case 0:
                //mesh.eulerAngles = new Vector3(0, 0, 0);
                _motionFlag = true;
                transform.DORotate(new Vector3(0, 270), 0.5f)
                    .OnComplete(() => _motionFlag = false);
                return _up;
            case 1:
                //mesh.eulerAngles = new Vector3(0, 270, 0);
                _motionFlag = true;
                transform.DORotate(new Vector3(0,180),0.5f)
                    .OnComplete(() => _motionFlag = false);
                return _left;
            case 2:
                //mesh.eulerAngles = new Vector3(0, 90, 0);
                _motionFlag = true;
                transform.DORotate(new Vector3(0,0),0.5f)
                    .OnComplete(() => _motionFlag = false);
                return _right;
            case 3:
                //mesh.eulerAngles = new Vector3(0, 180, 0);
                _motionFlag = true;
                transform.DORotate(new Vector3(0, 90), 0.5f)
                    .OnComplete(() => _motionFlag = false);
                return _down;
            default:
                return null;
        }
    }
}
