using UnityEngine;

public class ButtonTrigger : MonoBehaviour
{
    public Transform pipePrefab;
    public Transform spawnPoint;

    public void InitiatePipe()
    {
        Transform t = Instantiate(pipePrefab);
        t.position = spawnPoint.position;
    }
}
