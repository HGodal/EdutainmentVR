using UnityEngine;

public class DetectTool : MonoBehaviour
{
    CheckValidTools validScript;

    void Start()
    {
        validScript = GameObject.Find("/Logic").GetComponent<CheckValidTools>();
    }

    void OnTriggerEnter(Collider other)
    {
        validScript.Change(other, true);
    }

    void OnTriggerExit(Collider other)
    {
        validScript.Change(other, false);
    }
}
