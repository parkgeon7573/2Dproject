using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Util
{ 
    public static int GetPriority(int[] table)
    {
        if (table == null || table.Length == 0) return -1;

        int sum = 0;
        for (int i = 0; i < table.Length; i++)
            sum += table[i];
        int num = Random.Range(1, sum + 1);

        sum = 1;
        for(int i = 0; i < table.Length; i++)
        {
            if (num >= sum && num < sum + table[i])
                return i;
            sum += table[i];
        }
        return -1;
    }
    public static int GetPriority(float[] table)
    {
        if (table == null || table.Length == 0) return -1;

        float sum = 0.0f;
        for (int i = 0; i < table.Length; i++)
            sum += table[i];
        float num = Random.Range(0f, sum);

        sum = 0f;
        for (int i = 0; i < table.Length; i++)
        {
            if ((Mathf.Approximately(num,sum) || num > sum) && num < sum + table[i])
                return i;
            sum += table[i];
        }
        return -1;
    }
}
