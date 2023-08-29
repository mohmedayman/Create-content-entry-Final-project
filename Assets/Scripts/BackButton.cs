using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BackButton : MonoBehaviour
{
    private void Start()
    {
        Button backButton = GetComponent<Button>();
        backButton.onClick.AddListener(LoadMCQScene);
    }

    private void LoadMCQScene()
    {
        SceneManager.LoadScene("New Scene");
    }
}


