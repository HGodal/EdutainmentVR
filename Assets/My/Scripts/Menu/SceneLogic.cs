using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneLogic : MonoBehaviour
{
    public void RandomLevel()
    {
        int index = Random.Range(2, 8);
        SceneManager.LoadScene(index);
    }

    public void LoadScene(string desiredLevel)
    {
        SceneManager.LoadScene(desiredLevel, LoadSceneMode.Single);
    }

    public void ResetEveryThing()
    {
        StaticData.ResetScores();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex, LoadSceneMode.Single);
    }
}
