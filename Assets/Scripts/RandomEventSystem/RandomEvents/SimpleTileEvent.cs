using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleTileEvent : RandomEvent
{
    private int x;
    private int y;
    public SimpleTileEvent(int expiresAfter, int x, int y) : base(expiresAfter)
    {
        this.x = x;
        this.y = y;

        OnTriggerEvents.Add(new TileRandomEvent(x, y, false));
        OnExpireEvents.Add(new TileRandomEvent(x, y, true));
    }

    public override void OnBoardSizeUpdate(BoardRandomEvent e)
    {
        base.OnBoardSizeUpdate(e);
    }

    public override void OnExpire()
    {
        base.OnExpire();
    }

    public override void OnTrigger()
    {
        base.OnTrigger();
    }

    public override void OnTurnUpdate()
    {
        base.OnTurnUpdate();
    }
}
