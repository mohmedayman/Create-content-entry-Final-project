using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GoToDisplay : MonoBehaviour
{
    //public JSONReader jreader;
    void Start()
    {
        Button back = GetComponent<Button>();
        back.onClick.AddListener(GoToDisplayMenu);
    }
    public void GoToDisplayMenu()
    {
        //jreader.Reset();
        //PlayerPrefs.SetInt("LastSlideID", 0);
        SceneManager.LoadScene("UI-CompleteDisplayWindowScene");
    }
    public void TryAgain()
    {
        PlayerPrefs.SetInt("LastSlideID", 0);
    }
}
