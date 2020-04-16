using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class QuizManager : MonoBehaviour
{
    public Question[] questions;
    private static List<Question> unanswerdQuestions;

    private Question currentQuestion;

    
    public TextMeshProUGUI questionText;

    [SerializeField]
    private float timeBetweenQuestion = 1f;

    //private string gameDataFileName = "data.json";

    void Start()
    {
        if (unanswerdQuestions == null || unanswerdQuestions.Count == 0)
        {
            unanswerdQuestions = questions.ToList<Question>();
        }

        SetCurrentQuestion();
        //Debug.Log(currentQuestion.question + " is " + currentQuestion.isTrue);

        UserSelectFalse();
    }

    void SetCurrentQuestion()
    {
        int randomQuestionIndex = Random.Range(0, unanswerdQuestions.Count);
        currentQuestion = unanswerdQuestions[randomQuestionIndex];

        questionText.text = currentQuestion.question;

        
    }

    IEnumerator TransitionToNextQuestion ()
    {
        unanswerdQuestions.Remove(currentQuestion);

        yield return new WaitForSeconds(timeBetweenQuestion);

        //This will restart det scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void UserSelectTrue()
    {
        if (currentQuestion.isTrue)
        {
            Debug.Log("CORRECT");
        }
        else
        {
            Debug.Log("FALSE");
            
        }

        StartCoroutine(TransitionToNextQuestion());
    }

    public void UserSelectFalse()
    {
        if (!currentQuestion.isTrue) 
        {
            Debug.Log("CORRECT");
        }
        else
        {
            Debug.Log("FALSE");
            
        }

        StartCoroutine(TransitionToNextQuestion());
    }

    /*
    private void LoadGameData()
    {
        string filePath = Path.Combine(Application.streamingAssetsPath, gameDataFileName); 

        if (File.Exists(filePath))
        {
            string dataAsJson = File.ReadAllText(filePath);
            GameData loadedData 
        }
    }
    */
}
