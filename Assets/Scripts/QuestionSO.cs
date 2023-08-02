using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestionSO : ScriptableObject
{
    [TextArea(2,5)]
    [SerializeField] string question = "Enter new question text here";
    [SerializeField] string[] answers = new string[4];
    [SerializeField] int correctAnswerIndex;

    public void SetQuestion(string question) {
        this.question = question;
    }

    public string GetQuestion() {
        return question;
    }

    public void SetCorrectAnswerIndex(int index) {
        this.correctAnswerIndex = index;
    }

    public int GetCorrectAnswerIndex() {
        return correctAnswerIndex;
    }

    public void SetOptions(string[] options) {
        for (int i = 0; i < options.Length; i++) {
            answers[i] = options[i];
        }
    }

    public string GetAnswer(int index) {
        return answers[index];
    }
}
