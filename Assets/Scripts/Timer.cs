using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField] float timeToAnswer = 20f;
    [SerializeField] float timeToShowAnswer = 3.5f;
    float timerValue;
    public bool isAnsweringQuestion = false;
    public bool loadNextQuestion;
    public float fillFraction;

    void Update()
    {
        UpdateTimer();
    }

    public void CancelTimer() {
        timerValue = 0;
    }

    void UpdateTimer() {
        timerValue -= Time.deltaTime;

        if (isAnsweringQuestion) {
            if (timerValue > 0){
                fillFraction = timerValue / timeToAnswer;
            } 
            else {
                isAnsweringQuestion = false;
                timerValue = timeToShowAnswer;
            }
        }
        else {
            if (timerValue > 0){
                fillFraction = timerValue / timeToShowAnswer;
            } 
            else {
                isAnsweringQuestion = true;
                timerValue = timeToAnswer;
                loadNextQuestion = true;
            }
        }

    }
}

