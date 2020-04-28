using UnityEngine;
using VRTK.Prefabs.Interactions.Interactables;

public class DoorGrabber : MonoBehaviour
{
    public Transform handler;

    public void ReleaseDoor()
    {
        transform.position = handler.transform.position;
        transform.rotation = handler.transform.rotation;

        Rigidbody rbHandler = handler.GetComponent<Rigidbody>();
        rbHandler.velocity = Vector3.zero;
        rbHandler.angularVelocity = Vector3.zero;
    }

    void Update()
    {
        if (Vector3.Distance(handler.position, transform.position) > 0.4f)
        {
            GetComponent<InteractableFacade>().Ungrab();
        }
    }
}
