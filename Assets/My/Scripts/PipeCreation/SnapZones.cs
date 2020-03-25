using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using VRTK.Prefabs.Interactions.InteractableSnapZone;
using TMPro;

public class SnapZones : MonoBehaviour
{
    public TextMeshProUGUI text;

    private CommonLogic logic;

    private AudioSource pling;

    public GameObject upPipe;
    public GameObject straightPipe;
    public GameObject downPipe;

    public float pipeDimension;

    private int zPosition = 0;
    private int yPosition = 1;

    int goalSpotted;

    private int zTemp;
    private int yTemp;

    private List<GameObject> pipeList = new List<GameObject>();
    private List<GameObject> tempList = new List<GameObject>();

    private int angle = 0;
    private float[] angles = new float[4] { 0f, 90f, 180f, 270f };

    private Transform parentObject;

    private int[,] pipeGrid = new int[10, 5];

    int direction = 0;
    int zDirection = 1;
    int yDirection = 1;

    private void UpdateDirection(int retning)
    {
        retning = mod(retning, 4);

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
        yDirection = 1;
        while (pipeGrid[zDirection, yDirection] != 0)
        {
            switch (pipeGrid[zDirection, yDirection])
            {
                case 0:
                    return false;
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
                    Debug.Log("Traff Mål!");
                    return true;
            }
            UpdateDirection(direction);
        }

        return false;
    }

    int mod(int x, int m)
    {
        return (x % m + m) % m;
    }

    private void changeAngle(int change)
    {
        angle = mod(angle + change, 4);
    }

    private void newLocation(int angleChange)
    {
        switch (mod(angle + angleChange, 4))
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

    public void upTurn()
    {
        changeAngle(-1);
        pipeGrid[zPosition, yPosition] = 1;
        addPipeToList("UpPipe");
        createAllSnapZones();
    }

    public void straightTurn()
    {
        changeAngle(0);
        pipeGrid[zPosition, yPosition] = 2;
        addPipeToList("StraightPipe");
        createAllSnapZones();
    }

    public void downTurn()
    {
        changeAngle(1);
        pipeGrid[zPosition, yPosition] = 3;
        addPipeToList("DownPipe");
        createAllSnapZones();
    }

    private void addPipeToList(string pipeTag)
    {
        foreach (GameObject pipe in tempList)
        {
            if (pipe.tag.Equals(pipeTag))
            {
                pipeList.Add(pipe);
            }
        }
    }

    private bool checkIfNextAvailable(int type)
    {
        int tempAngle = mod(angle + type, 4);

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

    private void createAllSnapZones()
    {
        newLocation(0);
        foreach (GameObject tempPipe in tempList)
        {
            if (!pipeList.Contains(tempPipe))
            {
                Destroy(tempPipe);
            }
        }
        tempList.Clear();

        goalSpotted = 10;

        if (checkIfNextAvailable(0))
        {
            createPipe(upPipe, "UpPipe");
        }

        if (checkIfNextAvailable(1))
        {
            createPipe(straightPipe, "StraightPipe");
        }

        if (checkIfNextAvailable(2))
        {
            createPipe(downPipe, "DownPipe");
        }

        Debug.Log("Nå har templist størrelsen: " + tempList.Count());

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
            Debug.Log("TempList inni goalspotted antall ting:" + tempList.Count());
        }

        if (CheckRoute())
        {
            GameFinish();
        }
    }

    private void createPipe(GameObject pipeType, string tag)
    {
        GameObject newPipe = Instantiate(pipeType);
        newPipe.transform.parent = parentObject;
        newPipe.transform.localPosition = new Vector3(0.0f, yPosition * pipeDimension, zPosition * pipeDimension);
        newPipe.transform.localRotation = Quaternion.Euler(angles[angle], 0.0f, 0.0f);
        newPipe.tag = tag;
        tempList.Add(newPipe);
    }

    private void printGrid()
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

    public void UnSnapped()
    {
        //pling.Play();
        //Destroy(pipeList.ElementAt(pipeList.Count - 1));
        //pipeList.RemoveAt(pipeList.Count - 1);
        //newLocation(2);
    }

    private void DeactivateSnapzones()
    {
        // foreach (GameObject pipe in pipeList)
        // {
        //     pipe.GetComponent<SnapZoneFacade>().SnappedGameObject.transform.GetChild(1).gameObject.SetActive(false);
        //     //pipe.GetComponent<SnapZoneFacade>().SnappedGameObject.transform.GetChild(2).gameObject.SetActive(false);
        // }
        // pipeList.ElementAt(pipeList.Count - 1).GetComponent<SnapZoneFacade>().SnappedGameObject.transform.GetChild(1).gameObject.SetActive(true);
        // //pipeList.ElementAt(pipeList.Count - 1).GetComponent<SnapZoneFacade>().SnappedGameObject.transform.GetChild(2).gameObject.SetActive(true);
    }

    void Start()
    {
        logic = GameObject.Find("/Logic").GetComponent<CommonLogic>();
        pipeGrid[0, 1] = 5;
        for (int i = 0; i < pipeGrid.GetLength(1); i++)
        {
            pipeGrid[9, i] = 5;
        }
        pipeGrid[9, 1] = 10;



        parentObject = GameObject.Find("/Pipes").transform;
        pling = GetComponent<AudioSource>();
        createAllSnapZones();

        straightTurn();
        straightTurn();
        straightTurn();
        straightTurn();
        straightTurn();
        straightTurn();

        //straightTurn();
        //straightTurn();

        printGrid();
    }

    private void GameFinish()
    {
        Debug.Log("Nå vant du");
        pling.Play();
        foreach (GameObject tempPipe in tempList)
        {
            Destroy(tempPipe);
        }
        tempList.Clear();
        StaticData.levelScores[1] = 100;
        //logic.WaitChangeScene(5.0f, "Menu");
    }
}
