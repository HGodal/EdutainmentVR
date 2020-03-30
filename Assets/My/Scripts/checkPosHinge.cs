using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class checkPosHinge : MonoBehaviour
{

    private GameObject hinge;
  
    

    // private void OnTriggerEnter(Collider other)     
    // {
    //     hinge = GameObject.Find("/VentA");
    //     Vent1 = GameObject.Find("/Vent1");

    //     Vector3 test = new Vector3(0, 0.1f, 0);
    //     hinge.transform.position += test;
    //     Vent1.transform.position += test;
    // }

    private void OnTriggerEnter(Collider other) {
    
        hinge = GameObject.Find("/VentA");
        Vector3 test = new Vector3(-1.6f, 2.28f, -2.7f);
        hinge.transform.position = test;
         
    }

   

}
