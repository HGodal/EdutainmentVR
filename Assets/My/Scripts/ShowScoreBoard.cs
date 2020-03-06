using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class ShowScoreBoard : MonoBehaviour
{
    public GameObject scorePanel;
    private int line;
    private int row;

    // Start is called before the first frame update

    public void ResetTopList()
    {
        PlayerPrefs.SetString("HighScore", "|,|,|,|,|,|,|,|,|,|,");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex, LoadSceneMode.Single);
    }

    void Awake()
    {
        if (PlayerPrefs.GetString("HighScore").Equals(""))
        {
            PlayerPrefs.SetString("HighScore", "|,|,|,|,|,|,|,|,|,|,");
        }

        string[,] scoreTest = ScoreLogic.stringToList(PlayerPrefs.GetString("HighScore"));

        for (int i = 0; i < 10; i++)
        {
            line = i % 5;
            row = Mathf.FloorToInt(i / 5);

            GameObject newPanel = Instantiate(scorePanel);
            newPanel.transform.SetParent(GameObject.Find("/HighScoreCanvas/HighScorePanel").transform);
            newPanel.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = (i + 1).ToString() + ". " + scoreTest[i, 0] + " " + scoreTest[i, 1];

            RectTransform rect = newPanel.GetComponent<RectTransform>();

            newPanel.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);

            rect.anchoredPosition3D = new Vector3(0, 0, 0);

            //Left
            rect.offsetMin = new Vector2(2 + 93 * row, rect.offsetMin.y);
            //Right
            rect.offsetMax = new Vector2(-95 + 93 * row, rect.offsetMax.y);
            //Top
            rect.offsetMax = new Vector2(rect.offsetMax.x, -13 - 17 * line);
            //Bottom
            rect.offsetMin = new Vector2(rect.offsetMin.x, 70 - 17 * line);

            rect.Rotate(new Vector3(0, 90, 0));
        }
    }
}
