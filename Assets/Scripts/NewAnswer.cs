using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NewAnswer : MonoBehaviour
{
    [SerializeField] private Dropdown dropdown;
    [SerializeField] private TextMeshProUGUI answer;
    public void AddNewAnswer()
    {
        Dropdown.OptionData newOption = new Dropdown.OptionData(answer.text);
        dropdown.options.Add(newOption);
        dropdown.RefreshShownValue();
    }
}
