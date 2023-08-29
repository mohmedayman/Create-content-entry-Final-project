using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArrengingAnswers : MonoBehaviour
{
    [SerializeField] private Dropdown MainDropDown;
    [SerializeField] private Dropdown ArrangedDropDown;
    
    public void AddToArranged()
    {
        int selectedIndex = MainDropDown.value;
        string selectedText = MainDropDown.options[selectedIndex].text;
        Dropdown.OptionData newOption = new Dropdown.OptionData(selectedText);
        ArrangedDropDown.options.Add(newOption);
        ArrangedDropDown.RefreshShownValue();
    }
}
