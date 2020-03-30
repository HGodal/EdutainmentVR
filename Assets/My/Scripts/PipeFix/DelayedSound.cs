using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DelayedSound : MonoBehaviour
{
    AudioSource sound;

    private void Start()
    {
        sound = GetComponent<AudioSource>();
        StartCoroutine(LoopSound());
    }

    private IEnumerator LoopSound()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(1.0f, 5.0f));
            sound.Play();
        }
    }
}