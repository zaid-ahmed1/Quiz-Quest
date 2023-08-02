using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
public class QuestionSOMaker : MonoBehaviour
{
    string filePath = @"questions.csv";
    
    string question;
    string[] answers = new string[4];
    int correctAnswerIndex;
    private void Start() {
        readFromCSV();
    }
    
    void readFromCSV() {
        try {
            using (StreamReader reader = new StreamReader(filePath))
            {
                string line;
                while((line = reader.ReadLine()) != null) {
                    processLine(line);
                    CreateQuestionSO(question, answers, correctAnswerIndex);
                    Debug.Log(line);
                }
            }
        }
        catch{
            Debug.Log("Unable to read");
        }

    }

    void processLine(string line) {
        string[] values = line.Split(',');
        if (values.Length == 6) {
            string question = values[0];
            for (int i = 0; i < answers.Length; i++) {
                answers[i] = values[i + 1];
            correctAnswerIndex = int.Parse(values[5]);
            }
        } 
    }

    void CreateQuestionSO(string questionText, string[] answers, int correctAnswerIndex) {
            QuestionSO questionSO = ScriptableObject.CreateInstance<QuestionSO>();
            questionSO.SetQuestion(questionText);
            questionSO.SetOptions(answers);
            questionSO.SetCorrectAnswerIndex(correctAnswerIndex);
            
            string path = "Assets/Questions/" + questionText + ".asset";
            AssetDatabase.CreateAsset(questionSO, path);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
    }
}
