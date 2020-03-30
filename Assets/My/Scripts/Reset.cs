using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reset : MonoBehaviour
{
    public SnapZones script;


    private void OnTriggerEnter(Collider other)
    {
        script.CreateStart();
    }
}
