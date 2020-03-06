using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSelector : MonoBehaviour
{
    public void SelectSceneSingle(string selectedScene)
    {
        SceneManager.LoadScene(selectedScene, LoadSceneMode.Single);
    }

    public void SelectSceneAdditive(string selectedScene)
    {
        SceneManager.LoadScene(selectedScene, LoadSceneMode.Additive);
    }
}
