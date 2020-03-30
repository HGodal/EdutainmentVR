using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class checkIfWin : MonoBehaviour
{
    private GameObject close;
    private GameObject hinge;
    private GameObject Vent1;

    AudioSource Correct;

    

    void Start()
    {   
      checkWin();
    }

    private void checkWin() {

        Correct = GetComponent<AudioSource>();

        hinge = GameObject.Find("/VentA");
        Vent1 = GameObject.Find("/Vent1");
        close = GameObject.Find("/SluttVent");

        Vector3 test = new Vector3(-1.6f, 2.28f, -2.7f);
        Vector3 test1 = new Vector3(-2.34f, 2.18f, -2.82f);
        

        if (close.transform.rotation == Quaternion.Euler(0, 90, 0)) {
            if(Vent1.transform.position == test1) {
                if(hinge.transform.position == test) {
                    Correct.Play();
                    

                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
   
    }
}
