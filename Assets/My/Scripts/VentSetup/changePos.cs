using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class changePos : MonoBehaviour
{
    ScoreView progress;
    
    private GameObject vent1;
    Vector3 startPos;
    
    //float speed = 10;
    //float tall = 0.15f;

    private void Start()
    {
        vent1 = GameObject.Find("/Vent1");
        startPos = new Vector3(-2.34f, 0.15f, -2.82f);
        progress = GameObject.Find("/InfoCanvas/InfoText").GetComponent<ScoreView>();
         
    }

    public void ChangeVent()
    {
        
            vent1.transform.position = new Vector3(-2.317f, 2.202f, -2.82f);
            progress.UpdateScore(5);
            progress.WriteInfoText();
        
            
 
    }

   


}
