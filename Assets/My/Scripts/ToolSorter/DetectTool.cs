using UnityEngine;

public class DetectTool : MonoBehaviour
{
    private CheckValidTools validScript;

    private void Start()
    {
        validScript = GameObject.Find("/Logic").GetComponent<CheckValidTools>();
    }

    private void OnTriggerEnter(Collider other)
    {
        validScript.Change(other, true);
    }

    private void OnTriggerExit(Collider other)
    {
        validScript.Change(other, false);
    }
}
