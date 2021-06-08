using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Baumaßnahme : RandomEvent
{
    private int numberOfRows;
    public Baumaßnahme(int numberOfRows) : base(0)
    {
        this.Name = "Baumaßnahme";
        this.Description = "Dein Schloß wird erweitert.";
        this.numberOfRows = numberOfRows;
    }
    public Baumaßnahme(int numberOfRows, int cooldown) : base(0, cooldown)
    {
        this.Name = "Baumaßnahme";
        this.Description = "Dein Schloß wird erweitert.";
        this.numberOfRows = numberOfRows;
    }

    public override void OnTrigger()
    {
        OnTriggerEvents = new List<RandomEventComponent>();
        bool isWhitePlayer = SessionManager.Instance.IsWhiteTurn;

        BoardRandomEvent be;
        if (isWhitePlayer)
        {
            be = new BoardRandomEvent(BoardRandomEvent.ExtensionDirection.South, numberOfRows);
        }
        else
        {
            be = new BoardRandomEvent(BoardRandomEvent.ExtensionDirection.North, numberOfRows);
        }

        this.OnTriggerEvents.Add(be);

        base.OnTrigger();
    }
}
