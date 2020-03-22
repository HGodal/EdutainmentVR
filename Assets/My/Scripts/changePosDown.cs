using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class changePosDown : MonoBehaviour
{
    private GameObject hinge;
    private GameObject Vent1;

    private void OnTriggerEnter(Collider other)     
    {
        hinge = GameObject.Find("/VentA");
        Vent1 = GameObject.Find("/Vent1");

        Vector3 test = new Vector3(0, 0.01f, 0);
        hinge.transform.position -= test;
        Vent1.transform.position -= test;
    }
}
