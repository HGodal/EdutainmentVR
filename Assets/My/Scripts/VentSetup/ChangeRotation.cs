using UnityEngine;
using System.Collections;

public class ChangeRotation : MonoBehaviour
{
    GameObject ventPart;
    AudioSource correct;
    bool rotating;
    ScoreView progress;
    bool used;

    void Start()
    {
        rotating = false;
        used = false;
        ventPart = GameObject.Find("/SluttVent");
        progress = GameObject.Find("/InfoCanvas/InfoText").GetComponent<ScoreView>();
    }

    public void ChangeRot()
    {
        if (!used)
        {
            if (!rotating) StartCoroutine(Rotate(new Vector3(0, 90, 0), 1));
            progress.UpdateScore(5);
            progress.WriteInfoText();
            used = true;
        }
    }

    IEnumerator Rotate(Vector3 angles, float duration)
    {
        rotating = true;
        Quaternion startRotation = ventPart.transform.rotation;
        Quaternion endRotation = Quaternion.Euler(angles) * startRotation;
        for (float t = 0; t < duration; t += Time.deltaTime)
        {
            ventPart.transform.rotation = Quaternion.Lerp(startRotation, endRotation, t / duration);
            yield return null;
        }
        ventPart.transform.rotation = endRotation;
        rotating = false;
    }
}
