using System.Collections.Generic;

[System.Serializable]
public class QuestionData
{
    // Start is called before the first frame update
    public string QuestionTitle;
    public string QuestionText;
    public List<string> Answers = new List<string>();
    public string RightAnswer;
    public string Image;
    public string QuestionType;

}
