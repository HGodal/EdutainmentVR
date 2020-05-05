using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Screw : MonoBehaviour
{
    AudioSource drill;
    BoltAction bolts;

    private void Start()
    {
        bolts = GameObject.Find("/Bolts/SkrueLogikk").GetComponent<BoltAction>();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag.Equals("Drill"))
        {
            bolts.Screws(); 
        }
    }
}
