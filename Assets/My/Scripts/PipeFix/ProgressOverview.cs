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

    private void Start()
    {
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
            GetComponent<TextMeshProUGUI>().text = "Bak veggen på venstre side er det noen rør som lekker, \n" +
                "       og du må fikse dem! \n\n" +
                brokenPipes + " rør gjenstår! \n\n" +
                "Bruk hammeren til å slå ned segmenter av veggen, og rørtangen \n" +
                "        for å fikse de røde, ødelagte rørene. \n\n" +
                "+10 poeng for å fikse et rør. \n" +
                "-1 poeng for å ødelegge en bit av veggen.";
        }
        else
        {
            victory.Play();
            finished = true;
            StaticData.levelScores[3] = score;
            GetComponent<TextMeshProUGUI>().text = "Gratulerer! \n\n" +
                "Alle de ødelagte rørene er fikset, og vannet renner som det skal. \n\n" +
                "Du vil bli teleportert tilbake til menyen om 5 sekund.";
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