using UnityEngine;

public class GenerateWall : MonoBehaviour
{
    public GameObject wallPart;
    float wallPartSize;

    void Start()
    {
        wallPartSize = 0.4f;

        for (int i = 0; i < 17; i++)
        {
            for (int j = 0; j < 5; j++)
            {
                GameObject tempPart = InitWallPart();
                tempPart.transform.localPosition = new Vector3(0, wallPartSize * j, wallPartSize * i);
                //Color background = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
                float color = (float)(i + j) / (float)(2 * (17 + 5) - 4) + 0.2f;
                Color background = new Color(color, color, color);
                tempPart.GetComponent<Renderer>().material.SetColor("_Color", background);
            }
        }
    }

    private GameObject InitWallPart()
    {
        GameObject wall = Instantiate(wallPart);
        wall.transform.parent = transform;
        return wall;
    }
}
