using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using TMPro;

public class QuizManager : MonoBehaviour
{
    public GenerateJsonInfo allInfo;

    public TextMeshProUGUI questionText;
    TextMeshProUGUI scoreText;

    List<string> informationText;

    DisplayText teksten;
    CommonLogic logic;

    private int step;
    private int score;

    AudioSource[] sounds;

    void Start()
    {
        step = -1;
        scoreText = GameObject.Find("/TVset/ScoreCanvas/ScoreCounter").GetComponent<TextMeshProUGUI>();
        score = 0;

        logic = GameObject.Find("/RoomsAndVR/Logic/CommonLogic").GetComponent<CommonLogic>();
        teksten = GameObject.Find("/RoomsAndVR/Logic/DisplayTextLogic").GetComponent<DisplayText>();
        informationText = allInfo.GetSceneInfoList("quizList");

        sounds = GetComponents<AudioSource>();
    }

    private void Update()
    {
        if (step == 45)
        {
            GetComponent<TextMeshProUGUI>().text = allInfo.GetSceneInfo("quiz");
            StaticData.levelScores[6] = score;
            logic.WaitChangeScene(5.0f, "TheHub");
        }
    }

    public void NextQuestion()
    {
        step++;
        teksten.OverwriteText(informationText.ElementAt(step));
        step++;
    }

    public void ShowWhy()
    {
        teksten.OverwriteText(informationText.ElementAt(step));
    }

    public void UserSelectTrue()
    {
        for (int i = 1; i < 100; i += 3)
        {
            if (i == step)
            {
                if (informationText.ElementAt(step) == "True")
                {
                    UpdateScore(1);
                    sounds[0].Play();
                }
                else
                {
                    UpdateScore(0);
                    sounds[1].Play();
                }

                step++;
                ShowWhy();
            }
        }
    }

    public void UserSelectFalse()
    {
        for (int i = 1; i < 100; i += 3)
        {
            if (i == step)
            {
                if (informationText.ElementAt(step) == "False")
                {
                    UpdateScore(1);
                    sounds[0].Play();
                }
                else
                {
                    UpdateScore(0);
                    sounds[1].Play();
                }

                step++;
                ShowWhy();
            }
        }
    }

    public void UpdateScore(int value)
    {
        score += value;
        scoreText.text = score.ToString();
    }
}
