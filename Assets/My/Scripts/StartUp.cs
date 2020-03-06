using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartUp : MonoBehaviour
{

    public List<GameObject> pauseItems;
    public List<GameObject> gameItems;

    private void Awake()
    {
        foreach (GameObject item in pauseItems)
        {
            item.SetActive(true);
        }

        foreach (GameObject item in gameItems)
        {
            item.SetActive(false);
        }

    }
}
