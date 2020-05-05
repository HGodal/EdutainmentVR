using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnOnPower3 : MonoBehaviour
{

    public Material red;
    public Material green;
    public GameObject ebox3;
    public GameObject ebox2;
    int counter;
    ScoreCounter progress;
    private bool rod3 = false;


    // Start is called before the first frame update
    void Start()
    {
        progress = GameObject.Find("/InfoCanvas/InfoText").GetComponent<ScoreCounter>();
        progress.WriteInfoText();


    }



    public void TurnOnPower()
    {
        if (rod3 == false)
        {
            ebox3.GetComponent<Renderer>().material = green;
            // set next ebox to be red
            ebox2.GetComponent<Renderer>().material = red;
            progress.UpdateScore(1);
            progress.WriteInfoText();
            rod3 = true;
        }
        else
        {
            progress.UpdateScore(0);
        }
    }

}
