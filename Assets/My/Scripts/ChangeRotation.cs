using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeRotation : MonoBehaviour
{
    private GameObject close;
    AudioSource Correct;
   
    // Start is called before the first frame update
    void Start()
    {   
        Correct.GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other) {
        close = GameObject.Find("/SluttVent");
        close.transform.rotation = Quaternion.Euler(0, 90, 0);
        Correct.Play();
    }

   
}
