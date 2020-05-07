using UnityEngine;
using TMPro;

public class ShowScoreboard : MonoBehaviour
{
    public TextMeshProUGUI currentScore;
    public GameObject scorePanel;

    private int line;
    private int row;

    private void Start()
    {
        ShowCurrentScore();

        CheckValidFormat();

        string[,] score = ScoreLogic.StringToList(PlayerPrefs.GetString("HighScore"));

        for (int i = 0; i < 10; i++)
        {
            line = i % 5;
            row = Mathf.FloorToInt(i / 5);

            GameObject newPanel = Instantiate(scorePanel);
            newPanel.transform.SetParent(GameObject.Find("/Canvases/HighScoreCanvas/HighScorePanel").transform);
            newPanel.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = (i + 1).ToString() + ". " + score[i, 0] + " " + score[i, 1];
            newPanel.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);

            RectTransform rect = newPanel.GetComponent<RectTransform>();
            rect.anchoredPosition3D = new Vector3(0, 0, 0);
            rect.offsetMin = new Vector2(2 + 93 * row, rect.offsetMin.y);
            rect.offsetMax = new Vector2(-95 + 93 * row, rect.offsetMax.y);
            rect.offsetMax = new Vector2(rect.offsetMax.x, -13 - 17 * line);
            rect.offsetMin = new Vector2(rect.offsetMin.x, 70 - 17 * line);
            rect.Rotate(new Vector3(0, 90, 0));
        }
    }

    public void ShowCurrentScore()
    {
        currentScore.text = StaticData.GetScore().ToString();
    }

    private void CheckValidFormat()
    {
        if (PlayerPrefs.GetString("HighScore").Equals(""))
        {
            PlayerPrefs.SetString("HighScore", "|,|,|,|,|,|,|,|,|,|,");
        }
    }

    public void ResetTopList()
    {
        PlayerPrefs.SetString("HighScore", "|,|,|,|,|,|,|,|,|,|,");
        Start();
    }
}
