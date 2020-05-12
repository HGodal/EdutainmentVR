using TMPro;
using UnityEngine;

public class CheckValidTools : MonoBehaviour
{
    GenerateJsonInfo jsonInfo;
    DisplayText[] displayText;
    CommonLogic commonLogic;
    Countdown countDownTimer;
    AudioSource[] sounds;

    int counter;
    bool isActive;
    public int numOfCorrectItems;

    void Start()
    {
        jsonInfo = GameObject.Find("/JsonLogic").GetComponent<GenerateJsonInfo>();
        displayText = GameObject.Find("/RoomsAndVR/Logic/DisplayTextLogic").GetComponents<DisplayText>();
        commonLogic = GameObject.Find("/RoomsAndVR/Logic/CommonLogic").GetComponent<CommonLogic>();
        countDownTimer = GameObject.Find("/RoomsAndVR/Logic/CountdownLogic").GetComponent<Countdown>();
        sounds = GetComponents<AudioSource>();

        counter = 0;
        isActive = true;

        displayText[0].OverwriteText(string.Format(jsonInfo.GetSceneInfo("toolSorter1"), numOfCorrectItems));
        displayText[1].OverwriteText(counter.ToString());
    }

    public void Change(Collider other, bool entered)
    {
        if (isActive)
        {
            if (other.gameObject.tag == "Valid")
            {
                if (entered)
                {
                    counter++;
                    sounds[0].Play();
                }
                else
                {
                    counter--;
                }
            }
            else if (other.gameObject.tag == "Invalid")
            {
                if (entered)
                {
                    counter--;
                    sounds[1].Play();
                }
                else
                {
                    counter++;
                }
            }
            CheckScore();
        }
    }

    void CheckScore()
    {
        displayText[1].OverwriteText(counter.ToString());

        if (counter >= numOfCorrectItems)
        {
            sounds[2].Play();
            displayText[0].OverwriteText(jsonInfo.GetSceneInfo("toolSorter2"));
            countDownTimer.isPaused = true;
            StaticData.levelScores[0] = Mathf.FloorToInt(countDownTimer.GetTimer());
            commonLogic.WaitChangeScene(5.0f, "TheHub");
            isActive = false;
        }
    }
}
