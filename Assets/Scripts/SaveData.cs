using System;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class SaveData : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private TextMeshProUGUI Title;
    [SerializeField] private TextMeshProUGUI Quesion;
    [SerializeField] private TextMeshProUGUI RightAnswer;
    [SerializeField] private Dropdown dropdown;
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
        QuestionData questionData = new QuestionData();
        questionData.QuestionText = Quesion.text;
        questionData.QuestionTitle = Title.text;
        getImage();
        if (ValidateQuestion())
        {
            //storing data
            string imagePath = SaveImageToFile();
            questionData.Image = imagePath; // Store the image path instead of byte array(**)
            questionData.QuestionText = Quesion.text;
            questionData.QuestionTitle = Title.text;
            questionData.RightAnswer = RightAnswer.text;
            foreach (var item in dropdown.options) // store the answers
            {
                questionData.Answers.Add(item.text);
            }

            //storing in json
            questionDataWrapper.questionDataList.Add(questionData);
            string updatedJson = JsonUtility.ToJson(questionDataWrapper);
            File.WriteAllText(jsonPath, updatedJson);
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
                imageTexture = image.sprite.texture;
                Debug.LogError("Found image: ");
            }
            else
                Debug.LogError("Image not found");
        }
    }
    public bool ValidateQuestion()
    {
        if(Quesion.text.Length > 0 &&  Title.text.Length > 0 && imageTexture!=null && dropdown.options.Count >= 2)
        {
            foreach (var item in dropdown.options)
            {
                if(RightAnswer.text.Equals(item.text))
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
