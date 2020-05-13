using UnityEngine;

public class PuddleLogic : MonoBehaviour
{
    float time;
    float frequency1;
    float frequency2;
    Transform water1;
    Transform water2;

    void Start()
    {
        time = 0;
        frequency1 = Random.Range(1f, 4f);
        frequency2 = Random.Range(1f, 4f);
        water1 = transform.GetChild(0);
        water2 = transform.GetChild(1);
    }

    void Update()
    {
        time += Time.deltaTime;

        float sine1 = Mathf.Sin(time * frequency1) / 8;
        float sine2 = Mathf.Sin(time * frequency2) / 5;

        water1.localPosition = new Vector3(sine1, 0, -0.05f);
        water2.localPosition = new Vector3(sine2, 0, 0.05f);
    }
}
