using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MenuManager : MonoBehaviour
{
    public const string UIMCQInputWindowScene = "UI-MCQInputWindowScene";
    public const string UICompleteInputWindowScene = "UI-CompleteInputWindowScene";
    public const string UISequenceInputWindowScene = "UI-SequenceInputWindowScene";
    public const string UIInputWindowScene = "UI-InputWindowScene";
    public const string UIInputWindowScene1 = "UI-InputWindowScene 1";
    public const string UIInputWindowScene2 = "UI-InputWindowScene 2";
    public const string UIInputWindowScene3 = "UI-InputWindowScene 3"; 
    public void MultipleChoise()
    {
        SceneManager.LoadScene(UIMCQInputWindowScene);
    }
    public void FillTheBlank()
    {
        SceneManager.LoadScene(UICompleteInputWindowScene);
    }
    public void Sequnce()
    {
        SceneManager.LoadScene(UISequenceInputWindowScene);
    }
    public void ContentWithImage()
    {
        SceneManager.LoadScene(UIInputWindowScene);
    }public void ContentWithImage2()
    {
        SceneManager.LoadScene(UIInputWindowScene1);
    }public void ContentWithImage3()
    {
        SceneManager.LoadScene(UIInputWindowScene2);
    }public void ContentWithOutImage()
    {
        SceneManager.LoadScene(UIInputWindowScene3);
    }
    public void Quit()
    {
        Application.Quit();
    }
    
}
