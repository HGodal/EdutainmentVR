using System.Collections;
using UnityEngine;

public class DelayedSound : MonoBehaviour
{
    AudioSource sound;

    void Start()
    {
        sound = GetComponent<AudioSource>();
        StartCoroutine(LoopSound());
    }

    IEnumerator LoopSound()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(1.0f, 5.0f));
            sound.Play();
        }
    }
}