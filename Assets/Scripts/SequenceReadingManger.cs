using System;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SequenceReadingManger : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI quetsionText;
    [SerializeField] private TextMeshProUGUI TitleText;
    [SerializeField] private GameObject Img;
    [SerializeField] private GameObject AnswerField;
    [SerializeField] private GameObject CounterField;
    [SerializeField] private GameObject canvas;
    [SerializeField] private GameObject ImagePlace;
    public GameObject ScorePanel;
    private int XPos = -1760;
    private int YPos = -1000;
    private List<string> TempAnswers;
    private List<string> Answers;
    private void Start()
    {
    }
    public void GetQuestion(QuestionData questionData)
    {
        quetsionText.text = questionData.QuestionText;
        TitleText.text = questionData.Title;
        Answers = questionData.Answers;
        TempAnswers= new List<string>(questionData.Answers);
        Texture2D texture = LoadTextureFromFile(questionData.Image);
        Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.zero);
        ImagePlace.GetComponent<Image>().sprite = sprite;
        GenerateAnswers();
    }
    public void GenerateAnswers()
    {
            //quetsionText.text = loadedDataList[currentQuestion].QuestionText;
            for (int i = 0; i < Answers.Count; i++)
            {
                for (int j = 0; j < 2; j++)
                {
                    if (j == 0)
                    {
                        GameObject counterFieldInstance = Instantiate(CounterField, new Vector3(XPos, YPos, 0), Quaternion.identity);
                        Transform childOfCounter = counterFieldInstance.transform.GetChild(0);
                        TextMeshProUGUI textMesh = childOfCounter.GetComponent<TextMeshProUGUI>();
                        textMesh.text = (i+1).ToString();
                        counterFieldInstance.transform.SetParent(canvas.transform, false);
                        counterFieldInstance.tag = "counter";
                    }
                    else
                    {
                        XPos += 1000;
                        GameObject answerFieldInstance = Instantiate(AnswerField, new Vector3(XPos, YPos, 0), Quaternion.identity);
                        Transform childOfAnswerField = answerFieldInstance.transform.GetChild(0);
                        TextMeshProUGUI textMesh = childOfAnswerField.GetComponent<TextMeshProUGUI>();
                        int RandomAnswer = UnityEngine.Random.Range(0, TempAnswers.Count);
                        textMesh.text = TempAnswers[RandomAnswer];
                        TempAnswers.RemoveAt(RandomAnswer);
                        YPos += 220;
                        XPos -= 1000;
                        answerFieldInstance.transform.SetParent(canvas.transform, false);
                        answerFieldInstance.name = i.ToString();
                    }
                }
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
    public bool isRightAnswer()
    {
        GameObject[] answerObjects = GameObject.FindGameObjectsWithTag("answer");
        foreach (GameObject obj in answerObjects)
        {
            Transform childOfAnswer= obj.transform.GetChild(0);
            TextMeshProUGUI textMesh = childOfAnswer.GetComponent<TextMeshProUGUI>();
            if (!Answers[Int32.Parse(obj.name)].Equals(textMesh.text)) {
                Debug.LogError(Answers[Int32.Parse(obj.name)] + " " + textMesh.text);
                return false;
            }
        }
        return true;
    }
    public void ScoreController()
    {
        if (isRightAnswer())
            ScorePanel.GetComponent<Score>().ChangeScore();
    }
}
