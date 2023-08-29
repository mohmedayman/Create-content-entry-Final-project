using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SaveAnswers : MonoBehaviour
{
    [SerializeField] private Button SaveCurrentAnswers;
    [SerializeField] private Button AddNewAnswer;
    [SerializeField] private Button RemoveThisAnswer;
    [SerializeField] private Button AddThisAnswer;
    [SerializeField] private Button SaveQuestion;
    [SerializeField] private Button RemoveFromArrangedList;
    [SerializeField] private GameObject NewAnswerField;
    [SerializeField] private Dropdown MainDropDown;
    [SerializeField] private Dropdown ArrangedDropDown;
    
    public void SaveAnswersSequence()
    {
        if(MainDropDown.options.Count >=2)
        {
            SaveCurrentAnswers.gameObject.SetActive(false);
            AddNewAnswer.gameObject.SetActive(false);
            RemoveThisAnswer.gameObject.SetActive(false);
            AddThisAnswer.gameObject.SetActive(true);
            ArrangedDropDown.gameObject.SetActive(true);
            SaveQuestion.gameObject.SetActive(true);
            NewAnswerField.SetActive(false);
            RemoveFromArrangedList.gameObject.SetActive(true);
        }

    }
}
