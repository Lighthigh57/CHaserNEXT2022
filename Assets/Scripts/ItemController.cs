using UnityEngine;

public class ItemController : MonoBehaviour
{
    private GameManager _gm;

    private void Start()
    {
        DontDestroyOnLoad(gameObject);

        _gm = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    private bool _trigger = true;
    private bool _ptFlag = true;

    // ReSharper disable Unity.PerformanceAnalysis
    private void Update()
    {
        if (_trigger && _gm.step >= 2)
        {
            GetComponent<Rigidbody>().useGravity = false;
            GetComponent<Collider>().isTrigger = true;
            _trigger = false;
        }
    }

    public void Point()
    {
        if (_ptFlag)
        {
            _ptFlag = false;
            GetComponent<Rigidbody>().useGravity = true;
            GetComponent<Collider>().isTrigger = false;

            // ‰¹’Ç‰Á


            if (_gm.GetPlayerName() == "Player_1")
            {
                transform.position = new Vector3(-5, 10, 8);
                GameObject.Find("Pt_1").GetComponent<Counter>().Point();
            }
            else
            {
                transform.position = new Vector3(19, 10, 8);
                GameObject.Find("Pt_2").GetComponent<Counter>().Point();
            }
        }    
    }
}
