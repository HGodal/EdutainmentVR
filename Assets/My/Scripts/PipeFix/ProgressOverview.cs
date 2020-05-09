using UnityEngine;
using TMPro;

public class ProgressOverview : MonoBehaviour
{
    bool finished;
    int score;
    int brokenPipes;
    AudioSource victory;
    TextMeshProUGUI scoreText;
    CommonLogic commonLogic;
    GenerateJsonInfo allInfo;

    private void Start()
    {
        allInfo = GameObject.Find("/JsonInfo").GetComponent<GenerateJsonInfo>();
        finished = false;
        score = 0;
        victory = GetComponent<AudioSource>();
        scoreText = GameObject.Find("/TVset/ScoreCanvas/ScoreCounter").GetComponent<TextMeshProUGUI>();
        commonLogic = GameObject.Find("/CommonLogic").GetComponent<CommonLogic>();
    }

    public void WriteInfoText()
    {
        if (brokenPipes != 0)
        {
            GetComponent<TextMeshProUGUI>().text = string.Format(allInfo.GetSceneInfo("pipeFixer1"), brokenPipes);
        }
        else
        {
            victory.Play();
            finished = true;
            StaticData.levelScores[2] = score;
            GetComponent<TextMeshProUGUI>().text = allInfo.GetSceneInfo("pipeFixer2");
            commonLogic.WaitChangeScene(5.0f, "TheHub");
        }
        brokenPipes--;
    }

    public void SetBrokenPipes(int number)
    {
        brokenPipes = number;
        WriteInfoText();
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