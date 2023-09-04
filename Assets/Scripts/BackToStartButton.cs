using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BackToStartButton : MonoBehaviour
{
    // Start is called before the first frame update
    public Button back ;
    private string SceneName= "StartScene";
    void Start()
    {
         
        
    }
    public void GoToMenu()
    {
        SceneManager.LoadScene(SceneName);
    }
    // Update is called once per frame
    private void OnEnable()
    {
        back.onClick.AddListener(GoToMenu);
    }
    private void OnDisable()
    {
        back.onClick.RemoveAllListeners();
    }
}
