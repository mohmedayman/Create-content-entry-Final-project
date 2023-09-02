using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GoToDisplay : MonoBehaviour
{
    void Start()
    {
        Button back = GetComponent<Button>();
        back.onClick.AddListener(GoToDisplayMenu);
    }
    public void GoToDisplayMenu()
    {
        SceneManager.LoadScene("UI-CompleteDisplayWindowScene");
    }
}
