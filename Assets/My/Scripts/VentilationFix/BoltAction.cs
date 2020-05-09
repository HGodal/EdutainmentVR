using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class BoltAction : MonoBehaviour
{
    private float speed = 0.02f;
    float posY = 0f;

    private SluttVent sluttvent;

    int number;
    private bool unscrewed = false;
    public GameObject screwPrefab;

    ScoreCounter progress;
    private GameObject skrue0;

    AudioSource audioSource;
    public AudioClip drilling;

    private void Start()
    {
        sluttvent = GameObject.Find("/VentilLogikk").GetComponent<SluttVent>();
        progress = GameObject.Find("/InfoCanvas/InfoText").GetComponent<ScoreCounter>();
        audioSource = GetComponent<AudioSource>();

        number = 0;   

        makeNewScrew();
 
        skrue0 = GameObject.Find("Screw0");
    }

   

    public void makeNewScrew()
    {
        for (int i = 0; i < 1; i++)
        {
            GameObject newScrew = Instantiate(screwPrefab) as GameObject;
            newScrew.transform.parent = GameObject.Find("/Bolts").transform;

            newScrew.transform.position = new Vector3(0.3347f, 0.863f - posY, -2.3685f);
            newScrew.transform.rotation = Quaternion.Euler(0, 0, 0);
            newScrew.AddComponent<Screw>();

            //make new name for each screw that is made
            newScrew.gameObject.name = "Screw" + (number + i);
            newScrew.gameObject.tag = "Untagged";

            newScrew.gameObject.AddComponent<Rigidbody>();
            newScrew.gameObject.GetComponent<Rigidbody>().useGravity = false;

            newScrew.gameObject.AddComponent<BoxCollider>();
            newScrew.gameObject.GetComponent<BoxCollider>().isTrigger = true;
            
            posY += 0.4f;
        }
       
    }

    public void Screws()
    {
        //check if the screw is out
        if (unscrewed)
        {
            return;
        }

        audioSource.PlayOneShot(drilling, 1.0F);
        skrue0.transform.Translate(new Vector3(0f, 0f, speed * Time.deltaTime));
        skrue0.transform.Rotate(new Vector3(0f, 0f, 90 * Time.deltaTime));

    }

    public void Unscrewed()
    {
        //making changes to the screw when it is out 
        skrue0.gameObject.GetComponent<Rigidbody>().useGravity = true;
        skrue0.gameObject.GetComponent<BoxCollider>().isTrigger = false;

        unscrewed = true;
        Destroy(skrue0);
        audioSource.Stop();

        progress.UpdateScore(2);
        progress.WriteInfoText();
       
    }

}