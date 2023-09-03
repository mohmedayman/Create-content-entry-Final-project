using UnityEngine;
using System.IO;
using Newtonsoft.Json;
using System.Collections.Generic;
using UnityEngine.SocialPlatforms.Impl;
using TMPro;
using Unity.VisualScripting;
using System;

public class JSONReader : MonoBehaviour
{
    public GameObject ContentWithoutImage;
    public GameObject ContentWith1Image;
    public GameObject ContentWith2Image;
    public GameObject ContentWith3Image;
    public GameObject ScorePanel;
    public GameObject Complete;
    public GameObject MCQ;
    public GameObject Sequence;

    private string jsonFilePath = Application.dataPath + "/QuestionData.json";
    QuestionDataWrapper questionDataWrapper = new QuestionDataWrapper();
    int i = 0;
    void Start()
    {
        string jsonString = File.ReadAllText(jsonFilePath);
        questionDataWrapper = JsonConvert.DeserializeObject<QuestionDataWrapper>(jsonString);
        questionDataWrapper.questionDataList.Sort((x, y) => {
            int titleComparison = string.Compare(x.Title, y.Title, StringComparison.OrdinalIgnoreCase);

            if (titleComparison == 0)  // If the question titles are equal
            {
                // Sort by question type
                return string.Compare(x.QuestionType, y.QuestionType, StringComparison.OrdinalIgnoreCase);
            }

            return titleComparison;
        });
        string sortedJsonString= JsonUtility.ToJson(questionDataWrapper);
        File.WriteAllText(jsonFilePath, sortedJsonString);
        
        
    }
    public void  GetNextSlide()
    {
        //Debug.Log(questionDataWrapper.questionDataList[i].QuestionType);
        //Debug.Log(i);
        if (i >= questionDataWrapper.questionDataList.Count)
        {
            Debug.Log("Finished");
            
            ContentWithoutImage.SetActive(false);
            ContentWith1Image.SetActive(false);
            ContentWith2Image.SetActive(false);
            ContentWith3Image.SetActive(false);
            Complete.SetActive(false);
            Sequence.SetActive(false);
            MCQ.SetActive(false);
            ScorePanel.SetActive(true);
        }
        else
        {
            //Debug.Log(i +" "+ questionDataWrapper.questionDataList.Count);
            string QType = questionDataWrapper.questionDataList[i].QuestionType;
            int NumberOfImages = questionDataWrapper.questionDataList[i].ContentImages.Count;
            switch (QType)
            {
                case "Content":
                    MCQ.SetActive(false);
                    if (NumberOfImages == 0)
                    {
                        ContentWithoutImage.SetActive(true);
                        ContentWith1Image.SetActive(false);
                        ContentWith2Image.SetActive(false);
                        ContentWith3Image.SetActive(false);
                        ContentWithoutImage.GetComponent<ContentReader>().GetContent(questionDataWrapper.questionDataList[i]);
                    }
                    else if (NumberOfImages == 1)
                    {
                        ContentWithoutImage.SetActive(true);
                        ContentWithoutImage.SetActive(false);
                        ContentWith1Image.SetActive(true);
                        ContentWith2Image.SetActive(false);
                        ContentWith3Image.SetActive(false);
                        ContentWith1Image.GetComponent<ContentReader>().GetContent(questionDataWrapper.questionDataList[i]);
                    }
                    else if (NumberOfImages == 2)
                    {
                        ContentWithoutImage.SetActive(true);
                        ContentWithoutImage.SetActive(false);
                        ContentWith1Image.SetActive(false);
                        ContentWith2Image.SetActive(true);
                        ContentWith3Image.SetActive(false);
                        ContentWith2Image.GetComponent<ContentReader>().GetContent(questionDataWrapper.questionDataList[i]);
                    }
                    else
                    {
                        ContentWithoutImage.SetActive(true);
                        ContentWithoutImage.SetActive(false);
                        ContentWith1Image.SetActive(false);
                        ContentWith2Image.SetActive(false);
                        ContentWith3Image.SetActive(true);
                        ContentWith3Image.GetComponent<ContentReader>().GetContent(questionDataWrapper.questionDataList[i]);
                    }
                    Complete.SetActive(false);
                    Sequence.SetActive(false);
                    MCQ.SetActive(false);

                    break;
                case "Fill the blank":
                    MCQ.SetActive(false);
                    ContentWithoutImage.SetActive(false);
                    ContentWith1Image.SetActive(false);
                    ContentWith2Image.SetActive(false);
                    ContentWith3Image.SetActive(false);
                    Sequence.SetActive(false);
                    MCQ.SetActive(false);
                    Complete.SetActive(true);           
                    Complete.GetComponent<CompleteJSONReader>().GetQuestion(questionDataWrapper.questionDataList[i]);
                    break;
                case "MCQ":
                    ContentWithoutImage.SetActive(false);
                    ContentWith1Image.SetActive(false);
                    ContentWith2Image.SetActive(false);
                    ContentWith3Image.SetActive(false);
                    Sequence.SetActive(false);
                    MCQ.SetActive(true);
                    MCQ.GetComponent<MultibleChoiceReadingManger>().GetQuestion(questionDataWrapper.questionDataList[i]);
                    break;
                case "SequenceChoice":
                    MCQ.SetActive(false);
                    ContentWithoutImage.SetActive(false);
                    ContentWith1Image.SetActive(false);
                    ContentWith2Image.SetActive(false);
                    ContentWith3Image.SetActive(false);
                    Sequence.SetActive(true);
                    Sequence.GetComponent<SequenceReadingManger>().GetQuestion(questionDataWrapper.questionDataList[i]);
                    break;

                default:
                    break;
            }
        }
        i++;
    }
    
    public class QuestionDataWrapper
    {
        public List<QuestionData> questionDataList = new List<QuestionData>();
    }
}
