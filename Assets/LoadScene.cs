using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(NextScene(4));
    }

    // Update is called once per frame
    private IEnumerator NextScene(int duration)
    {
        yield return new WaitForSeconds(duration);
        SceneManager.LoadScene("VerktoySortering", LoadSceneMode.Single);
    }
}
