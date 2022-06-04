using System.Collections.Generic;
using UnityEngine;

public class ScriptShun : PlayerController
{
    private int[] priority;
    private int x, y;
    private int last;

    internal void Action1()
    {
        priority = new int[9];
        around = GetReady();
    }
    internal void Action2()
    {
        for (int i = 0; i < 9; i++)
        {
            if (around[i] == 3)
            {
                if (i % 2 == 1)
                    priority[i]++;
                else
                    SolveDiagonal(i, "get");
            }
        }
        for (int i = 0; i < 9; i++)
        {
            if (around[i] == 1)
            {
                if (i % 2 == 0)
                    SolveDiagonal(i, "avoid");
                else
                {
                    Put(ConvertDirection(i));
                    break;
                }
            }
            if (around[i] == 2)
                priority[i] = -2;
        }

        int max = priority[1];
        List<int> maxIndexs = new List<int> { 1 };

        for (int i = 3; i < 9; i += 2)
        {
            if (max < priority[i])
            {
                max = priority[i];
                maxIndexs = new List<int> { i };
            }
            else if (max == priority[i])
                maxIndexs.Add(i);
        }

        if (maxIndexs.Count != 1)
        {
            if (last == 1 && maxIndexs.Contains(7))
                maxIndexs.Remove(7);
            else if (last == 3 && maxIndexs.Contains(5))
                maxIndexs.Remove(5);
            else if (last == 5 && maxIndexs.Contains(3))
                maxIndexs.Remove(3);
            else if (last == 7 && maxIndexs.Contains(1))
                maxIndexs.Remove(1);
        }
        if (max < 0)
        {
            Look(SetDir(0));
            last = 0;
        }
        else
        {
            int moveto = maxIndexs[Random.Range(0, maxIndexs.Count)];
            Walk(ConvertDirection(moveto));
            last = moveto;
        }
    }
    /// <summary>
    /// �΂߂ɃI�u�W�F�N�g�����������̏���
    /// </summary>
    /// <param name="target">����</param>
    /// <param name="com">�R�}���h</param>
    private void SolveDiagonal(int target, string com)
    {
        if (target < 3)
            y = 1;
        else
            y = 7;
        if (target == 0 || target == 6)
            x = 3;
        else
            x = 5;
        if (com == "avoid")
            priority[x] = priority[y] = -1;
        else
            priority[x] = priority[y] = 1;
    }
    /// <summary>
    /// index���A�����ɕϊ��I
    /// </summary>
    /// <param name="index">�ϊ������index</param>
    /// <returns>�ϊ�����(x,y)</returns>
    private int[] ConvertDirection(int index)
    {
        switch (index)
        {
            case 1:
                return SetDir(0);
            case 3:
                return SetDir(1);
            case 5:
                return SetDir(2);
            case 7:
                return SetDir(3);
            default:
                Debug.LogWarning("�����w��ł��ĂȂ���I");
                return null;
        }
    }
}


