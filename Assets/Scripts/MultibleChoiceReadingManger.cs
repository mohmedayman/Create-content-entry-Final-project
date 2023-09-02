using System;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class MultibleChoiceReadingManger : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI quetsionText;
    [SerializeField] private TextMeshProUGUI TitleText;
    [SerializeField] private UnityEngine.UI.Button AnswerField;
    [SerializeField] private GameObject canvas;
    [SerializeField] private GameObject ImagePlace;
    public GameObject ScorePanel;
    private int XPos ;
    private int YPos = -50;
    private List<string> Answers;
    private List<string> TempAnswers;
    private string RightAnswer;
    private UnityAction<string> action;
    public void GetQuestion(QuestionData questionData)
    {
        quetsionText.text = questionData.QuestionText;
        TitleText.text = questionData.Title;
        Answers = questionData.Answers;
        TempAnswers = new List<string>(questionData.Answers);
        RightAnswer = questionData.RightAnswer;
        Texture2D texture = LoadTextureFromFile(questionData.Image);
        Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.zero);
        ImagePlace.GetComponent<Image>().sprite = sprite;
        GenerateAnswers();
    }
    public void GenerateAnswers()
    {
        for (int i = 0; i < Answers.Count; i++)
        {
            if(i%2==0)
                XPos = 0;
            else
                XPos = 1600;
            UnityEngine.UI.Button answerFieldInstance = Instantiate(AnswerField, new Vector3(XPos, YPos, 0), Quaternion.identity);
            Transform childOfAnswerField = answerFieldInstance.transform.GetChild(0);
            TextMeshProUGUI textMesh = childOfAnswerField.GetComponent<TextMeshProUGUI>();
            int RandomAnswer = UnityEngine.Random.Range(0, TempAnswers.Count);
            textMesh.text = TempAnswers[RandomAnswer];
            TempAnswers.RemoveAt(RandomAnswer);
            answerFieldInstance.transform.SetParent(canvas.transform, false);
            answerFieldInstance.name = i.ToString();
            action += ScoreController;
            answerFieldInstance.onClick.AddListener(() => ScoreController(answerFieldInstance.name));
            if (i%2!=0)
                YPos -= 280;
        }
    }
    // Load a Texture2D from a file path
    private Texture2D LoadTextureFromFile(string path)
    {
        Texture2D texture = null;

        if (System.IO.File.Exists(path))
        {
            byte[] fileData = System.IO.File.ReadAllBytes(path);
            texture = new Texture2D(2, 2);
            texture.LoadImage(fileData);
        }

        return texture;
    }
    public bool isRightAnswer(string text)
    {
        if(text.Equals(RightAnswer))
            return true;
        return false;
    }
    public void ScoreController(string name)
    {
        GameObject clickedButton = GameObject.Find(name);
        Transform childOfButton = clickedButton.transform.GetChild(0);
        TextMeshProUGUI textMesh = childOfButton.GetComponent<TextMeshProUGUI>();
        if (isRightAnswer(textMesh.text))
            ScorePanel.GetComponent<Score>().ChangeScore();
    }
}
