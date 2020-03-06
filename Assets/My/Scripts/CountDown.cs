using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class CountDown : MonoBehaviour
{
    public TextMeshProUGUI countdownText;
    public float timerDiff = 20.0f;
    float timer;
    public bool countUp = false;
    float minutes;
    float seconds;
    float oldSeconds;
    string correctTime;
    public bool isPaused = false;

    AudioSource tickSound;
    AudioSource coachWhistle;

    private GameObject logic;

    private void Start()
    {
        logic = GameObject.Find("Logic");

        oldSeconds = 0;
        tickSound = GetComponents<AudioSource>()[0];
        coachWhistle = GetComponents<AudioSource>()[1];

        //countdownText = GameObject.Find("TimerCanvas").GetComponent<TextMeshProUGUI>();

        if (countUp)
        {
            timer = 0.0f;
        }
        else
        {
            timer = timerDiff;
        }
    }

    private void Update()
    {
        if (!isPaused)
        {
            DoCounting();
        }
    }

    public float getTimer(){
        return timer;
    }

    private void DoCounting()
    {
        if (countUp)
        {
            timer += Time.deltaTime;
        }
        else
        {
            timer -= Time.deltaTime;
        }

        minutes = Mathf.FloorToInt(timer / 60F);
        seconds = Mathf.FloorToInt(timer - minutes * 60);

        if (seconds != oldSeconds)
        {
            tickSound.Play();
            oldSeconds = seconds;
        }

        correctTime = string.Format("{0:00}:{1:00}", minutes, seconds);
        countdownText.text = "<mspace=6>" + correctTime + "</mspace>";

        // change to something else!!
        if (timer <= 0.5f || timer > timerDiff + 1)
        {
            isPaused = true;
            coachWhistle.Play();
            logic.GetComponent<CheckValidTools>().writeInfo("Tiden er ute! Bedre lykke neste gang!\n\n\nDu vil bli teleportert tilbake til menyen om 5 sekund.");
            logic.GetComponent<CheckValidTools>().enabled = false;

            logic.GetComponent<CommonLogic>().WaitChangeScene(5.0f, "Menu");
        }
    }
}
