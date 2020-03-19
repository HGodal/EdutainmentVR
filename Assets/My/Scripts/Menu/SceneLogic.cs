using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLogic : MonoBehaviour
{
    public void randomLevel()
    {
        int index = Random.Range(1, 2);
        SceneManager.LoadScene(index);
    }

    public void resetEveryThing(){
        //Reset andre ting her også
        StaticData.resetScores();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex, LoadSceneMode.Single);
    }

}
