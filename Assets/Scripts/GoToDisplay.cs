using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GoToDisplay : MonoBehaviour
{
    
    public Button back;
    private string SceneName = "UI-CompleteDisplayWindowScene";
    void Start()
    {
        
    }
    public void GoToDisplayMenu()
    {
        
        SceneManager.LoadScene(SceneName);
    }
    private void OnEnable()
    {
        back.onClick.AddListener(GoToDisplayMenu);
    }
    private void OnDisable()
    {
        back.onClick.RemoveAllListeners();
    }
    public void TryAgain()
    {
        PlayerPrefs.SetInt("LastSlideID", 0);
    }
}
