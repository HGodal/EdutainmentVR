using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ventil : MonoBehaviour
{
    SluttVent sluttevent;
    BoltAction bolts;
   
    ScoreCounter progress;
    public Material red;

    void Start()
    {
        sluttevent = GameObject.Find("/VentilLogikk").GetComponent<SluttVent>();
        gameObject.AddComponent<Rigidbody>().useGravity = false;
        gameObject.AddComponent<BoxCollider>().isTrigger = true;

        progress = GameObject.Find("/InfoCanvas/InfoText").GetComponent<ScoreCounter>();

        bolts = GameObject.Find("/Bolts/SkrueLogikk").GetComponent<BoltAction>();

    }

    void OnTriggerExit(Collider other)
    {
        //check if the screw is out
        if (other.gameObject.tag == "Screw")
        { 
            bolts.Unscrewed();
            Free();
        }
    }

    //makes new vent when player touch with pipewrench
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "PipeWrench")
        {
            Destroy(gameObject);
            sluttevent.makeNewVent();
        }
    }

    public void Free()
    {
        gameObject.GetComponent<Rigidbody>().useGravity = true;
        gameObject.GetComponent<BoxCollider>().isTrigger = false;

        gameObject.GetComponent<Renderer>().material = red;

        progress.WriteInfoText();
    }

}
