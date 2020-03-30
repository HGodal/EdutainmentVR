using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class changePos : MonoBehaviour
{

    
    private GameObject Vent1;
    

    // private void OnTriggerEnter(Collider other)     
    // {
    //     hinge = GameObject.Find("/VentA");
    //     Vent1 = GameObject.Find("/Vent1");

    //     Vector3 test = new Vector3(0, 0.1f, 0);
    //     hinge.transform.position += test;
    //     Vent1.transform.position += test;
    // }

    private void OnTriggerEnter(Collider other) {
        
        Vent1 = GameObject.Find("/Vent1");
        Vector3 test1 = new Vector3(-2.34f, 2.18f, -2.82f);
        Vent1.transform.position = test1;
        
    }

  

}
