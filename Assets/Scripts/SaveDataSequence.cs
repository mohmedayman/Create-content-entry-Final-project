using System;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class SaveDataSequence : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private TextMeshProUGUI Title;
    [SerializeField] private TextMeshProUGUI Quesion;
    [SerializeField] private Dropdown dropdown;
    [SerializeField] private Dropdown Maindropdown;

    private Texture2D imageTexture;
    private List<string> DropdownOptions;
    private List<string> MainDropdownOptions;
    void Start()
    {
        // Get the options from both dropdowns
        DropdownOptions = new List<string>();
        foreach (Dropdown.OptionData option in dropdown.options)
        {
            DropdownOptions.Add(option.text);
        }

        MainDropdownOptions = new List<string>();
        foreach (Dropdown.OptionData option in Maindropdown.options)
        {
            MainDropdownOptions.Add(option.text);
        }
    }
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
            //storing data
            string imagePath = SaveImageToFile();
            questionData.Image = imagePath; // Store the image path instead of byte array
            questionData.QuestionText = Quesion.text;
            questionData.Title = Title.text;
            questionData.RightAnswer = "";
            questionData.QuestionType = "SequenceChoice";
            foreach (var item in dropdown.options) // store the answers
            {
                questionData.Answers.Add(item.text);
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
        if(Quesion.text.Length > 0 &&  Title.text.Length > 0 && dropdown.options.Count ==Maindropdown.options.Count && Maindropdown.options.Count == NewAnswer.NumOfAnswersFromDrop)
        {
            foreach (string option in DropdownOptions)
            {
                if (!MainDropdownOptions.Contains(option))
                {
                    return false;
                }
            }
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
