using UnityEngine;
using TMPro;

public class DisplayText : MonoBehaviour
{
    public TextMeshProUGUI textObject;

    public void OverwriteText(string newText)
    {
        textObject.text = newText;
    }

    public void AppendText(string newText)
    {
        textObject.text += newText;
    }

    public string GetText()
    {
        return textObject.text;
    }
}
