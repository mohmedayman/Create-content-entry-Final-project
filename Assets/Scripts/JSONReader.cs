using UnityEngine;
using System.IO;
using Newtonsoft.Json;
using System.Collections.Generic;
using UnityEngine.SocialPlatforms.Impl;
using TMPro;
using Unity.VisualScripting;
using System;
using System.Collections;

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
    int SlideIndex = 0;
    public TextMeshProUGUI CompletetimerText;
    public TextMeshProUGUI MCQtimerText;
    public TextMeshProUGUI SequencetimerText;
    private Coroutine timerCoroutine;
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
        SlideIndex = PlayerPrefs.GetInt("LastSlideID", 0);
        
    }
    public void  GetNextSlide()
    {
        if (timerCoroutine != null)
        {
            StopCoroutine(timerCoroutine);
            timerCoroutine = null;
        }
        //Debug.Log(questionDataWrapper.questionDataList[i].QuestionType);
        //Debug.Log(i);
        if (SlideIndex >= questionDataWrapper.questionDataList.Count)
        {
            //SlideIndex = 0;
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
            string QType = questionDataWrapper.questionDataList[SlideIndex].QuestionType;
            int NumberOfImages = questionDataWrapper.questionDataList[SlideIndex].ContentImages.Count;
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
                        ContentWithoutImage.GetComponent<ContentReader>().GetContent(questionDataWrapper.questionDataList[SlideIndex]);
                    }
                    else if (NumberOfImages == 1)
                    {
                        ContentWithoutImage.SetActive(true);
                        ContentWithoutImage.SetActive(false);
                        ContentWith1Image.SetActive(true);
                        ContentWith2Image.SetActive(false);
                        ContentWith3Image.SetActive(false);
                        ContentWith1Image.GetComponent<ContentReader>().GetContent(questionDataWrapper.questionDataList[SlideIndex]);
                    }
                    else if (NumberOfImages == 2)
                    {
                        ContentWithoutImage.SetActive(true);
                        ContentWithoutImage.SetActive(false);
                        ContentWith1Image.SetActive(false);
                        ContentWith2Image.SetActive(true);
                        ContentWith3Image.SetActive(false);
                        ContentWith2Image.GetComponent<ContentReader>().GetContent(questionDataWrapper.questionDataList[SlideIndex]);
                    }
                    else
                    {
                        ContentWithoutImage.SetActive(true);
                        ContentWithoutImage.SetActive(false);
                        ContentWith1Image.SetActive(false);
                        ContentWith2Image.SetActive(false);
                        ContentWith3Image.SetActive(true);
                        ContentWith3Image.GetComponent<ContentReader>().GetContent(questionDataWrapper.questionDataList[SlideIndex]);
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
                    Complete.GetComponent<CompleteJSONReader>().GetQuestion(questionDataWrapper.questionDataList[SlideIndex]);
                    timerCoroutine = StartCoroutine(StartTimer(20f,CompletetimerText));
                    break;
                case "MCQ":
                    ContentWithoutImage.SetActive(false);
                    ContentWith1Image.SetActive(false);
                    ContentWith2Image.SetActive(false);
                    ContentWith3Image.SetActive(false);
                    Sequence.SetActive(false);
                    MCQ.SetActive(true);
                    MCQ.GetComponent<MultibleChoiceReadingManger>().GetQuestion(questionDataWrapper.questionDataList[SlideIndex]);
                    timerCoroutine = StartCoroutine(StartTimer(20f, MCQtimerText));
                    break;
                case "SequenceChoice":
                    MCQ.SetActive(false);
                    ContentWithoutImage.SetActive(false);
                    ContentWith1Image.SetActive(false);
                    ContentWith2Image.SetActive(false);
                    ContentWith3Image.SetActive(false);
                    Sequence.SetActive(true);
                    Sequence.GetComponent<SequenceReadingManger>().GetQuestion(questionDataWrapper.questionDataList[SlideIndex]);
                    timerCoroutine = StartCoroutine(StartTimer(20f, SequencetimerText));
                    break;

                default:
                    break;
            }
        }
        PlayerPrefs.SetInt("LastSlideID", SlideIndex);
        SlideIndex++;
        
    }
    IEnumerator StartTimer(float duration, TextMeshProUGUI timerText)
    {
        float timer = duration;

        while (timer > 0f)
        {
            // Update the timer text UI
            timerText.text = timer.ToString("F1");

            yield return new WaitForSeconds(0.1f); // Update the timer every 0.1 seconds

            timer -= 0.1f;
        }

        GetNextSlide();
    }
    public void Reset()
    {
        SlideIndex = 0;
        PlayerPrefs.SetInt("LastSlideID", SlideIndex);
    }

    public class QuestionDataWrapper
    {
        public List<QuestionData> questionDataList = new List<QuestionData>();
    }
}
