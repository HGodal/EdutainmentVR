using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CommonLogic : MonoBehaviour
{
    public void WaitChangeScene(float seconds, string name)
    {
        StartCoroutine(WaitForSeconds(seconds, name));
    }

    IEnumerator WaitForSeconds(float sec, string name)
    {
        yield return new WaitForSeconds(sec);
        SceneManager.LoadScene(name, LoadSceneMode.Single);
    }

    public void ChangeScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
    }
}
