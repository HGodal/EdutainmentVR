using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.SceneManagement;

public class SaveScore : MonoBehaviour
{
    private string[] hei;
    TextMeshProUGUI writtenName;
    string[] listedUser;

    void Start()
    {
        listedUser = new string[2];
        writtenName = GameObject.Find("/ScoreCanvas/Panel/NameText").GetComponent<TextMeshProUGUI>();
    }

    public void lagScore()
    {
        string[,] scores = ScoreLogic.stringToList(PlayerPrefs.GetString("HighScore"));

        string[] tempUser = new string[2] { writtenName.text, StaticData.getScore().ToString() };
        for (int i = 0; i < scores.GetLength(0); i++)
        {
            listedUser[0] = scores[i, 0];
            listedUser[1] = scores[i, 1];

            if (listedUser[1].Equals(""))
            {
                scores[i, 0] = tempUser[0];
                scores[i, 1] = tempUser[1];

                tempUser[0] = listedUser[0];
                tempUser[1] = listedUser[1];
            }
            else
            {
                if (Int16.Parse(tempUser[1]) > Int16.Parse(listedUser[1]))
                {
                    scores[i, 0] = tempUser[0];
                    scores[i, 1] = tempUser[1];

                    tempUser[0] = listedUser[0];
                    tempUser[1] = listedUser[1];
                }
            }

        }

        PlayerPrefs.SetString("HighScore", ScoreLogic.listToString(scores));
        StaticData.resetScores();
        SceneManager.LoadScene("Menu", LoadSceneMode.Single);
    }
}
