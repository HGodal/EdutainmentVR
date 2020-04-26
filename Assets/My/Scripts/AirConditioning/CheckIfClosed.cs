using UnityEngine;

public class CheckIfClosed : MonoBehaviour
{
    bool isClosed;

    void Start()
    {
        isClosed = false;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 23)
        {
            isClosed = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == 23)
        {
            isClosed = false;
        }
    }

    public bool GetClosedStatus()
    {
        return isClosed;
    }
}
