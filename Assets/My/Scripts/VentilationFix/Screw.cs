using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Screw : MonoBehaviour
{
    private float screwOffset = 0.075f;
    AudioSource drill;
    private bool unscrewed = false;
    BoltAction bolts;

    private void Start()
    {
        bolts = GameObject.Find("/Bolts/SkrueLogikk").GetComponent<BoltAction>();

        AudioSource drill = gameObject.AddComponent<AudioSource>();
        drill.clip = Resources.Load("Correct") as AudioClip;
        drill.playOnAwake = false;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag.Equals("Drill"))
        {
            bolts.Screws(); 
            drill.Play();
        }
    }
}
