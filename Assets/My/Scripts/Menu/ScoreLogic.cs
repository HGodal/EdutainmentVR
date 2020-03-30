using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ScoreLogic : MonoBehaviour
{
    static string[] spearator = { "|", "," };
    static string stringScore;
    static string[,] arrayScore;

    public static string listToString(string[,] list)
    {
        stringScore = "";
        for (int i = 0; i < list.GetLength(0); i++)
        {
            stringScore += list[i, 0] + "|" + list[i, 1] + ",";
        }
        return stringScore;
    }

    public static string[,] stringToList(string text)
    {
        int x = 0;
        int y = 0;

        arrayScore = new string[10, 2];

        string[] strlist = text.Split(spearator, 21, StringSplitOptions.None);
        for (int i = 0; i < strlist.Length - 1; i++)
        {
            x = i % 2;
            y = Mathf.FloorToInt(i / 2);
            arrayScore[y, x] = strlist[i];
        }
        return arrayScore;
    }
}
