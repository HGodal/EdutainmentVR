using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TurnOnVent : MonoBehaviour
{
    private GameObject hinge;
    private GameObject Vent1;
    private GameObject close;
    TextMeshProUGUI counterGUI;
    AudioSource Ventilation;
   
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other) {

        Ventilation = GetComponent<AudioSource>();
        
        hinge = GameObject.Find("/VentA");
        Vent1 = GameObject.Find("/Vent1");
        close = GameObject.Find("/SluttVent");
        

        Vector3 test = new Vector3(-1.6f, 2.28f, -2.7f);
        Vector3 test1 = new Vector3(-2.34f, 2.18f, -2.82f);
        //må sjekke om alt er på riktig plass før ventilasjon kan bli skrudd på
        if (close.transform.rotation == Quaternion.Euler(0, 90, 0)) {
            if(Vent1.transform.position == test1) {
                if(hinge.transform.position == test) {
                    Ventilation.Play();
                    
                }
            }
        }
    }

    }


