using UnityEngine;
using TMPro;

public class TurnOnVent : MonoBehaviour
{
    private GameObject hinge;
    private GameObject Vent1;
    private GameObject close;
    TextMeshProUGUI counterGUI;
    AudioSource Ventilation;
    Countdown countDownTimer;
    CommonLogic logic;
    TextMeshProUGUI infoGUI;

    private void Start()
    {
        countDownTimer = GameObject.Find("ButtonVent/Button1/Front/CountDown").GetComponent<Countdown>();
        logic = GameObject.Find("Logic").GetComponent<CommonLogic>();
        infoGUI = GameObject.Find("/InfoCanvas/InfoText").GetComponent<TextMeshProUGUI>();

        hinge = GameObject.Find("/VentA");
        Vent1 = GameObject.Find("/Vent1");
        close = GameObject.Find("/SluttVent");

        Ventilation = GetComponent<AudioSource>();
    }

    public void WriteInfo(string text)
    {
        infoGUI.text = text;
    }

    private void OnTriggerEnter(Collider other)
    {
        Vector3 test = new Vector3(-1.6f, 2.28f, -2.7f);
        Vector3 test1 = new Vector3(-2.34f, 2.18f, -2.82f);
        //må sjekke om alt er på riktig plass før ventilasjon kan bli skrudd på
        if (close.transform.rotation == Quaternion.Euler(0, 90, 0))
        {
            if (Vent1.transform.position == test1)
            {
                if (hinge.transform.position == test)
                {
                    Ventilation.Play();
                    WriteInfo("Gratulerer du vant spillet! \n\n du blir teleportert tilbake til menyen om 8 sekunder");
                    countDownTimer.isPaused = true;
                    StaticData.levelScores[0] = Mathf.FloorToInt(countDownTimer.GetTimer());
                    logic.WaitChangeScene(8.0f, "Menu");
                }
            }
        }
    }
}
