using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class checkPosHinge : MonoBehaviour
{

    private GameObject hinge;

    private void Start()
    {
        hinge = GameObject.Find("/VentA");
    }

    private void OnTriggerEnter(Collider other) {
    
        
        Vector3 test = new Vector3(-1.6f, 2.28f, -2.7f);
        hinge.transform.position = test;
         
    }

   

}
