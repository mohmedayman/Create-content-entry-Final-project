using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BackButton : MonoBehaviour
{
    public Button backButton;
    private string SceneName = "New Scene";
    private void Start()
    {
       
    }

    private void LoadMCQScene()
    {
        SceneManager.LoadScene(SceneName);
    }
    private void OnEnable()
    {
        backButton.onClick.AddListener(LoadMCQScene);
    }
    private void OnDisable()
    {
        backButton.onClick.RemoveAllListeners();
    }
}


