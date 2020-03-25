using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK.Prefabs.Interactions.InteractableSnapZone;

public class ControlPipeGrabFunction : MonoBehaviour
{
    SnapZoneFacade snapObject;
    void Start()
    {
        snapObject = GetComponent<SnapZoneFacade>();
    }

    void Update()
    {

    }

    public void RemoveGrabOption(List<GameObject> list)
    {
        foreach (GameObject pipe in list)
        {

        }
    }
}
