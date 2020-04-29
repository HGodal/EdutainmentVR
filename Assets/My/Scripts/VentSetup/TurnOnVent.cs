using UnityEngine;
using TMPro;

public class TurnOnVent : MonoBehaviour
{
    private GameObject hinge;
    private GameObject vent1;
    private GameObject close;

    ScoreView progress;

    AudioSource ventilation;
    
    bool play;

    private void Start()
    {
        ventilation = GetComponent<AudioSource>();
        
        

        progress = GameObject.Find("/InfoCanvas/InfoText").GetComponent<ScoreView>();

        hinge = GameObject.Find("/VentA");
        vent1 = GameObject.Find("/Vent1");
        close = GameObject.Find("/SluttVent");
    }

    

    private void Update()
    {
        if (play == true)
        {
            ventilation.Play();
        }
        if (play == false)
        {
            ventilation.Stop();
        }
    }

    public void TurnOn()
    {
        Vector3 test = new Vector3(-1.6f, 2.28f, -2.7f);
        Vector3 test1 = new Vector3(-2.317f, 2.202f, -2.82f);
        //må sjekke om alt er på riktig plass før ventilasjon kan bli skrudd på
        if (close.transform.rotation == Quaternion.Euler(0, 90, 0))
        {
            if (vent1.transform.position == test1 && hinge.transform.position == test)
            {
                    ventilation.Play();
                    progress.UpdateScore(5);
                    progress.WriteInfoText(); 
                    
            }
        }
    }
}
