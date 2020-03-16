using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateWall : MonoBehaviour
{
    public GameObject wallPart;
    float wallPartSize;
    Transform root;

    void Start()
    {
        wallPartSize = 0.4f;
        root = GameObject.Find("/RoomsAndVR/Rooms/DefaultRoom/BreakReadyWall").transform;

        for (int i = 0; i < 18; i++)
        {
            for (int j = 0; j < 5; j++)
            {
                GameObject tempPart = initWallPart();
                tempPart.transform.localPosition = new Vector3(0, wallPartSize * j, wallPartSize * i);
                // Color background = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
                // tempPart.GetComponent<Renderer>().material.SetColor("_Color", background);
            }
        }
    }

    private GameObject initWallPart()
    {
        GameObject wall = Instantiate(wallPart);
        wall.transform.parent = root;
        wall.transform.localPosition = new Vector3(0, 0, 0);
        return wall;
    }
}
