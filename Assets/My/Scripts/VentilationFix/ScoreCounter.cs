using UnityEngine;
using TMPro;

public class ScoreCounter : MonoBehaviour
{
    bool finished;
    int score;
    
    AudioSource victory;
    TextMeshProUGUI scoreText;
    CommonLogic commonLogic;
    public GenerateJsonInfo allInfo;

    private void Start()
    {
        Debug.Log(allInfo.GetSceneInfo("ventFixer1"));
        finished = false;
        score = 0;
        victory = GetComponent<AudioSource>();
        scoreText = GameObject.Find("/TVset/ScoreCanvas/ScoreCounter").GetComponent<TextMeshProUGUI>();
        commonLogic = GameObject.Find("/RoomsAndVR/Logic/CommonLogic").GetComponent<CommonLogic>();

        WriteInfoText();
    }

    public void WriteInfoText()
    {
        if (score == 0)
        {
            GetComponent<TextMeshProUGUI>().text = allInfo.GetSceneInfo("ventFixer1");
        }
        else if (score == 3)
        {
            GetComponent<TextMeshProUGUI>().text = allInfo.GetSceneInfo("ventFixer2");
        }
        
        else if (score >= 4 && score < 10)
        {
            GetComponent<TextMeshProUGUI>().text = allInfo.GetSceneInfo("ventFixer3");
        }
        else if (score >= 10)
        {
            GetComponent<TextMeshProUGUI>().text = allInfo.GetSceneInfo("ventFixer4");
            StaticData.levelScores[4] = score;
            commonLogic.WaitChangeScene(5.0f, "TheHub");
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