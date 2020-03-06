using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class SnapZones : MonoBehaviour
{
    private CommonLogic logic;

    private AudioSource pling;

    public GameObject leftPipe;
    public GameObject straightPipe;
    public GameObject rightPipe;

    public float pipeDimension;

    private int xPosition = -1;
    private int zPosition = 0;

    private int xTemp;
    private int zTemp;

    private int totalAngle;

    private List<GameObject> pipeList = new List<GameObject>();
    private List<GameObject> tempList = new List<GameObject>();

    private int angle = 1;
    private float[] angles = new float[4] { 0f, 90f, 180f, 270f };

    private Transform parentObject;

    //  Destination
    //  Funksjon som leser verdiene i rutenett og ser om den klarer å komme seg fra start til slutt.
    //      Da skal spillet bli ferdig.

    private int[,] rutenett = new int[13, 6];
    //  4 bortover og 6 oppover
    //  Default er 0, 1 venstre, 2 rett frem, 3 høyre
    //  4 uvisst?

    int mod(int x, int m)
    {
        return (x % m + m) % m;
    }

    private void changeAngle(int change)
    {
        //  Tilsvarer mod, men må bli skrevet sånn i C#
        totalAngle = angle + change;
        angle = (totalAngle % 4 + 4) % 4;
    }

    private void newLocation()
    {
        switch (angle)
        {
            case 0:
                zPosition += 1;
                break;
            case 1:
                xPosition += 1;
                break;
            case 2:
                zPosition -= 1;
                break;
            case 3:
                xPosition -= 1;
                break;
        }
    }

    public void leftTurn()
    {
        changeAngle(-1);
        rutenett[xPosition, zPosition] = 1;
        addPipeToList("LeftPipe");
        createAllSnapZones();
    }

    public void straightTurn()
    {
        changeAngle(0);
        rutenett[xPosition, zPosition] = 2;
        addPipeToList("StraightPipe");
        createAllSnapZones();
    }

    public void rightTurn()
    {
        changeAngle(1);
        rutenett[xPosition, zPosition] = 3;
        addPipeToList("RightPipe");
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
        int tempAngle = (angle + type) % 4;
        switch (tempAngle)
        {
            case 0:
                xTemp = -1;
                zTemp = 0;
                break;
            case 1:
                xTemp = 0;
                zTemp = 1;
                break;
            case 2:
                xTemp = 1;
                zTemp = 0;
                break;
            case 3:
                xTemp = 0;
                zTemp = -1;
                break;
        }

        if (xPosition + xTemp >= rutenett.GetLength(0) || zPosition + zTemp >= rutenett.GetLength(1))
        {
            return false;
        }

        if (xPosition + xTemp < 0 || zPosition + zTemp < 0)
        {
            return false;
        }

        if (rutenett[xPosition + xTemp, zPosition + zTemp] == 0)
        {
            return true;
        }

        return false;
    }

    private void createAllSnapZones()
    {
        if (rutenett[11, 0] != 0)
        {
            pling.Play();
            StaticData.levelScores[1] = 100;
            logic.WaitChangeScene(5.0f, "Menu");
        }

        newLocation();
        foreach (GameObject tempPipe in tempList)
        {
            if (!pipeList.Contains(tempPipe))
            {
                Destroy(tempPipe);
            }
        }
        tempList.Clear();

        if (checkIfNextAvailable(0))
        {
            createPipe(leftPipe, "LeftPipe");
        }

        if (checkIfNextAvailable(1))
        {
            createPipe(straightPipe, "StraightPipe");
        }

        if (checkIfNextAvailable(2))
        {
            createPipe(rightPipe, "RightPipe");
        }
    }

    private void createPipe(GameObject pipeType, string tag)
    {
        GameObject newPipe = Instantiate(pipeType);
        newPipe.transform.parent = parentObject;
        newPipe.transform.localPosition = new Vector3(xPosition * pipeDimension, 0.0f, zPosition * pipeDimension);
        newPipe.transform.rotation = Quaternion.Euler(0.0f, angles[angle], 0.0f);
        newPipe.tag = tag;
        tempList.Add(newPipe);
    }

    private void printGrid()
    {
        string logText = "\n";
        for (int i = rutenett.GetLength(1) - 1; i >= 0; i--)
        {
            for (int j = 0; j < rutenett.GetLength(0); j++)
            {
                logText += "[" + rutenett[j, i] + "]";
            }
            logText += "\n";
        }
        Debug.Log(logText);
    }

    void Start()
    {
        logic = GameObject.Find("/Logic").GetComponent<CommonLogic>();

        parentObject = GameObject.Find("/Pipes").transform;
        pling = GetComponent<AudioSource>();
        createAllSnapZones();
    }
}
