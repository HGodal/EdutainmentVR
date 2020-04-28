using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CheckObjects : MonoBehaviour
{

    TextMeshProUGUI infoGUI;
    TextMeshProUGUI counterGUI;
    int endScore;
    int counter;
    private GameObject close;
    private GameObject hinge;
    private GameObject Vent1;
    AudioSource Correct;
    AudioSource Ventilation;
    Countdown countDownTimer;
    CommonLogic logic;
    ScoreView progress;

    private void Start() {

        Correct = GetComponent<AudioSource>();

        hinge = GameObject.Find("/VentA");
        Vent1 = GameObject.Find("/Vent1");
        close = GameObject.Find("/SluttVent");

       
        checkValid();
        
    }


    public void checkValid() 
    {
        Vector3 test = new Vector3(-1.6f, 2.28f, -2.7f);
        Vector3 test1 = new Vector3(-2.317f, 2.149995f, -2.82f);
        

        if (close.transform.rotation == Quaternion.Euler(0, 90, 0)) {
           progress.UpdateScore(5);
            if(Vent1.transform.position == test1) {
                progress.UpdateScore(5);
                if(hinge.transform.position == test) {
                    progress.UpdateScore(5);

                    }

                }
            progress.WriteInfoText();
            }
        }
        
   
    }
    
   

