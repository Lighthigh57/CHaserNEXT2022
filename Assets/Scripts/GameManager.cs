using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class GameManager : MonoBehaviour
{
    private const int Width = 15;
    private const int Height = 17;
    private int[,] _map = new int[Height, Width]; //0:empty, 1:enemy, 2:block, 3: item

    private readonly GameObject[,] _floorBase = new GameObject[Height, Width];
    private GameObject _frame;

    private bool isPlaying = true;

    //P1 player1;
    private ScriptShun _player1;

    //P2 player2;
    private ScriptShun _player2;

    private int _playerNum = 1;
    internal string PlayerName { get; private set; }
    
    private UIManager uiInfo;

    [FormerlySerializedAs("UIManager")] [SerializeField] private GameObject uiManager;
    [SerializeField] internal int step;
    [SerializeField] internal int turnNum = 100;
    [SerializeField] private float delay = 1f;

    private void Start()
    {
        uiInfo = uiManager.GetComponent<UIManager>();
        uiInfo.SetTurn(turnNum);
        MapInit();
        _frame = GameObject.Find("Board");
        var fBase = (GameObject)Resources.Load("FloorBase");

        for (var i = 0; i < Height; i++)
        {
            for (var j = 0; j < Width; j++)
            {
                var temp = Instantiate(
                                        fBase,
                                        new Vector3(j, -2, Height - 1 - i),
                                        Quaternion.identity
                                        );
                temp.transform.SetParent(_frame.transform);
                _floorBase[i, j] = temp;
            }
        }
    }

    private void MapInit()
    {
        //for (int i = 0; i < HEIGHT; i++)
        //{
        //    for (int j = 0; j < WIDTH; j++)
        //    {
        //        if (i == 0 || i == HEIGHT - 1 || j == 0 || j == WIDTH - 1)
        //        {
        //            map[i, j] = 2;
        //        }
        //        else
        //        {
        //            map[i, j] = 0;
        //        }

        //        if (i == 8 && (j == 0 || j == 14))
        //        {
        //            map[i, j] = 1;
        //        }
        //    }
        //}

        _map = new[,]{
            {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
            {0,3,0,3,0,3,0,3,0,3,0,3,0,3,0},
            {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
            {0,3,0,0,3,0,0,0,0,3,3,3,0,3,0},
            {0,2,0,3,3,3,0,2,0,0,3,0,0,2,0},
            {0,3,0,0,0,0,0,3,0,0,0,0,0,3,0},
            {0,0,0,3,0,0,3,3,0,2,0,0,0,0,0},
            {0,3,0,3,0,0,0,0,0,3,3,0,0,3,0},
            {0,0,1,0,0,3,0,3,0,3,0,0,1,0,0},
            {0,3,0,0,3,3,0,0,0,0,0,3,0,3,0},
            {0,0,0,0,0,2,0,3,3,0,0,3,0,0,0},
            {0,3,0,0,0,0,0,3,0,0,0,0,0,3,0},
            {0,2,0,0,3,0,0,2,0,3,3,3,0,2,0},
            {0,3,0,3,3,3,0,0,0,0,3,0,0,3,0},
            {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
            {0,3,0,3,0,3,0,3,0,3,0,3,0,3,0},
            {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0}
        };

        
    }

    public int[] GetReady(int x, int y)
    {
        if (Judge(x, y))
        {
            FinishGame(PlayerName + "is lose...");
        }

        var result = new int[9];

        for (var i = 0; i < 3; i++)
        {
            for (var j = 0; j < 3; j++)
            {
                if (CheckArea(x - 1 + j, y - 1 + i))
                    result[j + 3 * i] = 2;
                else if (i == 1 && j == 1)
                    result[j + 3 * i] = 0;
                else
                    result[j + 3 * i] = _map[y - 1 + i, x - 1 + j];
            }
        }

        return result;
    }

    public void Walk(int x, int y, int[] dir) //dir = up: , left: , right: , down;
    {       
        if (CheckArea(x + dir[0], y + dir[1]) || _map[y + dir[1], x + dir[0]] == 2)
        {
            // ����
            FinishGame(PlayerName + "is lose...");
        }
        else
        {
            if (_map[y + dir[1], x + dir[0]] == 3)
            {
                _map[y, x] = 2;
                StartCoroutine(DestroyWall(_floorBase[y, x]));
                if (Judge(x + dir[0], y + dir[1]))
                {
                    FinishGame(PlayerName + "is lose...");
                }
            }
            else
            {
                _map[y, x] = 0;
            }
            _map[y + dir[1], x + dir[0]] = 1;
        }
    }

    private IEnumerator DestroyWall(GameObject wall)
    {
        yield return new WaitForSeconds(0.5f);
        Destroy(wall);
    }

    public void Put(int x, int y, int[] dir)
    {
        if (!CheckArea(x + dir[0], y + dir[1]))
        {
            if (_map[y + dir[1], x + dir[0]] == 1)
            {
                //����
                FinishGame(PlayerName + "is win!!!");
            }

            _map[y + dir[1], x + dir[0]] = 2;
            Destroy(_floorBase[y + dir[1], x + dir[0]]);
            // ���Ŕ���
            if (Judge(x, y))
            {
                FinishGame(PlayerName + "is lose...");
            }
        }
    }

    public int[] Look(int x, int y, int[] dir)
    {
        var result = new int[9];
        int targetX = x + 2 * dir[1], targetY = y + 2 * dir[0];

        for (var i = 0; i < 3; i++)
        {
            for (var j = 0; j < 3; j++)
            {
                if (CheckArea(targetX - 1 + j, targetY - 1 + i))
                {
                    result[j + 3 * i] = 2;
                }
                else
                {
                    result[j + 3 * i] = _map[targetY - 1 + i, targetX - 1 + j];
                }
            }
        }

        return result;
    }

    public int[] Search(int x, int y, int[] dir)
    {
        var result = new int[9];

        for (var i = 1; i <= 9; i++)
        {
            if (CheckArea(x + dir[0] * i, y + dir[1] * i))
            {
                result[i - 1] = 2;
            }
            else
            {
                result[i - 1] = _map[y + dir[1] * i, x + dir[0] * i];
            }
        }

        return result;
    }

    private bool CheckArea(int x, int y) // ��O����
    {
        return x is < 0 or > Width - 1 || y is < 0 or > Height - 1;
    }

    private bool Judge(int x, int y) // 4���̕ǔ���
    {
        // ans 1

        //bool result = true;
        //for (int i = 0; i < 4; i++)
        //{
        //    result = result
        //        && CheckArea(x + (int)Mathf.Cos(Mathf.PI / 4 * i), y + (int)Mathf.Sin(Mathf.PI / 4 * i))
        //        || map[y + (int)Mathf.Sin(Mathf.PI / 4 * i), x + (int)Mathf.Cos(Mathf.PI / 4 * i)] == 2;
        //}
        //return result;

        // ans 2

        //bool result = (CheckArea(x + 0, y + 1) || map[y + 1, x + 0] == 2)
        //    && (CheckArea(x - 1, y + 0) || map[y + 0, x - 1] == 2)
        //    && (CheckArea(x + 1, y + 0) || map[y + 0, x + 1] == 2)
        //    && (CheckArea(x + 0, y - 1) || map[y - 1, x + 0] == 2);

        // ans 3

        var result = (CheckArea(x + 1, y + 0) || _map[y + 0, x + 1] == 2);
        result = result && (CheckArea(x - 1, y + 0) || _map[y + 0, x - 1] == 2);
        result = result && (CheckArea(x + 0, y + 1) || _map[y + 1, x + 0] == 2);
        result = result && (CheckArea(x + 0, y - 1) || _map[y - 1, x + 0] == 2);

        return result;
    }

    private void FinishGame(string message)
    {
        _finishDelay = true;
        Debug.Log(message);
        StartCoroutine(nameof(DelayFinish));
    }

    private IEnumerator DelayFinish()
    {
        isPlaying = false;
        yield return new WaitForSeconds(2);
        SceneManager.sceneLoaded += GameSceneLoaded;

        SceneManager.LoadScene("Result");
    }

    private static void GameSceneLoaded(Scene next, LoadSceneMode mode)
    {
        //var gameManager = GameObject.Find("").GetComponent<>();

        //gameManager.setMap(map);

        SceneManager.sceneLoaded -= GameSceneLoaded;
    }

    private void Update()
    {
        if (step == 0)
        {
            StartCoroutine(nameof(Opening));
            step = 1;
        }
        if (step == 2)
        {
            StartCoroutine(nameof(Game));
            step = 3;
        }
        if (step == 4)
        {
            var pt1 = GameObject.Find("Pt_1").GetComponent<Counter>().GetPt();
            var pt2 = GameObject.Find("Pt_2").GetComponent<Counter>().GetPt();
            if (pt1 > pt2)
            {
                FinishGame("Player_1 is win!!!");
            }
            if (pt1 < pt2)
            {
                FinishGame("Player_2 is win!!!");
            }
            if (pt1 == pt2)
            {
                FinishGame("---Draw---");
            }
        }
    }

    private IEnumerator Opening()
    {
        var item = (GameObject)Resources.Load("Item");

        for (var i = 0; i < Height; i++)
        {
            StartCoroutine(nameof(RowCreate), i);
            yield return new WaitForSeconds(0.1f);
        }
        yield return new WaitForSeconds(3);

        for (var i = 0; i < Height; i++)
        {
            for (var j = 0; j < Width; j++)
            {
                if (_map[i, j] == 2)
                {
                    Destroy(_floorBase[i, j]);
                }
            }
        }
        for (var i = 0; i < Height; i++)
        {

            for (var j = 0; j < Width; j++)
            {
                switch (_map[i, j])
                {
                    case 1:
                        var temp = Instantiate(
                            (GameObject)Resources.Load("Player_" + _playerNum),
                            new Vector3(j, 10, Height - 1 - i),
                            Quaternion.identity
                            );
                        temp.name = "Player_" + _playerNum;

                        if (_playerNum == 1)
                        {
                            temp.transform.Find("default").rotation = Quaternion.LookRotation(Vector3.right);
                            _player1 = temp.GetComponent<ScriptShun>();
                            _player1.Init(j, i);

                            _playerNum++;
                        }
                        else
                        {
                            temp.transform.Find("default").rotation = Quaternion.LookRotation(Vector3.left);
                            _player2 = temp.GetComponent<ScriptShun>();
                            _player2.Init(j, i);
                        }
                        yield return null;
                        break;
                    case 3:
                        Instantiate(
                            item,
                            new Vector3(j, 10, Height - 1 - i),
                            Quaternion.identity
                            );
                        yield return null;
                        break;
                }
            }
        }
        yield return new WaitForSeconds(2);
        step = 2;
    }

    private IEnumerator RowCreate(int i)
    {
        var floor = (GameObject)Resources.Load("floor");
        for (var j = 0; j < Width; j++)
        {
            var temp = Instantiate(
                                    floor,
                                    new Vector3(j, 10, Height - 1 - i),
                                    Quaternion.identity
                                    );
            if (temp != null) DontDestroyOnLoad(temp);
            yield return new WaitForSeconds(0.1f);
        }
    }

    private bool _finishDelay;

    private IEnumerator Game()
    {
        for (var j = 0; j < turnNum; j++)
        {
            Debug.Log("Turn: " + j);
            uiInfo.SetTurn(turnNum - j);
            _player1.Action1();
            //yield return new WaitForSeconds(delay);

            PlayerName = _player1.name;

            _player1.Action2();
            yield return new WaitForSeconds(delay);

            
            if (_finishDelay)
                yield return new WaitForSeconds(3);
            if (!isPlaying)
                yield break;

            _player2.Action1();
            //yield return new WaitForSeconds(delay);

            PlayerName = _player2.name;

            _player2.Action2();
            yield return new WaitForSeconds(delay);

            
            if (_finishDelay)
                yield return new WaitForSeconds(3);
            if (!isPlaying)
                yield break;

        }
        yield return new WaitForSeconds(1);
        step = 4;
    }

}