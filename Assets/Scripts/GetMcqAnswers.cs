using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GetMcqAnswers : MonoBehaviour
{
    [SerializeField] private Dropdown dropdown;
    [SerializeField] private Button SubmitAnswersButton;
    [SerializeField] private Button SaveQuestion;
    [SerializeField] private Dropdown ArrangedDropDown;
    [SerializeField] private Button AddToArranged;
    [SerializeField] private Button RemoveFromArranged;
    [SerializeField] private TextMeshProUGUI Instructions;

    public void AddToDropDown()
    {
        SubmitAnswersButton.gameObject.SetActive(false);
        dropdown.gameObject.SetActive(true);
        SaveQuestion.gameObject.SetActive(true);
        ArrangedDropDown.gameObject.SetActive(true) ;
        AddToArranged.gameObject.SetActive(true) ;
        RemoveFromArranged.gameObject.SetActive(true) ;
        Instructions.gameObject.SetActive(true) ;
        GameObject[] answerObjects = GameObject.FindGameObjectsWithTag("answer");
        List<string> options = new List<string>();

        foreach (GameObject obj in answerObjects)
        {
            Transform childOfAnswer = obj.transform.GetChild(1);
            TextMeshProUGUI textMesh = childOfAnswer.GetComponent<TextMeshProUGUI>();
            options.Add(textMesh.text);
        }
        dropdown.AddOptions(options);
        DestroyAnswerrFields();
    }
    public void DestroyAnswerrFields()
    {
        GameObject[] answerObjects = GameObject.FindGameObjectsWithTag("answer");
        List<string> options = new List<string>();

        foreach (GameObject obj in answerObjects)
        {
            Destroy(obj);
        }
    }
}





