using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

public class QuestionSOMaker : MonoBehaviour
{
    string filePath = Path.Combine("Assets", "StreamingAssets", "questions.csv");
    int questionCount = 0;
    string question;
    string[] answers = new string[4];
    int correctAnswerIndex;

    private void Start()
    {
        readFromCSV();
        Debug.Log(filePath);
    }

    void readFromCSV()
    {
        try
        {
            using (StreamReader reader = new StreamReader(filePath))
            {
                string line;
                bool isFirstLine = true; // Flag to track the first line

                while ((line = reader.ReadLine()) != null)
                {
                    if (isFirstLine)
                    {
                        isFirstLine = false; // Set the flag to false for subsequent lines
                        continue; // Skip the first line
                    }

                    processLine(line);
                    CreateQuestionSO(question, answers, correctAnswerIndex);

                    Debug.Log(line);
                }
            }
        }
        catch (IOException ex)
        {
            Debug.Log("Unable to read: " + ex.Message);
        }
    }

    void processLine(string line)
    {
        string[] values = line.Split(',');
        if (values.Length == 6)
        {
            question = values[0];
            for (int i = 0; i < answers.Length; i++)
            {
                answers[i] = values[i + 1];
            }
            correctAnswerIndex = int.Parse(values[5]);
        }
    }

    void CreateQuestionSO(string questionText, string[] answers, int correctAnswerIndex)
    {
        QuestionSO questionSO = ScriptableObject.CreateInstance<QuestionSO>();
        string questionName = "Question" + questionCount.ToString() + ".asset";
        questionCount++;
        string path = Path.Combine("Assets", "Resources", "Questions", questionName);
        AssetDatabase.CreateAsset(questionSO, path);
        questionSO.SetQuestion(questionText);
        questionSO.SetOptions(answers);
        questionSO.SetCorrectAnswerIndex(correctAnswerIndex);
        AssetDatabase.SaveAssets();

        // Notify the Editor that the object has been modified and should be saved.
        EditorUtility.SetDirty(questionSO);
    }
}
