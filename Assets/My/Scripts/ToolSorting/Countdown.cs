using UnityEngine;
using TMPro;

public class Countdown : MonoBehaviour
{
    public GenerateJsonInfo jsonInfo;
    private DisplayText displayText;

    public TextMeshProUGUI countdownText;
    public float timerDiff;
    float timer;
    public bool countUp = false;
    float minutes;
    float seconds;
    float oldSeconds;
    string correctTime;
    public bool isPaused = false;

    private CommonLogic commonLogic;

    AudioSource tickSound;
    AudioSource coachWhistle;

    private GameObject logic;

    private void Start()
    {
        displayText = GameObject.Find("/RoomsAndVR/Logic/DisplayTextLogic").GetComponent<DisplayText>();

        logic = GameObject.Find("/Logic");
        commonLogic = GameObject.Find("/RoomsAndVR/Logic/CommonLogic").GetComponent<CommonLogic>();

        oldSeconds = 0;
        tickSound = GetComponents<AudioSource>()[0];
        coachWhistle = GetComponents<AudioSource>()[1];

        if (countUp)
        {
            timer = 0.0f;
        }
        else
        {
            timer = timerDiff;
        }
    }

    private void Update()
    {
        if (!isPaused)
        {
            DoCounting();
        }
    }

    private void DoCounting()
    {
        if (countUp)
        {
            timer += Time.deltaTime;
        }
        else
        {
            timer -= Time.deltaTime;
        }

        minutes = Mathf.FloorToInt(timer / 60F);
        seconds = Mathf.FloorToInt(timer - minutes * 60);

        if (seconds != oldSeconds)
        {
            tickSound.Play();
            oldSeconds = seconds;
        }

        correctTime = string.Format("{0:00}:{1:00}", minutes, seconds);
        countdownText.text = "<mspace=6>" + correctTime + "</mspace>";

        if (timer <= 1.0f || timer > timerDiff + 1)
        {
            isPaused = true;
            coachWhistle.Play();
            displayText.OverwriteText(jsonInfo.GetSceneInfo("toolSorter3"));
            //logic.GetComponent<CheckValidTools>().enabled = false;  //  Denne burde ikke vært her

            commonLogic.WaitChangeScene(5.0f, "TheHub");
        }
    }
    public float GetTimer()
    {
        return timer;
    }
}
