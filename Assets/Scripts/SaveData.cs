using System;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using static UnityEditor.Progress;
using UnityEngine.SceneManagement;

public class SaveData : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private TextMeshProUGUI Title;
    [SerializeField] private TextMeshProUGUI Quesion;
    [SerializeField] private TextMeshProUGUI RightAnswer;
    private Texture2D imageTexture;

    public void SaveIntoJson()
    {
        string jsonPath = Application.dataPath + "/QuestionData.json";
        QuestionDataWrapper questionDataWrapper = new QuestionDataWrapper();
        if (File.Exists(jsonPath))
        {
            string existingJson = File.ReadAllText(jsonPath);
            questionDataWrapper = JsonUtility.FromJson<QuestionDataWrapper>(existingJson);
        }

        getImage();
        if (ValidateQuestion())
        {
            QuestionData questionData = new QuestionData();
            questionData.QuestionText = Quesion.text;
            questionData.Title = Title.text;
            questionData.QuestionType = "MCQ";
            //storing data
            string imagePath = SaveImageToFile();
            questionData.Image = imagePath; // Store the image path instead of byte array(**)
            questionData.QuestionText = Quesion.text;
            questionData.Title = Title.text;
            questionData.RightAnswer = RightAnswer.text;
            GameObject[] answerObjects = GameObject.FindGameObjectsWithTag("answer");
            foreach (GameObject obj in answerObjects)
            {
                Transform childOfAnswer = obj.transform.GetChild(1);
                TextMeshProUGUI textMesh = childOfAnswer.GetComponent<TextMeshProUGUI>();
                questionData.Answers.Add(textMesh.text);
            }

            //storing in json
            questionDataWrapper.questionDataList.Add(questionData);
            string updatedJson = JsonUtility.ToJson(questionDataWrapper);
            File.WriteAllText(jsonPath, updatedJson);
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

    }
    private void getImage()
    {
        GameObject[] imageObjects = GameObject.FindGameObjectsWithTag("image");
        foreach (GameObject imageObject in imageObjects)
        {
            Image image = imageObject.GetComponent<Image>();
            if (image != null)
            {
                try
                {
                    imageTexture = image.sprite.texture;
                }
                catch (Exception e)
                {
                    Debug.Log(e.ToString());
                }

            }
        }
    }
    public bool ValidateQuestion()
    {
        if(Quesion.text.Length > 0 &&  Title.text.Length > 0)
        {
            GameObject[] answerObjects = GameObject.FindGameObjectsWithTag("answer");
            foreach (GameObject obj in answerObjects)
            {
                
                Transform childOfAnswer = obj.transform.GetChild(1);
                TextMeshProUGUI textMesh = childOfAnswer.GetComponent<TextMeshProUGUI>();
                if (RightAnswer.text.Equals(textMesh.text))
                    return true;
            }
            return false;
        }
        else
            return false;
    }
    private string SaveImageToFile()
    {
        if (imageTexture != null)
        {
            string imagePath = "Assets" + "/SavedImages/" + DateTime.Now.Ticks + ".png";
            File.WriteAllBytes(imagePath, imageTexture.EncodeToPNG());
            return imagePath;
        }
        return null;
    }

    public class QuestionDataWrapper
    {
        public List<QuestionData> questionDataList = new List<QuestionData>();
    }

}
