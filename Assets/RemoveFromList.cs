using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RemoveFromList : MonoBehaviour
{
    [SerializeField] private Dropdown dropdown;
    public void RemoveFromDropList()
    {
        int selectedIndex = dropdown.value;
        if (selectedIndex >= 0 && selectedIndex < dropdown.options.Count)
        {
            dropdown.options.RemoveAt(selectedIndex);
            dropdown.RefreshShownValue();
        }
    }
}
