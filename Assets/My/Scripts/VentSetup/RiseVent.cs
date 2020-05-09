using System.Collections;
using UnityEngine;

public class RiseVent : MonoBehaviour
{
    GameObject ventPart;
    ScoreView progress;
    bool isRaised;
    bool raising;
    public string gameObjectName;

    void Start()
    {
        isRaised = false;
        raising = false;
        ventPart = GameObject.Find("/" + gameObjectName);
        progress = GameObject.Find("/InfoCanvas/InfoText").GetComponent<ScoreView>();
    }

    public void MoveVentPart()
    {
        if (!isRaised)
        {
            if (!raising) StartCoroutine(Rise(new Vector3(0.0f, 2.03f, 0.0f), 1.0f));
            ventPart.transform.localPosition += new Vector3(0.0f, 2.03f, 0.0f);
            progress.UpdateScore(5);
            progress.WriteInfoText();
            isRaised = true;
        }
    }

    private IEnumerator Rise(Vector3 distance, float duration)
    {
        raising = true;
        Vector3 startPosition = ventPart.transform.localPosition;
        Vector3 endPosition = startPosition + distance;
        for (float t = 0; t < duration; t += Time.deltaTime)
        {
            ventPart.transform.localPosition = Vector3.Lerp(startPosition, endPosition, t / duration);
            yield return null;
        }

        raising = false;
    }
}
