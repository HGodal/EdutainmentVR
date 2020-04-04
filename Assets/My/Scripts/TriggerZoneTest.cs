using UnityEngine;

public class TriggerZoneTest : MonoBehaviour
{

    public AudioSource correctSound;
    public AudioSource incorrectSound;

    private void Start()
    {

    }

    public void OnTriggerEnter(Collider collider)
    {

        if (collider.gameObject.tag == "Valid")
        {
            correctSound.Play();
        }
        else if (collider.gameObject.tag == "Invalid")
        {
            incorrectSound.Play();
        }
    }
}
