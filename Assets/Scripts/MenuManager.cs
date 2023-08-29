using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MenuManager : MonoBehaviour
{
    //public GameObject menu_panel;
    //public GameObject levels_panel;

    //public void ShowQuestions()
    //{
    //    levels_panel.SetActive(true);
    //    menu_panel.SetActive(false);
    //}
    //public void ShowMenu()
    //{
    //    levels_panel.SetActive(false);
    //    menu_panel.SetActive(true);
    //}
    public void MultipleChoise()
    {
        SceneManager.LoadScene("UI-MCQInputWindowScene");
    }
    public void FillTheBlank()
    {
        SceneManager.LoadScene("UI-CompleteInputWindowScene");
    }
    public void Sequnce()
    {
        SceneManager.LoadScene("UI-SequenceInputWindowScene");
    }
    public void ContentWithImage()
    {
        SceneManager.LoadScene("UI-InputWindowScene");
    }public void ContentWithImage2()
    {
        SceneManager.LoadScene("UI-InputWindowScene 1");
    }public void ContentWithImage3()
    {
        SceneManager.LoadScene("UI-InputWindowScene 2");
    }public void ContentWithOutImage()
    {
        SceneManager.LoadScene("UI-InputWindowScene 3");
    }
    public void Quit()
    {
        Application.Quit();
    }
}
