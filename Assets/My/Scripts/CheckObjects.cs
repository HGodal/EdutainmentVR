using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CheckObjects : MonoBehaviour
{

    TextMeshProUGUI infoGUI;
    TextMeshProUGUI counterGUI;
    int endScore;
    int counter;
    private GameObject close;
    private GameObject hinge;
    private GameObject Vent1;
    AudioSource Correct;
    AudioSource Ventilation;
    CountDown countDownTimer;
    CommonLogic logic;

    private void Start() {

        Ventilation = GetComponent<AudioSource>();
        endScore = 15;
        counter = 0;
        countDownTimer = GameObject.Find("ButtonVent/Button1/Front/CountDown").GetComponent<CountDown>();
        logic = GameObject.Find("Logic").GetComponent<CommonLogic>();

        infoGUI = GameObject.Find("/InfoCanvas/InfoText").GetComponent<TextMeshProUGUI>();
        string startString = "Ser ut som ventilasjonen ikke er helt ferdig satt sammen, \n" +
            "dette gjør at viften ikke går.\n\n" +
            "Trykk på knappene i spillet for å gjøre ventilasjonen komplett. \n\n" +
            "+5 poeng for riktig plassering \n" +
            "-5 poeng for feil plassering.\n\n\n" +
            "Det finnes 3 ting å rette på,\n" +
            "se om du klarer å fikse opp i det før tiden renner ut";
        WriteInfo(startString);

        counterGUI = GameObject.Find("/TVset/ScoreCanvas/ScoreCounter").GetComponent<TextMeshProUGUI>();
        WriteCounter(counter.ToString());
        checkValid();
        
    }

    public void WriteInfo(string text)
    {
        infoGUI.text = text;
    }

    public void WriteCounter(string text)
    {
        counterGUI.text = text;
    }

    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.tag == "Valid") {
            WriteCounter((counter+=5).ToString());
        }
    }

    public void checkValid() 
    {
        Correct = GetComponent<AudioSource>();

        hinge = GameObject.Find("/VentA");
        Vent1 = GameObject.Find("/Vent1");
        close = GameObject.Find("/SluttVent");
        

        Vector3 test = new Vector3(-1.6f, 2.28f, -2.7f);
        Vector3 test1 = new Vector3(-2.34f, 2.18f, -2.82f);
        

        if (close.transform.rotation == Quaternion.Euler(0, 90, 0)) {
           WriteCounter((counter+=5).ToString());
            if(Vent1.transform.position == test1) {
                WriteCounter((counter+=5).ToString());
                if(hinge.transform.position == test) {
                    WriteCounter((counter+=5).ToString());

                    }

                }
                checkScore();
            }
        }
        
    public void checkScore()
    {
        counterGUI.text = counter.ToString();
        if (counter >= endScore) {
            WriteInfo("Gratulerer du vant spillet! \n\n du blir teleportert tilbake til menyen om 5 sekunder");
            countDownTimer.isPaused = true;
            StaticData.levelScores[0] = Mathf.FloorToInt(countDownTimer.getTimer());
            logic.WaitChangeScene(5.0f, "Menu");
        }
    }
    }
    
   

