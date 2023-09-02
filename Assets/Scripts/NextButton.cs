using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class NextButton : MonoBehaviour
{
    // Start is called before the first frame update
    //private GameObject gameObject;
    public JSONReader reader;
    private UnityAction action;
    void Start()
    {
        Button NextButton = GetComponent<Button>();
        //action += reader.GetNextSlide();
        NextButton.onClick.AddListener(LoadScene);
    }
    public void LoadScene()
    {
        //reader = new JSONReader();
        reader.GetNextSlide();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
