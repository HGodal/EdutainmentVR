using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonSound : MonoBehaviour
{
    public AudioSource audiosource;
    // Start is called before the first frame update
    void Start()
    {
        audiosource.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    public void PlaySound (Collider other)
    {
        audiosource.Play();
        //if (other.gameObject.name.Contains("Poke.L")) {
          //  audiosource.Play();
       // }
    }
}
