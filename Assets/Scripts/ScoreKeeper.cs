using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreKeeper : MonoBehaviour
{

    int correctAnswers = 0;
    int questionsSeen = 0;

    private void Update() {
        
    }

    public int GetCorrectAnswers() {
        return correctAnswers;
    }

    public void IncrementCorrectAnswers() {
        correctAnswers++;
    }

    public int GetQuestionsSeen() {
        return questionsSeen;
    }

    public void IncrementQuestionsSeen() {
        questionsSeen++;
    }


    public string ShowScore() {
        return (correctAnswers.ToString() + "/" + questionsSeen.ToString());
    }

    public string ShowFinalScore() {
        return ((correctAnswers).ToString() + "/" + (questionsSeen-1).ToString());
    }

    public int GetIncorrectAnswers() {
        return questionsSeen - correctAnswers;
    }

}

