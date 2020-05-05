 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnOnPower : MonoBehaviour
{ 
    
    public Material red;
    public Material green;
    public GameObject ebox1;
    public GameObject ebox3;

    ScoreCounter progress;
    private bool rod = false;


    // Start is called before the first frame update
    void Start()
    {
        progress = GameObject.Find("/InfoCanvas/InfoText").GetComponent<ScoreCounter>();    
    }

    public void TurnOn()
    {
        if (rod == false)
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
}
