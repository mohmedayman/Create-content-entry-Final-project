using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ContentReader : MonoBehaviour
{
    //private string jsonDataPath = Application.dataPath + "/QuestionData.json";
    //private Texture2D texture;
    [SerializeField] private List<Image> image;
    [SerializeField] private TextMeshProUGUI Title;
    [SerializeField] private TextMeshProUGUI Content;
    //[SerializeField] private TextMeshProUGUI RightAnswer;
    private Texture2D[] texture = new Texture2D[3];
    //QuestionDataWrapper questionDataWrapper = new QuestionDataWrapper();
    //QuestionData questionData = new QuestionData();
    void Start()
    {
        // Read the JSON file
        //if (File.Exists(jsonDataPath))
        //{
        //    string jsonString = File.ReadAllText(jsonDataPath);

        //    questionDataWrapper = JsonUtility.FromJson<QuestionDataWrapper>(jsonString);


        //}
        //GetContent();
    }
    public void GetContent(QuestionData questionData)
    {

        //questionData = questionDataWrapper.questionDataList[3];
        //Debug.Log(questionData.Image);
        Title.text = questionData.Title;
        Content.text = questionData.ContentText;

        for (int i = 0; i < image.Count; i++)
        {
            Debug.Log(questionData.ContentImages[i]);
            texture[i] = GetComponent<ImageLoader>().LoadImageFromFile(questionData.ContentImages[i]);
            if (texture != null)
            {
                Debug.Log("hi");
                image[i].sprite = Sprite.Create(texture[i], new Rect(0, 0, texture[i].width, texture[i].height), Vector2.one * 0.5f);
            }
        }


    }
    //public class QuestionDataWrapper
    //{
    //    public List<QuestionData> questionDataList = new List<QuestionData>();
    //}
}
