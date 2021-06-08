using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class AnzeigeEvents : MonoBehaviour
{
    public Text eventsText;

    
    private Queue<string> letzteEvents;
    // Start is called before the first frame update
    void Start()
    {
        letzteEvents = new Queue<string>();
    }

    public void StartDialog(Dialog dialog)
    {
        letzteEvents.Clear();

        foreach (string events in dialog.events)
        {
            letzteEvents.Enqueue(events);

        }

        ZeigeNaechstenZug();
    }

    public void ZeigeNaechstenZug()
    {
        if (letzteEvents.Count == 0)
        {
            EndDialog();
            return;
        }

        string evs = letzteEvents.Dequeue();
        eventsText.text = evs;
    }

    void EndDialog()
    {
        Debug.Log("Ende");
    }
}

