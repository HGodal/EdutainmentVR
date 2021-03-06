﻿using UnityEngine;

public class TurnOnPower : MonoBehaviour
{
    public Material red;
    public Material green;
    public GameObject ebox1;
    public GameObject ebox2;
    public GameObject ebox3;
    bool rod;
    bool rod2;
    bool rod3;
    ScoreCounter progress;

    void Start()
    {
        progress = GameObject.Find("/InfoCanvas/InfoText").GetComponent<ScoreCounter>();
        rod = false;
        rod2 = false;
        rod3 = false;
    }

    public void TurnOn()
    {
        if (rod == false && rod2 == false && rod3 == false)
        {
            ebox1.GetComponent<Renderer>().material = green;
            // set next ebox to be red
            ebox3.GetComponent<Renderer>().material = red;
            progress.UpdateScore(1);
            rod = true;
        }
        else
        {
            progress.UpdateScore(0);
        }
    }

    public void TurnOnPower2()
    {
        if (rod == true && rod2 == false && rod3 == true)
        {
            ebox2.GetComponent<Renderer>().material = green;
            progress.UpdateScore(1);
            progress.WriteInfoText();
            rod2 = true;
        }
        else
        {
            progress.UpdateScore(0);
        }
    }

    public void TurnOnPower3()
    {
        if (rod == true && rod2 == false && rod3 == false)
        {
            ebox3.GetComponent<Renderer>().material = green;
            // set next ebox to be red
            ebox2.GetComponent<Renderer>().material = red;
            progress.UpdateScore(1);
            rod3 = true;
        }
        else
        {
            progress.UpdateScore(0);
        }
    }
}
