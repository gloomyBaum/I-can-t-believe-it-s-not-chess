using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class Dialog
{
    public ArrayList zuege;
    public ArrayList events;

    public void SetZuege(ArrayList zug)
    {
        zuege = new ArrayList();
        zuege.Add(zug);
    }

    public void SetEvents(ArrayList ev)
    {
        events = new ArrayList();
        events.Add(ev);
    }

    public void EndGame()
    {
        zuege.Clear();
        events.Clear();
    }
       

}
