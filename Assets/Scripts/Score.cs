using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Score : MonoBehaviour
{
    public int score = 0;
    [SerializeField] private TextMeshProUGUI ScoreTxt;
    // Start is called before the first frame update
     public void ChangeScore()
    {
        score+=50;
        ScoreTxt.text = score.ToString();
    }
}
