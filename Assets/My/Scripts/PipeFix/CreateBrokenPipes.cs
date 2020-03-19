using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateBrokenPipes : MonoBehaviour
{
    GameObject pipes;
    AudioSource[] drippingSounds;
    AudioSource tempSound;
    int numberOfBrokenPipes;
    ProgressOverview progress;

    private void Start()
    {
        pipes = GameObject.Find("/Pipes");
        drippingSounds = GetComponents<AudioSource>();
        numberOfBrokenPipes = 0;
        progress = GameObject.Find("/InfoCanvas/InfoText").GetComponent<ProgressOverview>();

        BreakPipes();
    }

    private void BreakPipes()
    {
        foreach (Transform item in pipes.transform)
        {
            if (Random.Range(0.0f, 1.0f) >= 0.9f)
            {
                item.gameObject.tag = "Pipe";
                item.gameObject.GetComponent<Renderer>().material.SetColor("_Color", Color.red);
                item.gameObject.AddComponent<DelayedSound>();

                tempSound = item.gameObject.AddComponent<AudioSource>();
                tempSound.clip = drippingSounds[Random.Range(0, drippingSounds.Length)].clip;

                numberOfBrokenPipes++;
            }
        }
        progress.SetBrokenPipes(numberOfBrokenPipes);
    }
}