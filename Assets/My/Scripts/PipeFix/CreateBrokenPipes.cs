using UnityEngine;

public class CreateBrokenPipes : MonoBehaviour
{
    GameObject pipes;
    AudioSource[] drippingSounds;
    AudioSource tempSound;
    int numberOfBrokenPipes;
    ProgressOverview progress;
    public GameObject puddle;

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

                CreateLeakageIndicator(item.gameObject);

                numberOfBrokenPipes++;
            }
        }
        progress.SetBrokenPipes(numberOfBrokenPipes);
    }

    private void CreateLeakageIndicator(GameObject brokenPipe)
    {
        GameObject newPuddle = Instantiate(puddle);
        newPuddle.transform.parent = brokenPipe.transform;
        newPuddle.transform.position = new Vector3(brokenPipe.transform.position.x, 0, brokenPipe.transform.position.z);
    }
}
