using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class EndScreen : MonoBehaviour
{

    [SerializeField] TextMeshProUGUI finalScoreText;
    ScoreKeeper scoreKeeper;
    public bool win;
    void Awake()
    {
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
        win = true;
    }

    void Update() {
        if (scoreKeeper.GetIncorrectAnswers() > 1) {
            win = false;
        }
    }

    public void ShowFinalScore() {
        if (win) {
            finalScoreText.text = "Congratulations!\nYour score is " + scoreKeeper.ShowFinalScore();
        }
        else {
            finalScoreText.text = "Better luck next time!\nYour score is " + scoreKeeper.ShowFinalScore();
        }
    }
}
