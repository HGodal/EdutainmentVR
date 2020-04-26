using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class InConStats : MonoBehaviour
{
    [Range(0, 1)]
    public int sideClearance;
    [Range(0, 1)]
    public int lowerClearance;
    [Range(0, 1)]
    public int correctRoom;
    [Range(0, 2)]
    public int placement;

    public int CalculateScore()
    {
        return sideClearance * 5 + lowerClearance * 10 + correctRoom * 20 + placement * 5;
    }
}
