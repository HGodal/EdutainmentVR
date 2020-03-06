using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectTool : MonoBehaviour
{
    private CheckValidTools validScript;

    private void Start(){
        validScript = GameObject.Find("Logic").GetComponent<CheckValidTools>();
    }
    private void OnTriggerEnter(Collider other) {
        validScript.Enter(other);
    }

    private void OnTriggerExit(Collider other) {
        validScript.Exit(other);
    }
}
