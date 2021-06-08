using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogAnzeige : MonoBehaviour
{
    public Dialog dialog;

    public void DialogStarten()
    {
        FindObjectOfType<AnzeigeZuege>().StartDialog(dialog);
        FindObjectOfType<AnzeigeEvents>().StartDialog(dialog);
    }
}

