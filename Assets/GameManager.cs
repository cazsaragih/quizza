using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    public Text factText;
    public GameObject correctPanel;
    public GameObject wrongPanel;
    public Button trueButton;
    public Button falseButton;
    public GameObject scorePanel;
    public Text scoreText;

    public Question[] questions;
    private List<Question> unansweredQuestions;
    private Question currentQuestion;

    public float transitionTime = 1f;
    private int score = 0;

    void Start()
    {
        scorePanel.SetActive(false);
        LoadNextQuestion();
    }

    void LoadNextQuestion()
    {
        if (unansweredQuestions == null)
        {
            unansweredQuestions = new List<Question>(questions);
        }
        else if(unansweredQuestions.Count == 0)
        {
            ShowResult();
            return;
        }

        trueButton.interactable = true;
        falseButton.interactable = true;

        correctPanel.SetActive(false);
        wrongPanel.SetActive(false);
        factText.transform.parent.gameObject.SetActive(true);

        GenerateRandomQuestion();
    }

    void GenerateRandomQuestion()
    {
        int randomIndex = Random.Range(0, unansweredQuestions.Count);
        currentQuestion = unansweredQuestions[randomIndex];
        unansweredQuestions.Remove(currentQuestion);
        factText.text = currentQuestion.fact;
    }

    public void AnswerQuestion(bool value)
    {
        trueButton.interactable = false;
        falseButton.interactable = false;

        factText.transform.parent.gameObject.SetActive(false);
        if (currentQuestion.isTrue == value)
        {
            score++;
            correctPanel.SetActive(true);
        }
        else
        {
            wrongPanel.SetActive(true);
        }

        StartCoroutine(TransitionToNextQuestion());
    }

    IEnumerator TransitionToNextQuestion()
    {
        yield return new WaitForSeconds(transitionTime);

        LoadNextQuestion();
    }

    void ShowResult()
    {
        scorePanel.SetActive(true);
        scoreText.text = "You scored " + score + " out of " + questions.Length;
    }
}
