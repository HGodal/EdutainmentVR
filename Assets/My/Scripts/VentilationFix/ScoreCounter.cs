using UnityEngine;
using TMPro;

public class ScoreCounter : MonoBehaviour
{
    bool finished;
    int score;
    
    AudioSource victory;
    TextMeshProUGUI scoreText;
    CommonLogic commonLogic;
    GenerateJsonInfo allInfo;
    Countdown countDownTimer;

    void Start()
    {
        allInfo = GameObject.Find("/JsonLogic").GetComponent<GenerateJsonInfo>();
        finished = false;
        countDownTimer = GameObject.Find("/RoomsAndVR/Logic/CountDown").GetComponent<Countdown>();
        score = 0;
        victory = GetComponent<AudioSource>();
        scoreText = GameObject.Find("/TVset/ScoreCanvas/ScoreCounter").GetComponent<TextMeshProUGUI>();
        commonLogic = GameObject.Find("/RoomsAndVR/Logic/CommonLogic").GetComponent<CommonLogic>();

        WriteInfoText();

        Debug.Log(allInfo.GetSceneInfo("ventFixer2"));
    }

    public void WriteInfoText()
    {
        if (score == 0)
        {
            GetComponent<TextMeshProUGUI>().text = allInfo.GetSceneInfo("ventFixer1");
        }
        else if (score >= 3 && score < 5)
        {
            GetComponent<TextMeshProUGUI>().text = allInfo.GetSceneInfo("ventFixer2");
        }
        else if (score >= 5 && score < 10)
        {
            GetComponent<TextMeshProUGUI>().text = allInfo.GetSceneInfo("ventFixer3");
        }
        else if (score >= 10)
        {
            GetComponent<TextMeshProUGUI>().text = allInfo.GetSceneInfo("ventFixer4");
            StaticData.levelScores[4] = score;
            countDownTimer.isPaused = true;
            commonLogic.WaitChangeScene(5.0f, "TheHub");
            victory.Play();
        }
    }

    public void UpdateScore(int value)
    {
        if (!finished)
        {
            score += value;
            scoreText.text = score.ToString();
        }
    }
}