using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BackToStartButton : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Button back=GetComponent<Button>();
        back.onClick.AddListener(GoToMenu);
    }
    public void GoToMenu()
    {
        SceneManager.LoadScene("StartScene");
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
