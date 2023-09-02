using System;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class CompleteSaveData : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private TextMeshProUGUI Title;
    [SerializeField] private TextMeshProUGUI Quesion;
    [SerializeField] private TextMeshProUGUI RightAnswer;
    //[SerializeField] private Dropdown dropdown;
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
        questionData.Title = Title.text;
        questionData.QuestionType = "Fill the blank";
        getImage();
        if (ValidateQuestion())
        {
            //storing data
            string imagePath = SaveImageToFile();
            questionData.Image = imagePath; // Store the image path instead of byte array(**)
            questionData.QuestionText = Quesion.text;
            questionData.Title = Title.text;
            questionData.RightAnswer = RightAnswer.text;
            

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
        if(Quesion.text.Length > 0 &&  Title.text.Length > 0 && imageTexture!=null )
        {
            
            return true;
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
