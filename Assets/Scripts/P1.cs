using UnityEngine;

public class P1 : PlayerController
{
    public void Action1()
    {
        around = GetReady();
    }

    private int _state; // 0 normal; 1 item; 2 neerIem
    private int[] _look;
    private int _lookdir;

    public void Action2()
    {
        if (_state == 1)
        {
            _state = 0;
            if (_look[4] != 2)
            {
                Walk(SetDir(_lookdir));
                return;
            }
        }

        if (_state == 0)
        {
            for (int i = 0; i < 4; i++)
            {
                if (around[2 * i + 1] == 1)
                {
                    Put(SetDir(i));
                    return;
                }
            }

            for (int i = 0; i < 4; i++)
            {
                if (around[2 * i + 1] == 3)
                {
                    if (
                        !(
                            (i == 0 || i == 3) && (around[2 * i] == 2 && around[2 * i + 2] == 2) ||
                            (i == 1 || i == 2) && (around[2 * i - 2] == 2 && around[2 * i + 4] == 2)
                         )
                      )
                    {
                        Walk(SetDir(i));
                        return;
                    }
                }
            }
            for (int i = _lookdir; i < 4; i++)
            {
                if (around[2 * i + 1] == 3)
                {
                    if (
                        (i == 0 || i == 3) && (around[2 * i] == 2 && around[2 * i + 2] == 2) ||
                        (i == 1 || i == 2) && (around[2 * i - 2] == 2 && around[2 * i + 4] == 2)
                      )
                    {
                        _look = Look(SetDir(i));
                        _state = 1;
                        _lookdir = i;
                        return;
                    }
                }
            }

            int dir = Random.Range(0, 4);
            while (around[2 * dir + 1] != 0)
            {
                dir = Random.Range(0, 4);
            }
            Walk(SetDir(dir));
        }
    }

    //int state = 0; // 0 normal; 1 item; 2 neerIem
    //int[] look;
    //int lookdir;

    //public void Action2()
    //{
    //    if (state == 1)
    //    {
    //        state = 0;
    //        if (look[4] != 2)
    //        {
    //            Walk(SetDir(lookdir));
    //            return;
    //        }
    //    }

    //    if (state == 0)
    //    {
    //        for (int i = 0; i < 4; i++)
    //        {
    //            if (around[2 * i + 1] == 1)
    //            {
    //                Put(SetDir(i));
    //                return;
    //            }
    //        }

    //        for (int i = 0; i < 4; i++)
    //        {
    //            if (around[2 * i + 1] == 3)
    //            {
    //                if (
    //                    !(
    //                        (i == 0 || i == 3) && (around[2 * i] == 2 && around[2 * i + 2] == 2) ||
    //                        (i == 1 || i == 2) && (around[2 * i - 2] == 2 && around[2 * i + 4] == 2)
    //                     )
    //                  )
    //                {
    //                    Walk(SetDir(i));
    //                    return;
    //                }
    //            }
    //        }
    //        for (int i = lookdir; i < 4; i++)
    //        {
    //            if (around[2 * i + 1] == 3)
    //            {
    //                if (
    //                    (i == 0 || i == 3) && (around[2 * i] == 2 && around[2 * i + 2] == 2) ||
    //                    (i == 1 || i == 2) && (around[2 * i - 2] == 2 && around[2 * i + 4] == 2)
    //                  )
    //                {
    //                    look = Look(SetDir(i));
    //                    state = 1;
    //                    lookdir = i;
    //                    return;
    //                }
    //            }
    //        }

    //        int dir = Random.Range(0, 4);
    //        while (around[2 * dir + 1] != 0)
    //        {
    //            dir = Random.Range(0, 4);
    //        }
    //        Walk(SetDir(dir));
    //        return;

    //    }
    //}

}
