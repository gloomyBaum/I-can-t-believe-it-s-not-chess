using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnzeigeZuege : MonoBehaviour
{

    public Text zuegeText;


    private Queue<string> letzteZuege;
    // Start is called before the first frame update
    void Start()
    {
        letzteZuege = new Queue<string>();
    }

    public void StartDialog(Dialog dialog)
    {
        letzteZuege.Clear();

        foreach (string zuege in dialog.zuege)
        {
            letzteZuege.Enqueue(zuege);
           
        }

        ZeigeNaechstenZug();
    }

    public void ZeigeNaechstenZug()
    {
        if(letzteZuege.Count == 0)
        {
            EndDialog();
            return;
        }

        string zuege = letzteZuege.Dequeue();
        zuegeText.text = zuege;
    }

    void EndDialog()
    {
        Debug.Log("Ende");
    }
}
