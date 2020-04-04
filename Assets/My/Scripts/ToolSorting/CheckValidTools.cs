using TMPro;
using UnityEngine;

public class CheckValidTools : MonoBehaviour
{
    private DisplayText[] displayText;
    private CommonLogic commonLogic;
    private Countdown countDownTimer;
    private AudioSource[] sounds;

    private int counter;
    private bool isActive;
    public int numOfCorrectItems;

    private void Start()
    {
        displayText = GameObject.Find("/RoomsAndVR/Logic/DisplayTextLogic").GetComponents<DisplayText>();
        commonLogic = GameObject.Find("/RoomsAndVR/Logic/CommonLogic").GetComponent<CommonLogic>();
        countDownTimer = GameObject.Find("/RoomsAndVR/Logic/CountdownLogic").GetComponent<Countdown>();
        sounds = GetComponents<AudioSource>();

        counter = 0;
        isActive = true;

        string startString = "Finn alle verktøyene som hører til rørlegger-yrket \n" +
            "og legg dem på bordet til høyre.\n\n" +
            "Scoren kan du se på monitoren oppe til høyre.\n\n" +
            "+1 poeng for riktig verktøy.\n" +
            "-1 poeng for feil verktøy.\n\n\n" +
            "Det finnes " + numOfCorrectItems + " riktige verktøy,\n" +
            "se om du klarer å finne alle før tiden går ut!";

        displayText[0].OverwriteText(startString);
        displayText[1].OverwriteText(counter.ToString());
    }

    public void Change(Collider other, bool entered)
    {
        if (isActive)
        {
            if (other.gameObject.tag == "Valid")
            {
                if (entered)
                {
                    counter++;
                    sounds[0].Play();
                }
                else
                {
                    counter--;
                }
            }
            else if (other.gameObject.tag == "Invalid")
            {
                if (entered)
                {
                    counter--;
                    sounds[1].Play();
                }
                else
                {
                    counter++;
                }
            }
            CheckScore();
        }
    }

    private void CheckScore()
    {
        displayText[1].OverwriteText(counter.ToString());

        if (counter >= numOfCorrectItems)
        {
            sounds[2].Play();
            displayText[0].OverwriteText("Gratulerer! Du fant alle verktøyene!\n\n Du blir teleportert tilbake til menyen om 5 sekunder.");
            countDownTimer.isPaused = true;
            StaticData.levelScores[0] = Mathf.FloorToInt(countDownTimer.GetTimer());
            commonLogic.WaitChangeScene(5.0f, "TheHub");
            isActive = false;
        }
    }
}
