using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ViewScore : MonoBehaviour
{
    public TextMeshProUGUI score;

    void Start()
    {
        score.text = StaticData.getScore().ToString();
    }
}
