using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CreateAlphabet : MonoBehaviour
{
    private GameObject root;
    public Button letterButton;
    private string alphabet;
    private int line;
    private int row;
    public TextMeshProUGUI nameText;

    private void Start()
    {
        root = GameObject.Find("/Canvases/KeyboardCanvas/Panel");
        alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZÆØÅ0";

        for (int i = 0; i < alphabet.Length; i++)
        {
            row = i % 6;
            line = Mathf.FloorToInt(i / 6);

            Button newButton = Instantiate(letterButton);
            newButton.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = alphabet[i].ToString();
            newButton.transform.SetParent(root.transform);
            newButton.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
            newButton.GetComponent<RectTransform>().anchoredPosition3D = new Vector3(-100 + (40 * row), 42 - (17 * line), -1);

            if (i == 29)
            {
                newButton.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "<--";
                newButton.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 36);
                newButton.GetComponent<RectTransform>().anchoredPosition3D = new Vector3(-100 + (40 * row), 42 - (17 * (line + 0.52f)), -1);
            }
        }
    }
}
