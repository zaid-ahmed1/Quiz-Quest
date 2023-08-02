using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;
using System.IO;
using System.Linq;

public class Quiz : MonoBehaviour
{

    [Header("Questions")]
    [SerializeField] TextMeshProUGUI questionText;
    List<QuestionSO> questions = new List<QuestionSO>();
    QuestionSO currentQuestion;

    [Header("Answers")]
    [SerializeField] GameObject[] answerButtons;
    int correctAnswerIndex;
    bool hasAnsweredEarly;

    [Header("Button Colours")]
    [SerializeField] Sprite defaultAnswerSprite;
    [SerializeField] Sprite correctAnswerSprite;
    [SerializeField] Sprite incorrectAnswerSprite;

    [Header("Timer")]
    [SerializeField] Image timerImage;
    Timer timer;
    Image buttonImage;

    [Header("Score")]
    ScoreKeeper scoreKeeper;
    [SerializeField] TextMeshProUGUI scoreText;
    

    void Start()
    {

        LoadQuestions();
        GetNextQuestion();

        scoreKeeper = FindObjectOfType<ScoreKeeper>();
        timer = FindObjectOfType<Timer>();
    }



    void Update()
    {
        timerImage.fillAmount = timer.fillFraction;
        if (timer.loadNextQuestion)
        {
            hasAnsweredEarly = false;
            scoreKeeper.IncrementQuestionsSeen();
            GetNextQuestion();
            timer.loadNextQuestion = false;
        }
        else if (!hasAnsweredEarly && !timer.isAnsweringQuestion)
        {
            hasAnsweredEarly = true;
            TimeOver();
        }
    }

    void TimeOver()
    {
        Debug.Log("Time up");
        DisplayAnswer(4); 
        SetButtonState(false);
        scoreText.text = "Score: " + scoreKeeper.ShowScore(); 
    }


    void LoadQuestions()
    {
        QuestionSO[] loadedQuestions = Resources.LoadAll<QuestionSO>("Questions");
        questions.AddRange(loadedQuestions);
    }

    void DisplayAnswer(int index) {
        Debug.Log("In display answer");
        Image incorrectButtonImage;
        if (index == 4) {
            correctAnswerIndex = currentQuestion.GetCorrectAnswerIndex();
            questionText.text = "The answer is: " + currentQuestion.GetAnswer(correctAnswerIndex);
            buttonImage = answerButtons[correctAnswerIndex].GetComponent<Image>();
            buttonImage.sprite = correctAnswerSprite;
        }
        else if (index != currentQuestion.GetCorrectAnswerIndex()) {
            correctAnswerIndex = currentQuestion.GetCorrectAnswerIndex();
            questionText.text = "The answer is: " + currentQuestion.GetAnswer(correctAnswerIndex);
            buttonImage = answerButtons[correctAnswerIndex].GetComponent<Image>();
            buttonImage.sprite = correctAnswerSprite;
            incorrectButtonImage = answerButtons[index].GetComponent<Image>();
            incorrectButtonImage.sprite = incorrectAnswerSprite;
        }
        else {
            questionText.text = "Correct!";
            buttonImage = answerButtons[index].GetComponent<Image>();
            buttonImage.sprite = correctAnswerSprite;
            scoreKeeper.IncrementCorrectAnswers();
        }
    }

    public void OnAnswerSelected(int index) {
        hasAnsweredEarly = true;
        DisplayAnswer(index);
        SetButtonState(false);
        timer.CancelTimer();
        scoreText.text = "Score: " + scoreKeeper.ShowScore();
    }

    void GetNextQuestion() {
        if (questions.Count > 0) {
            SetButtonState(true);
            SetDefaultButtonSprites();
            GetRandomQuestion();
            DisplayQuestion();
        }
    }

    void GetRandomQuestion()
    {
        int index = UnityEngine.Random.Range(0, questions.Count);
        currentQuestion = questions[index];
        if (questions.Contains(currentQuestion)) {
            questions.Remove(currentQuestion);
        }
    }

    void DisplayQuestion() {
        questionText.text = currentQuestion.GetQuestion();
        for (int i = 0; i < answerButtons.Length; i++) {
            TextMeshProUGUI buttonText = answerButtons[i].GetComponentInChildren<TextMeshProUGUI>();
            buttonText.text = currentQuestion.GetAnswer(i);
        }
    }

    void SetButtonState(bool state) {
        for (int i = 0; i < answerButtons.Length; i++) {
            Button button = answerButtons[i].GetComponent<Button>();
            button.interactable = state;
        }
    }
    void SetDefaultButtonSprites() {
        for (int i = 0; i < answerButtons.Length; i++) {
            buttonImage = answerButtons[i].GetComponent<Image>();
            buttonImage.sprite = defaultAnswerSprite;
        }
    }


    QuestionSO CreateQuestion(string questionText, string[] answers, int correctAnswerIndex) {
        QuestionSO newQuestion = ScriptableObject.CreateInstance<QuestionSO>();
        newQuestion.SetQuestion(questionText);
        newQuestion.SetOptions(answers);
        newQuestion.SetCorrectAnswerIndex(correctAnswerIndex);
        return newQuestion;
    }
}
