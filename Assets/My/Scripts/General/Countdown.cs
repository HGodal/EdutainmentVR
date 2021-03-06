using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Countdown : MonoBehaviour
{
    public TextMeshProUGUI countdownText;
    public float timerDiff;
    public bool countUp = false;
    public bool isPaused = false;
    GenerateJsonInfo jsonInfo;
    DisplayText displayText;
    float timer;
    float minutes;
    float seconds;
    float oldSeconds;
    string correctTime;
    CommonLogic commonLogic;
    AudioSource tickSound;
    AudioSource coachWhistle;
    GameObject logic;

    void Start()
    {
        jsonInfo = GameObject.Find("/JsonLogic").GetComponent<GenerateJsonInfo>();
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

    void Update()
    {
        if (!isPaused)
        {
            DoCounting();
        }
    }

    void DoCounting()
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
            if (SceneManager.GetActiveScene().name.Equals("VerktoySortering"))
            {
                 displayText.OverwriteText(jsonInfo.GetSceneInfo("toolSorter3"));
                 logic.GetComponent<CheckValidTools>().enabled = false;
            }
            commonLogic.WaitChangeScene(5.0f, "TheHub");
        }
    }
    public float GetTimer()
    {
        return timer;
    }
}
