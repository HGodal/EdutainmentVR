using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK.Prefabs.Interactions.Controllables;
using UnityEngine.SceneManagement;
using System.Linq;


public class Progress : MonoBehaviour
{
    public AudioSource victorySound;
    public AudioSource progressSound;
    public DisplayText text;
    public DisplayText score;
    public DisplayText conPlacement;
    GenerateJsonInfo generateJsonInfo;
    public GameObject inConGrab;
    public GameObject outConGrab;
    GameObject validators;
    GameObject inConSnaps;
    GameObject outConSnaps;
    List<string> informationText;
    List<int> segmentScores;
    private int step;
    private int openObjects;
    public CommonLogic commonLogic;

    void Start()
    {
        conPlacement.GetComponent<DisplayText>().textObject.transform.parent.gameObject.SetActive(false);
        step = 0;
        openObjects = 6;
        validators = GameObject.Find("/Interactables/Validators");
        inConSnaps = GameObject.Find("/InConSnaps");
        outConSnaps = GameObject.Find("/OutConSnaps");
        generateJsonInfo = GameObject.Find("/JsonLogic").GetComponent<GenerateJsonInfo>();
        informationText = generateJsonInfo.GetSceneInfoList("ventilationSteps");
        segmentScores = new List<int>();

        ButtonPress();
    }

    public void ButtonPress()
    {
        switch (step)
        {
            case 0:
                UpdateScoreAndInfo(0);
                break;
            case 1:
                CheckClosedObjects();
                break;
            case 2:
                CheckAirconditionInside();
                break;
            case 3:
                CheckAirconditionOutside();
                break;
            default:
                break;
        }
    }

    private void UpdateScoreAndInfo(int newScore)
    {
        progressSound.Play();

        segmentScores.Add(newScore);
        score.OverwriteText(segmentScores.Sum().ToString());

        text.OverwriteText(string.Format(informationText.ElementAt(step), segmentScores.Sum(), 123));

        step++;
    }

    public void CheckClosedObjects()
    {
        //  Fullfører trinnet.
        int closedObjects = 0;
        foreach (Transform child in validators.transform)
        {
            if (child.GetComponent<CheckIfClosed>().GetClosedStatus())
            {
                closedObjects++;
            }
        }
        UpdateScoreAndInfo((int)Mathf.Ceil((closedObjects * 30) / openObjects));

        //  Setter opp neste trinn.
        Instantiate(inConGrab);
        conPlacement.GetComponent<DisplayText>().textObject.transform.parent.gameObject.SetActive(true);
        conPlacement.OverwriteText(generateJsonInfo.GetSceneInfo("insideVentilationRules"));
    }

    private void CheckAirconditionInside()
    {
        //  Fullfører trinnet.
        foreach (Transform snapZone in inConSnaps.transform)
        {
            if (snapZone.GetComponent<UsageStatus>().GetUsageStatus())
            {
                UpdateScoreAndInfo(snapZone.GetComponent<InConStats>().CalculateScore());

                //  Setter opp neste trinn.
                Instantiate(outConGrab);
                conPlacement.OverwriteText(generateJsonInfo.GetSceneInfo("outsideVentilationRules"));
                return;
            }
        }
    }

    private void CheckAirconditionOutside()
    {
        //  Fullfører trinnet
        foreach (Transform snapZone in outConSnaps.transform)
        {
            if (snapZone.GetComponent<UsageStatus>().GetUsageStatus())
            {
                if (snapZone.GetComponent<OutConStats>().CalculateScore(inConSnaps) != -1)
                {
                    conPlacement.GetComponent<DisplayText>().textObject.transform.parent.gameObject.SetActive(false);
                    UpdateScoreAndInfo(snapZone.GetComponent<OutConStats>().CalculateScore(inConSnaps));

                    victorySound.Play();
                    StaticData.levelScores[5] = segmentScores.Sum();
                    commonLogic.WaitChangeScene(5.0f, "TheHub");
                }
                return;
            }
        }
    }
}
