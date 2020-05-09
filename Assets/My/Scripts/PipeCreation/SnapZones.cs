using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using TMPro;

public class SnapZones : MonoBehaviour
{
    GenerateJsonInfo jsonInfo;
    public TextMeshProUGUI text;
    public TextMeshProUGUI infoText;

    private CommonLogic logic;

    private AudioSource pling;

    public GameObject upPipe;
    public GameObject straightPipe;
    public GameObject downPipe;

    public float pipeDimension;

    private int zPosition;
    private int yPosition;

    int goalSpotted;

    private int zTemp;
    private int yTemp;

    private List<GameObject> pipeList = new List<GameObject>();
    private List<GameObject> tempList = new List<GameObject>();

    private int angle;
    private float[] angles = new float[4] { 0f, 90f, 180f, 270f };

    private Transform parentObject;

    private int[,] pipeGrid = new int[10, 6];

    int score;

    int direction;
    int zDirection;
    int yDirection;

    //  Initialize --------------------------------------------------------------------------------
    void Start()
    {
        jsonInfo = GameObject.Find("/JsonInfo").GetComponent<GenerateJsonInfo>();

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

        PrintGrid();
    }

    //  Call Class --------------------------------------------------------------------------------
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
    private void CreateAllSnapZones()
    {
        NewLocation(0);
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

    private void CreatePipe(GameObject pipeType, string tag)
    {
        GameObject newPipe = Instantiate(pipeType);
        newPipe.transform.parent = parentObject;
        newPipe.transform.localPosition = new Vector3(0.0f, yPosition * pipeDimension, zPosition * pipeDimension);
        newPipe.transform.localRotation = Quaternion.Euler(angles[angle], 0.0f, 0.0f);
        newPipe.tag = tag;
        tempList.Add(newPipe);
    }

    //  Calculate Next Position --------------------------------------------------------------------------------
    private void UpdateDirection(int retning)
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

    private bool CheckRoute()
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

    private void ChangeAngle(int change)
    {
        angle = Mod(angle + change, 4);
    }

    private void NewLocation(int angleChange)
    {
        switch (Mod(angle + angleChange, 4))
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

    private bool CheckIfNextAvailable(int type)
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
    private void CreateObstacles()
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

    private void CreatePhysicalObstacles(int zPos, int yPos)
    {
        GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        cube.transform.parent = parentObject.GetChild(0).transform;
        cube.transform.localScale = new Vector3(0.2f, pipeDimension, pipeDimension);
        cube.transform.localPosition = new Vector3(0, yPos * pipeDimension, zPos * pipeDimension);
        cube.GetComponent<Renderer>().material.color = new Color(0.2f, 0.2f, 0.2f);
    }

    //  Finish Game --------------------------------------------------------------------------------
    private void GameFinish()
    {
        pling.Play();
        foreach (GameObject tempPipe in tempList)
        {
            Destroy(tempPipe);
        }
        tempList.Clear();
        StaticData.levelScores[1] = score;
        infoText.text = jsonInfo.GetSceneInfo("pipeBuilder2");
        logic.WaitChangeScene(5.0f, "Menu");
    }

    //  Other Methods --------------------------------------------------------------------------------
    private void AddPipeToList(string pipeTag)
    {
        foreach (GameObject pipe in tempList)
        {
            if (pipe.tag.Equals(pipeTag))
            {
                pipeList.Add(pipe);
            }
        }
    }

    private void DisplayScore()
    {
        text.text = score.ToString();
    }

    private void PrintGrid()
    {
        string logText = "\n";
        for (int i = pipeGrid.GetLength(1) - 1; i >= 0; i--)
        {
            for (int j = 0; j < pipeGrid.GetLength(0); j++)
            {
                logText += "[" + pipeGrid[j, i] + "]";
            }
            logText += "\n";
        }
        Debug.Log(logText);
    }
}
