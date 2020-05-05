using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class BoltAction : MonoBehaviour
{
   
   
    private GameObject ventil;

    private float speed = 0.02f;
    

    float posY = 0f;

    private SluttVent sluttvent;

    int number;
    private bool unscrewed = false;
    public GameObject screwPrefab;

    

    ScoreCounter progress;
    private GameObject skrue0;
    

    private GameObject collision;
   

    private void Start()
    {
        
        //bolt = GameObject.Find("/Bolts/Screw");
        sluttvent = GameObject.Find("/VentilLogikk").GetComponent<SluttVent>();

        progress = GameObject.Find("/InfoCanvas/InfoText").GetComponent<ScoreCounter>();
        collision = GameObject.Find("/Screw0/collision");
        
        number = 0;   

        makeNewScrew();
        //skrue2 = GameObject.Find("Screw2");
        skrue0 = GameObject.Find("Screw0");
        //skrue1 = GameObject.Find("Screw1");
  
    }

   

    public void makeNewScrew()
    {
        for (int i = 0; i < 1; i++)
        {
            
            GameObject newScrew = Instantiate(screwPrefab) as GameObject;
            newScrew.transform.parent = GameObject.Find("/Bolts").transform;
            //posY vil ikke endre seg, alle skruene kommer på samme plass
            newScrew.transform.position = new Vector3(0.3127f, 0.9896f - posY, -2.2345f);
            newScrew.transform.rotation = Quaternion.Euler(0, 0, 0);
            newScrew.AddComponent<Screw>();

            //make new name for each screw that is made
            newScrew.gameObject.name = "Screw" + (number + i);
            newScrew.gameObject.tag = "Untagged";

            newScrew.gameObject.AddComponent<Rigidbody>();
            newScrew.gameObject.GetComponent<Rigidbody>().useGravity = false;

            newScrew.gameObject.AddComponent<BoxCollider>();
            newScrew.gameObject.GetComponent<BoxCollider>().isTrigger = true;
            
            //legger på posY, men ser ikke ut til å vike 
            posY += 0.4f;
 
        }
       
    }

    public void Screws()
    {
        //check if the screw is out
        if (unscrewed == true)
        {
            return;
        }

        skrue0.transform.Translate(new Vector3(0f, 0f, speed * Time.deltaTime));
        skrue0.transform.Rotate(new Vector3(0f, 0f, 90 * Time.deltaTime));
        /*
        skrue1.transform.Translate(new Vector3(0f, 0f, speed * Time.deltaTime));
        skrue1.transform.Rotate(new Vector3(0f, 0f, 90 * Time.deltaTime));

        skrue2.transform.Translate(new Vector3(0f, 0f, speed * Time.deltaTime));
        skrue2.transform.Rotate(new Vector3(0f, 0f, 90 * Time.deltaTime));
        */
        

    }

    IEnumerator Coroutine(float time)
    {
        yield return new WaitForSeconds(time);
    }

    public void Unscrewed()
    {
        StartCoroutine(Coroutine(10));

        //making changes to the screw when it is out 
        skrue0.gameObject.GetComponent<Rigidbody>().useGravity = true;
        skrue0.gameObject.GetComponent<BoxCollider>().isTrigger = false;
        /*
        skrue1.gameObject.GetComponent<Rigidbody>().useGravity = true;
        skrue1.gameObject.GetComponent<BoxCollider>().isTrigger = false;

        skrue2.gameObject.GetComponent<Rigidbody>().useGravity = true;
        skrue2.gameObject.GetComponent<BoxCollider>().isTrigger = false;
        */
        unscrewed = true;
        Destroy(skrue0);

        progress.UpdateScore(1);

        progress.WriteInfoText();
       
    }







}