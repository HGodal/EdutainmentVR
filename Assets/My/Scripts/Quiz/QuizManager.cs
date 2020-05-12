using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using TMPro;

public class QuizManager : MonoBehaviour
{
    GenerateJsonInfo allInfo;
    TextMeshProUGUI scoreText;
    List<string> informationText;
    DisplayText teksten;
    CommonLogic logic;
    int step;
    int score;
    AudioSource[] sounds;
    bool waitingForAnswer;

    void Start()
    {
        allInfo = GameObject.Find("/JsonLogic").GetComponent<GenerateJsonInfo>();
        scoreText = GameObject.Find("/TVset/ScoreCanvas/ScoreCounter").GetComponent<TextMeshProUGUI>();
        logic = GameObject.Find("/RoomsAndVR/Logic/CommonLogic").GetComponent<CommonLogic>();
        teksten = GameObject.Find("/RoomsAndVR/Logic/DisplayTextLogic").GetComponent<DisplayText>();
        informationText = allInfo.GetSceneInfoList("quizList");

        step = -3;
        score = 0;

        sounds = GetComponents<AudioSource>();
        waitingForAnswer = false;
        GetComponent<TextMeshProUGUI>().text = allInfo.GetSceneInfo("quiz1");
    }

    public void UserGuess(bool choice)
    {
        if (waitingForAnswer)
        {
            if (choice == bool.Parse(informationText[step + 1]))
            {
                UpdateScore(5);
                sounds[0].Play();
            }
            else
            {
                sounds[1].Play();
            }
            teksten.OverwriteText(informationText.ElementAt(step + 2));
            waitingForAnswer = false;
        }
    }

    public void NextQuestion()
    {
        if (!waitingForAnswer)
        {
            step += 3;
            if (step == informationText.Count)
            {
                GetComponent<TextMeshProUGUI>().text = allInfo.GetSceneInfo("quiz2");
                StaticData.levelScores[6] = score;
                sounds[2].Play();
                logic.WaitChangeScene(5.0f, "TheHub");
            }
            else if (step < informationText.Count)
            {
                waitingForAnswer = true;
                teksten.OverwriteText(informationText.ElementAt(step));
            }
        }
    }

    public void UpdateScore(int value)
    {
        score += value;
        scoreText.text = score.ToString();
    }
}
