using UnityEngine;

public class WallGrid : MonoBehaviour
{
    public Material gridMaterial;

    void Start()
    {
        for (float i = 0.0f; i <= 3.9; i += 0.354f)
        {
            GameObject gridPart = CreateDefaultCube();

            gridPart.transform.localPosition = new Vector3(0, 0.1f, i);
            gridPart.transform.localScale = new Vector3(0.02f, 3.8f, 0.02f);
        }

        for (float i = 0.0f; i <= 2.1; i += 0.354f)
        {
            GameObject gridPart = CreateDefaultCube();

            gridPart.transform.localPosition = new Vector3(0, i, 1.9f);
            gridPart.transform.localScale = new Vector3(0.02f, 0.02f, 4.0f);
        }
    }

    GameObject CreateDefaultCube()
    {
        GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        cube.transform.parent = GameObject.Find("/Grid").transform;
        Destroy(cube.GetComponent<BoxCollider>());
        cube.GetComponent<Renderer>().material = gridMaterial;
        cube.layer = 9;
        return cube;
    }
}
