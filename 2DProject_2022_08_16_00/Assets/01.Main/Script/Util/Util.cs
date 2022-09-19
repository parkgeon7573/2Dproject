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
}
