using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeRotation : MonoBehaviour
{
    private GameObject close;
    private AudioSource correct;

    void Start()
    {
        correct = GetComponent<AudioSource>();
        close = GameObject.Find("/SluttVent");
    }

    private void OnTriggerEnter(Collider other)
    {
        close.transform.rotation = Quaternion.Euler(0, 90, 0);
        correct.Play();
    }


}
