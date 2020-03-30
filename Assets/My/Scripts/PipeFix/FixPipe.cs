using UnityEngine;
using TMPro;

public class FixPipe : MonoBehaviour
{
    Material pipeColor;
    AudioSource fixSound;
    ProgressOverview progress;

    private void Start()
    {
        pipeColor = Resources.Load<Material>("PipeColor");
        fixSound = GetComponent<AudioSource>();
        progress = GameObject.Find("/InfoCanvas/InfoText").GetComponent<ProgressOverview>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Pipe"))
        {
            progress.UpdateScore(10);
            progress.WriteInfoText();
            fixSound.Play();
            other.gameObject.GetComponent<Renderer>().material = pipeColor;
            other.gameObject.tag = "Untagged";

            Destroy(other.gameObject.GetComponent<DelayedSound>());
            Destroy(other.gameObject.GetComponent<AudioSource>());
        }
    }
}