using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class checkPosHinge : MonoBehaviour
{

    private GameObject hinge;

    ScoreView progress;

    private void Start()
    {
        hinge = GameObject.Find("/VentA");
        progress = GameObject.Find("/InfoCanvas/InfoText").GetComponent<ScoreView>();
    }


    public void ChangeHinge()
    {
        Vector3 test = new Vector3(-1.6f, 2.28f, -2.7f);
        hinge.transform.position = test;
        progress.UpdateScore(5);
    }

   

}
