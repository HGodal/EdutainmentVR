using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorGrid : MonoBehaviour
{
    public Material gridMaterial;

    // Start is called before the first frame update
    void Start()
    {
        for (float i = -3.3f; i <= 3.3; i+=0.3f)
        {
            GameObject gridPart = createDefaultCube();

            gridPart.transform.position = new Vector3(0, -0.015f, i);
            gridPart.transform.localScale = new Vector3(5.0f, 0.03f, 0.03f);
        }

        for (float i = -2.4f; i <= 2.4; i+=0.3f)
        {
            GameObject gridPart = createDefaultCube();

            gridPart.transform.rotation = Quaternion.Euler(0, 90, 0);;
            gridPart.transform.position = new Vector3(i, -0.015f, 0);
            gridPart.transform.localScale = new Vector3(7.0f, 0.03f, 0.03f);
        }
    }

    private GameObject createDefaultCube(){
            GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
            cube.transform.parent = GameObject.Find("/Grid").transform;
            Destroy(cube.GetComponent<BoxCollider>());
            cube.GetComponent<Renderer>().material = gridMaterial;
            cube.layer = 9;
            return cube;
    }
}
