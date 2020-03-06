using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatePipe : MonoBehaviour
{
    public GameObject interactableObject;

    private GameObject createPipeObject()
    {
        GameObject newPipe = Instantiate(interactableObject);
        newPipe.transform.parent = GameObject.Find("/Pipes").transform;
        newPipe.transform.position = new Vector3(0.0f, 0.0f, 0.0f);
 
        return newPipe;
    }

    // Start is called before the first frame update
    void Start()
    {
        createPipeObject();
        createPipeObject();
        createPipeObject();
        createPipeObject();
        createPipeObject();
        createPipeObject();
        createPipeObject();
        createPipeObject();
        createPipeObject();
    }
}
