using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_ZuegeAnzeige : MonoBehaviour
{
    public Text text;

    public void SetText(string s)
    {
        text.text = s;
    }
}
