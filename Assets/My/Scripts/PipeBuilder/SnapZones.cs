using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using TMPro;

public class SnapZones : MonoBehaviour
{
    public TextMeshProUGUI text;
    public TextMeshProUGUI infoText;

    CommonLogic logic;

    AudioSource pling;

    public GameObject upPipe;
    public GameObject straightPipe;
    public GameObject downPipe;
    public float pipeDimension;

    int zPosition;
    int yPosition;

    int goalSpotted;

    int zTemp;
    int yTemp;

    List<GameObject> pipeList = new List<GameObject>();
    List<GameObject> tempList = new List<GameObject>();

    int angle;
    float[] angles = new float[4] { 0f, 90f, 180f, 270f };

    Transform parentObject;

    int[,] pipeGrid = new int[10, 6];

    int score;
    int direction;
    int zDirection;
    int yDirection;
    int zPosition;
    int yPosition;
    int goalSpotted;
    int zTemp;
    int yTemp;
    int angle;
    int[,] pipeGrid = new int[10, 6];
    float[] angles = new float[4] { 0f, 90f, 180f, 270f };
    List<GameObject> pipeList = new List<GameObject>();
    List<GameObject> tempList = new List<GameObject>();
    Transform parentObject;
    GenerateJsonInfo jsonInfo;
    CommonLogic logic;
    AudioSource pling;

    //  Initialize --------------------------------------------------------------------------------
    void Start()
    {
        jsonInfo = GameObject.Find("/JsonLogic").GetComponent<GenerateJsonInfo>();
        logic = GameObject.Find("/Logic").GetComponent<CommonLogic>();
        parentObject = GameObject.Find("/Pipes").transform;
        pling = GetComponent<AudioSource>();
        infoText.text = jsonInfo.GetSceneInfo("pipeBuilder1");

        CreateStart();
    }

    public void CreateStart()
    {
        zPosition = 0;
        yPosition = 2;
        angle = 0;

        direction = 0;
        zDirection = 1;
        yDirection = 1;

        score = pipeGrid.GetLength(0) * pipeGrid.GetLength(1) + 1;

        for (int i = 1; i < parentObject.childCount; i++)
        {
            Destroy(parentObject.GetChild(i).gameObject);
        }
        for (int i = 0; i < parentObject.GetChild(0).childCount; i++)
        {
            Destroy(parentObject.GetChild(0).GetChild(i).gameObject);
        }
        tempList.Clear();
        pipeList.Clear();
        CreateObstacles();
        CreateAllSnapZones();
    }

    //  Methods Called by Placing Pipes ----------------------------------------------------------------
    public void UpTurn()
    {
        ChangeAngle(-1);
        pipeGrid[zPosition, yPosition] = 1;
        AddPipeToList("UpPipe");
        CreateAllSnapZones();
    }

    public void StraightTurn()
    {
        ChangeAngle(0);
        pipeGrid[zPosition, yPosition] = 2;
        AddPipeToList("StraightPipe");
        CreateAllSnapZones();
    }

    public void DownTurn()
    {
        ChangeAngle(1);
        pipeGrid[zPosition, yPosition] = 3;
        AddPipeToList("DownPipe");
        CreateAllSnapZones();
    }

    //  Create Snapzones --------------------------------------------------------------------------------
    void CreateAllSnapZones()
    {
        NewLocation();
        foreach (GameObject tempPipe in tempList)
        {
            if (!pipeList.Contains(tempPipe))
            {
                Destroy(tempPipe);
            }
        }
        tempList.Clear();

        goalSpotted = 10;

        if (CheckIfNextAvailable(0))
        {
            CreatePipe(upPipe, "UpPipe");
        }

        if (CheckIfNextAvailable(1))
        {
            CreatePipe(straightPipe, "StraightPipe");
        }

        if (CheckIfNextAvailable(2))
        {
            CreatePipe(downPipe, "DownPipe");
        }

        if (goalSpotted < tempList.Count)
        {
            Debug.Log("Goal spotted");
            for (int i = tempList.Count - 1; i >= 0; i--)
            {
                if (i != goalSpotted)
                {
                    Destroy(tempList.ElementAt(i));
                    tempList.RemoveAt(i);
                }
            }
        }

        score--;
        DisplayScore();

        if (CheckRoute())
        {
            GameFinish();
        }
    }

    void CreatePipe(GameObject pipeType, string tag)
    {
        GameObject newPipe = Instantiate(pipeType);
        newPipe.transform.parent = parentObject;
        newPipe.transform.localPosition = new Vector3(0.0f, yPosition * pipeDimension, zPosition * pipeDimension);
        newPipe.transform.localRotation = Quaternion.Euler(angles[angle], 0.0f, 0.0f);
        newPipe.tag = tag;
        tempList.Add(newPipe);
    }

    //  Calculate Next Position --------------------------------------------------------------------------------
    void UpdateDirection(int retning)
    {
        retning = Mod(retning, 4);

        switch (retning)
        {
            case 0:
                zDirection += 1;
                break;
            case 1:
                yDirection -= 1;
                break;
            case 2:
                zDirection -= 1;
                break;
            case 3:
                yDirection += 1;
                break;
        }
    }

    bool CheckRoute()
    {
        direction = 0;
        zDirection = 1;
        yDirection = 2;
        while (pipeGrid[zDirection, yDirection] != 0 && pipeGrid[zDirection, yDirection] != 5)
        {
            switch (pipeGrid[zDirection, yDirection])
            {
                case 1:
                    direction--;
                    break;
                case 2:
                    //  Skal bare gå rett frem;
                    break;
                case 3:
                    direction++;
                    break;
                default:
                    pling.Play();
                    return true;
            }
            UpdateDirection(direction);
        }

        return false;
    }

    int Mod(int x, int m)
    {
        return (x % m + m) % m;
    }

    void ChangeAngle(int change)
    {
        angle = Mod(angle + change, 4);
    }

    void NewLocation()
    {
        switch (Mod(angle, 4))
        {
            case 0:
                zPosition += 1;
                break;
            case 1:
                yPosition -= 1;
                break;
            case 2:
                zPosition -= 1;
                break;
            case 3:
                yPosition += 1;
                break;
        }
    }

    bool CheckIfNextAvailable(int type)
    {
        int tempAngle = Mod(angle + type, 4);

        switch (tempAngle)
        {
            case 0:
                zTemp = 0;
                yTemp = 1;
                break;
            case 1:
                zTemp = 1;
                yTemp = 0;
                break;
            case 2:
                zTemp = 0;
                yTemp = -1;
                break;
            case 3:
                zTemp = -1;
                yTemp = 0;
                break;
        }

        if (zPosition + zTemp >= pipeGrid.GetLength(0) || yPosition + yTemp >= pipeGrid.GetLength(1))
        {
            return false;
        }

        if (zPosition + zTemp < 0 || yPosition + yTemp < 0)
        {
            return false;
        }

        if (pipeGrid[zPosition + zTemp, yPosition + yTemp] == 0)
        {
            return true;
        }
        if (pipeGrid[zPosition + zTemp, yPosition + yTemp] == 10)
        {
            goalSpotted = type;
            return true;
        }

        return false;
    }

    //  Create Obstacles --------------------------------------------------------------------------------
    void CreateObstacles()
    {
        for (int i = 1; i < pipeGrid.GetLength(0) - 2; i++)
        {
            for (int j = 0; j < pipeGrid.GetLength(1) - 1; j++)
            {
                pipeGrid[i, j] = 0;
                if (Random.Range(0.0f, 1.0f) > 0.7)
                {
                    if (!(i == zPosition + 1 && j == yPosition))
                    {
                        pipeGrid[i, j] = 5;
                        CreatePhysicalObstacles(i, j);
                    }
                }
            }
        }

        for (int i = 0; i < pipeGrid.GetLength(1); i++)
        {
            pipeGrid[9, i] = 5;
            pipeGrid[0, i] = 5;
        }

        pipeGrid[9, 2] = 10;
    }

    void CreatePhysicalObstacles(int zPos, int yPos)
    {
        GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        cube.transform.parent = parentObject.GetChild(0).transform;
        cube.transform.localScale = new Vector3(0.2f, pipeDimension, pipeDimension);
        cube.transform.localPosition = new Vector3(0, yPos * pipeDimension, zPos * pipeDimension);
        cube.GetComponent<Renderer>().material.color = new Color(0.2f, 0.2f, 0.2f);
    }

    //  Finish Game --------------------------------------------------------------------------------
    void GameFinish()
    {
        pling.Play();
        foreach (GameObject tempPipe in tempList)
        {
            Destroy(tempPipe);
        }
        tempList.Clear();
        StaticData.levelScores[1] = score;
        infoText.text = jsonInfo.GetSceneInfo("pipeBuilder2");
        logic.WaitChangeScene(5.0f, "TheHub");
    }

    //  Other Methods --------------------------------------------------------------------------------
    void AddPipeToList(string pipeTag)
    {
        foreach (GameObject pipe in tempList)
        {
            if (pipe.tag.Equals(pipeTag))
            {
                pipeList.Add(pipe);
            }
        }
    }

    void DisplayScore()
    {
        text.text = score.ToString();
    }
}
