using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleBoardSizeEvent : RandomEvent
{
    //try expiresAfter = -1 um zu verhindern dass Event zweimal getriggert wird?
    public SimpleBoardSizeEvent(string name, string description, BoardRandomEvent.ExtensionDirection direction, int numberOfRows) : base(0, name, description)
    {
        this.OnTriggerEvents.Add(new BoardRandomEvent(direction, numberOfRows));
    }

    public SimpleBoardSizeEvent(int cooldown, string name, string description, BoardRandomEvent.ExtensionDirection direction, int numberOfRows) : base(cooldown, name, description)
    {
        this.OnTriggerEvents.Add(new BoardRandomEvent(direction, numberOfRows));
    }
}
