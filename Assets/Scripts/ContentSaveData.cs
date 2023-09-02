using System;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class ContentSaveData : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private TextMeshProUGUI Title;
    [SerializeField] private TextMeshProUGUI Content;
    //[SerializeField] private TextMeshProUGUI RightAnswer;
    //[SerializeField] private Dropdown dropdown;
    private List<Texture2D> imageTexture=new List<Texture2D>();

    public void SaveIntoJson()
    {
        Debug.Log("hi");
        string jsonPath = Application.dataPath + "/QuestionData.json";
        ContentDataWrapper contentDataWrapper = new ContentDataWrapper();
        if (File.Exists(jsonPath))
        {
            string existingJson = File.ReadAllText(jsonPath);
            contentDataWrapper = JsonUtility.FromJson<ContentDataWrapper>(existingJson);
        }
        QuestionData contentData = new QuestionData();
        contentData.ContentText = Content.text;
        contentData.Title = Title.text;
        contentData.QuestionType = "Content";
        getImage();
        if (ValidateContent())
        {
            //storing data
            
            for (int i = 0; i < imageTexture.Count; i++)
            {
                string imagePath = SaveImageToFile(i);
                contentData.ContentImages.Add(imagePath); // Store the image path instead of byte array(**)
            }
           
            contentData.ContentText = Content.text;
            contentData.Title = Title.text;
            //contentData.RightAnswer = RightAnswer.text;
            //foreach (var item in dropdown.options) // store the answers
            //{
            //    questionData.Answers.Add(item.text);
            //}

            //storing in json
            contentDataWrapper.questionDataList.Add(contentData);
            string updatedJson = JsonUtility.ToJson(contentDataWrapper);
            File.WriteAllText(jsonPath, updatedJson);
        }

    }
    private void getImage()
    {
        GameObject[] imageObjects = GameObject.FindGameObjectsWithTag("image");
        int i = 0;
        foreach (GameObject imageObject in imageObjects)
        {
            
            Image image = imageObject.GetComponent<Image>();
            if (image != null)
            {
                Debug.Log(i);
                imageTexture.Add(image.sprite.texture);
                Debug.LogError("Found image: ");
                i++;
            }
            else
                Debug.LogError("Image not found");
        }
    }
    public bool ValidateContent()
    {
        
        if (Content.text.Length > 0 && Title.text.Length > 0 && imageTexture != null)
        {
            Debug.Log("no");
            return true;
        }

        else
        {
            //Debug.Log("no");
            return false;
        }
    }
    private string SaveImageToFile(int i)
    {
        if (imageTexture != null)
        {
            string imagePath = "Assets" + "/SavedImages/" + DateTime.Now.Ticks + ".png";
            File.WriteAllBytes(imagePath, imageTexture[i].EncodeToPNG());
            return imagePath;
        }
        return null;
    }

    public class ContentDataWrapper
    {
        public List<QuestionData> questionDataList = new List<QuestionData>();
    }

}
