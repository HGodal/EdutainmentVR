using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CheckValidTools : MonoBehaviour
{
    AudioSource correctSound;
    AudioSource wrongSound;
    AudioSource finishSound;
    TextMeshProUGUI counterGUI;
    TextMeshProUGUI infoGUI;
    int counter;
    int numOfCorrectItems;
    bool isActive;
    CommonLogic logic;
    CountDown countDownTimer;


    private void Start()
    {
        logic = GameObject.Find("Logic").GetComponent<CommonLogic>();
        countDownTimer = GameObject.Find("Logic/CountDown").GetComponent<CountDown>();

        counter = 0;
        numOfCorrectItems = 7;
        isActive = true;

        correctSound = GetComponents<AudioSource>()[0];
        wrongSound = GetComponents<AudioSource>()[1];
        finishSound = GetComponents<AudioSource>()[2];

        counterGUI = GameObject.Find("/TVset/ScoreCanvas/ScoreCounter").GetComponent<TextMeshProUGUI>();
        writeCounter(counter.ToString());

        infoGUI = GameObject.Find("/InfoCanvas/InfoText").GetComponent<TextMeshProUGUI>();
        string startString = "Finn alle verktøyene som hører til rørlegger-yrket \n" +
            "og legg dem på bordet til høyre.\n\n" +
            "Scoren kan du se på monitoren oppe til høyre.\n\n" +
            "+1 poeng for riktig verktøy.\n" +
            "-1 poeng for feil verktøy.\n\n\n" +
            "Det finnes " + numOfCorrectItems + " riktige verktøy,\n" +
            "se om du klarer å finne alle før tiden går ut!";
        writeInfo(startString);
    }

    public void writeInfo(string text)
    {
        infoGUI.text = text;
    }

    public void writeCounter(string text)
    {
        counterGUI.text = text;
    }

    public void Enter(Collider other)
    {
        if (isActive)
        {
            if (other.gameObject.tag == "Valid")
            {
                writeCounter((counter++).ToString());
                correctSound.Play();
            }
            else if (other.gameObject.tag == "Invalid")
            {
                writeCounter((counter--).ToString());
                wrongSound.Play();
            }
            checkScore();
        }
    }

    public void Exit(Collider other)
    {
        if (isActive)
        {
            if (other.gameObject.tag == "Valid")
            {
                writeCounter((counter--).ToString());
            }
            else if (other.gameObject.tag == "Invalid")
            {
                writeCounter((counter++).ToString());
            }
            checkScore();
        }
    }

    private void checkScore()
    {
        counterGUI.text = counter.ToString();

        if (counter >= numOfCorrectItems)
        {
            finishSound.Play();
            writeInfo("Gratulerer! Du fant alle verktøyene!\n\n Du blir teleportert tilbake til menyen om 5 sekunder.");
            countDownTimer.isPaused = true;
            StaticData.levelScores[0] = Mathf.FloorToInt(countDownTimer.getTimer());
            logic.WaitChangeScene(5.0f, "Menu");
            isActive = false;
        }
    }
}
