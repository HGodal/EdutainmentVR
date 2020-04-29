using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class changePos : MonoBehaviour
{
    ScoreView progress;
    
    private GameObject vent1;

    private void Start()
    {
        vent1 = GameObject.Find("/Vent1");
        progress = GameObject.Find("/InfoCanvas/InfoText").GetComponent<ScoreView>();
    }

    public void ChangeVent()
    {
        
            vent1.transform.position = new Vector3(-2.317f, 2.202f, -2.82f);
            progress.UpdateScore(5);
            progress.WriteInfoText();
        
            
 
    }

   


}
