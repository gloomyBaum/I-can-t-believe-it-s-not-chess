using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScenarioSelectionDropdown : MonoBehaviour
{
    List<string> m_DropOptions = new List<string> { "Standard", "Chaos", "Apokalypse", "Festung", "See", "Schach", "DebugScenario"};

    Dropdown m_Dropdown;

    void Start()
    {
        //Fetch the Dropdown GameObject the script is attached to
        m_Dropdown = GetComponent<Dropdown>();
        //Clear the old options of the Dropdown menu
        m_Dropdown.ClearOptions();
        //Add the options created in the List above
        m_Dropdown.AddOptions(m_DropOptions);

        //Listener
        m_Dropdown.onValueChanged.AddListener(DropdownValueChanged);
    }

    private void DropdownValueChanged(int newPosition)
    {
        GameManager.Instance.ChangeScenario(m_DropOptions[newPosition]);
    }
}
