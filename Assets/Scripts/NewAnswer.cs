using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NewAnswer : MonoBehaviour
{
    [SerializeField] private Dropdown dropdown;
    [SerializeField] private TextMeshProUGUI answer;
    [SerializeField] private GameObject AnswerField;
    [SerializeField] private Button NumOfAnswers;
    [SerializeField] private Button AddNewQuestion;
    [SerializeField] private GameObject Canvas;
    [SerializeField] private GameObject RightAnswerField;
    private int XPos;
    private int YPos = -80;
    public void AddNewAnswer()
    {
        GenerateAnswers();
    }
    public void GenerateAnswers()
    {
        //quetsionText.text = loadedDataList[currentQuestion].QuestionText;
        for (int i = 0; i <= dropdown.value; i++)
        {
            if (i % 2 == 0)
                XPos = -870;
            else
                XPos = 900;
            GameObject answerFieldInstance = Instantiate(AnswerField, new Vector3(XPos, YPos, 0), Quaternion.identity);
            answerFieldInstance.transform.SetParent(Canvas.transform, false);
            if (i % 2 != 0)
                    YPos -= 210;
        }
        dropdown.gameObject.SetActive(false);
        AddNewQuestion.gameObject.SetActive(true);
        answer.gameObject.SetActive(true);
        RightAnswerField.gameObject.SetActive(true);
        NumOfAnswers.gameObject.SetActive(false);
    }
}
