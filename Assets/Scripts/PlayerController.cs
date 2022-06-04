using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public int posX, posY;

    public GameManager gm;
    private Transform _mesh;

    private readonly int[] _up    = {  0, -1 };
    private readonly int[] _down  = {  0,  1 };
    private readonly int[] _left  = { -1,  0 };
    private readonly int[] _right = {  1,  0 };

    public int[] around;

    private bool _motionFlag;

    private readonly int _step = 8;

    public void Init(int setX, int setY)
    {
        posX = setX;
        posY = setY;
        //Debug.Log(name + ": x = " + posX + ", y = " + posY);
    }

    private void Start()
    {
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        _mesh = transform.Find("default");
    }

    public int[] GetReady()
    {
        var temp = gm.GetReady(posX, posY);
        WriteLog(temp, "GetReady");
        return temp;
    }

    public void Walk(int[] dir) //dir = up: , left: , right: , down;
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
        for(var i = 0; i < _step; i++)
        {
            transform.Translate(
                dir[0] / (float)_step,
                0,
                -(float)dir[1] / _step);
            yield return null;
        }
    }

    public void Put(int[] dir)
    {
        WriteLog(dir, "Put");
        gm.Put(posX, posY, dir);
    }

    public int[] Look(int[] dir)
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

    public void WriteLog(int[] array, string com)
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

    public int[] SetDir(int dir)
    {
        switch (dir)
        {
            case 0:
                //mesh.eulerAngles = new Vector3(0, 0, 0);
                _motionFlag = true;
                StartCoroutine(RotateMotion(0));
                return _up;
            case 1:
                //mesh.eulerAngles = new Vector3(0, 270, 0);
                _motionFlag = true;
                StartCoroutine(RotateMotion(270));
                return _left;
            case 2:
                //mesh.eulerAngles = new Vector3(0, 90, 0);
                _motionFlag = true;
                StartCoroutine(RotateMotion(90));
                return _right;
            case 3:
                //mesh.eulerAngles = new Vector3(0, 180, 0);
                _motionFlag = true;
                StartCoroutine(RotateMotion(180));
                return _down;
            default:
                return null;
        }
    }

    private IEnumerator RotateMotion(int targetDeg)
    {
        var nowDeg = _mesh.eulerAngles.y;

        if (Mathf.Abs(targetDeg - nowDeg) >= 270)
        {
            for (var i = 1; i <= _step; i++)
            {
                _mesh.eulerAngles = new Vector3(0, nowDeg + (nowDeg - targetDeg) / 3 / _step * i, 0);
                yield return null;
            }
        }
        else
        {
            for (var i = 1; i <= _step; i++)
            {
                _mesh.eulerAngles = new Vector3(0, nowDeg + (targetDeg - nowDeg) / _step * i, 0);
                yield return null;
            }
        }

        
        _motionFlag = false;
        yield return null;
    }
}
