using UnityEngine;

public class ChangeRotation : MonoBehaviour
{
    private GameObject close;
    private AudioSource correct;

    ScoreView progress;

    void Start()
    {
        close = GameObject.Find("/SluttVent");

        progress = GameObject.Find("/InfoCanvas/InfoText").GetComponent<ScoreView>();

    }

    public void ChangeRot()
    {
        close.transform.rotation = Quaternion.Euler(0, 90, 0);
        progress.UpdateScore(5);
        progress.WriteInfoText();
    }
}
