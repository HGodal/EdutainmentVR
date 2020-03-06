using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ButtonListener : MonoBehaviour
{
    private TextMeshProUGUI nameText;

    void Start()
    {
        nameText = GameObject.Find("/ScoreCanvas/Panel/NameText").GetComponent<TextMeshProUGUI>();
    }

    public void writeLetter()
    {
        if (this.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text == "<--")
        {
            nameText.text = nameText.text.Remove(nameText.text.Length - 1);
        }
        else if (nameText.text.Length < 10)
        {
            nameText.text += this.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text;
        }
    }

}
