using UnityEngine;
using TMPro;

public class ScoreView : MonoBehaviour
{
    int score;
   
    TextMeshProUGUI scoreText;
    CommonLogic commonLogic;
    public GenerateJsonInfo allInfo;

    Countdown countDownTimer;

    private void Start()
    {
        countDownTimer = GameObject.Find("CountDown").GetComponent<Countdown>();
        score = 0;
        
        scoreText = GameObject.Find("/TVset/ScoreCanvas/ScoreCounter").GetComponent<TextMeshProUGUI>();
        commonLogic = GameObject.Find("RoomsAndVR/Logic/CommonLogic").GetComponent<CommonLogic>();

        WriteInfoText();
    }

    public void WriteInfoText()
    {
        if (score == 0)
        {
            GetComponent<TextMeshProUGUI>().text = allInfo.GetSceneInfo("ventSetup");
        }
        else if (score >= 15 && score < 20)
        {
            GetComponent<TextMeshProUGUI>().text = allInfo.GetSceneInfo("ventSetup1");
        }
        else if (score >= 20)
        {
            GetComponent<TextMeshProUGUI>().text = allInfo.GetSceneInfo("ventSetup2");
            countDownTimer.isPaused = true;
            StaticData.levelScores[3] = score;
            commonLogic.WaitChangeScene(8.0f, "TheHub");
        }
    }

    public void UpdateScore(int value)
    {
            score += value;
            scoreText.text = score.ToString();
    }
}
