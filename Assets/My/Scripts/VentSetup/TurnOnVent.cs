using UnityEngine;

public class TurnOnVent : MonoBehaviour
{
    GameObject hinge;
    GameObject vent1;
    GameObject close;
    public ScoreView scoreView;
    ScoreView progress;
    AudioSource ventilation;

    void Start()
    {
        ventilation = GetComponent<AudioSource>();

        progress = GameObject.Find("/InfoCanvas/InfoText").GetComponent<ScoreView>();

        hinge = GameObject.Find("/VentA");
        vent1 = GameObject.Find("/Vent1");
        close = GameObject.Find("/SluttVent");
    }

    public void TurnOn()
    {
        if (scoreView.GetScore() == 15)
        {
            ventilation.Play();
            progress.UpdateScore(5);
            progress.WriteInfoText();
        }
    }
}
