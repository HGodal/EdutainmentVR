using UnityEngine;

public class SluttVent : MonoBehaviour
{
    public Transform ventilPrefab;
    public Transform spawnPoint;
    ScoreCounter progress;
    void Start()
    {
        progress = GameObject.Find("/InfoCanvas/InfoText").GetComponent<ScoreCounter>();
    }

    public void makeNewVent()
    {
        Transform v = Instantiate(ventilPrefab);
        v.position = spawnPoint.position;

        v.gameObject.name = "ventilFix";

        v.gameObject.AddComponent<Rigidbody>();
        v.gameObject.GetComponent<Rigidbody>().useGravity = true;

        progress.UpdateScore(5);
        progress.WriteInfoText();
    }
}
