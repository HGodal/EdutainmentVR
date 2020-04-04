using UnityEngine;
using TMPro;
using System;
using UnityEngine.SceneManagement;

public class SaveScore : MonoBehaviour
{
    private string[] hei;
    private TextMeshProUGUI writtenName;
    private string[] listedUser;

    void Start()
    {
        listedUser = new string[2];
        writtenName = GameObject.Find("/Canvases/ScoreCanvas/Panel/NameText").GetComponent<TextMeshProUGUI>();
    }

    public void CreateScore()
    {
        string[,] scores = ScoreLogic.StringToList(PlayerPrefs.GetString("HighScore"));

        string[] tempUser = new string[2] { writtenName.text, StaticData.GetScore().ToString() };
        for (int i = 0; i < scores.GetLength(0); i++)
        {
            listedUser[0] = scores[i, 0];
            listedUser[1] = scores[i, 1];

            if (listedUser[1].Equals("") || Int16.Parse(tempUser[1]) > Int16.Parse(listedUser[1]))
            {
                scores[i, 0] = tempUser[0];
                scores[i, 1] = tempUser[1];

                tempUser[0] = listedUser[0];
                tempUser[1] = listedUser[1];
            }
        }

        PlayerPrefs.SetString("HighScore", ScoreLogic.ListToString(scores));
        StaticData.ResetScores();
        SceneManager.LoadScene("TheHub", LoadSceneMode.Single);
    }
}
