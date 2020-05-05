using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnOnPower2 : MonoBehaviour
{

    public Material red;
    public Material green;
    public GameObject ebox2;
   
    ScoreCounter progress;
    private bool rod2 = false;


    // Start is called before the first frame update
    void Start()
    {
        progress = GameObject.Find("/InfoCanvas/InfoText").GetComponent<ScoreCounter>();
        progress.WriteInfoText();


    }


    public void TurnOnPower()
    {
        if (rod2 == false)
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

}
