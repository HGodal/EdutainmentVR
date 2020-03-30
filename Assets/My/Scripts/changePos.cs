using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class changePos : MonoBehaviour
{

    
    private GameObject Vent1;

    private void Start()
    {
        Vent1 = GameObject.Find("/Vent1");
    }


    private void OnTriggerEnter(Collider other) {
        Vector3 test1 = new Vector3(-2.34f, 2.18f, -2.82f);
        Vent1.transform.position = test1;
        
    }

  

}
