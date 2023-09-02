using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CompleteJSONReader : MonoBehaviour
{
    //private string jsonDataPath = Application.dataPath + "/QuestionData.json";
    private Texture2D texture;
    [SerializeField] private Image image;
    [SerializeField] private TextMeshProUGUI Title;
    [SerializeField] private TextMeshProUGUI Quesion;
    [SerializeField] private TextMeshProUGUI Answer;
    public GameObject ScorePanel;
    //[SerializeField] private TextMeshProUGUI Score;
   // public int score=0;
    //public JSONReader reader;
    //QuestionDataWrapper questionDataWrapper = new QuestionDataWrapper();
    //QuestionData questionData = new QuestionData();
    private string answer;
    void Start()
    {
        // Read the JSON file
        //if (File.Exists(jsonDataPath))
        //{
        //    string jsonString = File.ReadAllText(jsonDataPath);

        //    questionDataWrapper = JsonUtility.FromJson<QuestionDataWrapper>(jsonString);


        //}
        //GetQuestion();
        //Score=""
    }
    public void GetQuestion(QuestionData questionData)
    {

       // questionData = questionDataWrapper.questionDataList[0];
        //Debug.Log(questionData.Image);
        Title.text = questionData.Title;
        Quesion.text = questionData.QuestionText;
        texture = GetComponent<ImageLoader>().LoadImageFromFile(questionData.Image);
        if (texture != null)
        {
            Debug.Log("hi");
            image.sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.one * 0.5f);
        }
        answer=questionData.RightAnswer;
    }
    public void CompareAnswer()
    {
        if (Answer.text.ToUpper() == answer.ToUpper())
        {
            ScorePanel.GetComponent<Score>().ChangeScore();
        }
    }
    
    //public class QuestionDataWrapper
    //{
    //    public List<QuestionData> questionDataList = new List<QuestionData>();
    //}
}
