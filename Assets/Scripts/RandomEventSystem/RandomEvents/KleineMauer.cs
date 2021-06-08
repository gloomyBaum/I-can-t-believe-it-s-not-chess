using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KleineMauer : RandomEvent
{

    public enum Orientation
    {
        Vertical,
        Horizontal
    }

    private Vector2Int impactPosition;
    private bool active;
    private Orientation orientation;

    public KleineMauer(Orientation orientation) : base(1, "Mauer im Feld", "Jemand kam auf die tolle Idee eine Mauer zu bauen... Man weiß nicht wer.")
    {
        this.orientation = orientation;
    }

    public KleineMauer() : base(1, "Mauer im Feld", "Jemand kam auf die tolle Idee eine Mauer zu bauen... Man weiß nicht wer.")
    {
        this.orientation = Orientation.Horizontal;
    }

    public KleineMauer(int cooldown, Orientation orientation) : base(cooldown, "Mauer im Feld", "Jemand kam auf die tolle Idee eine Mauer zu bauen... Man weiß nicht wer.")
    {
        this.orientation = orientation;

    }
    public KleineMauer(int cooldown) : base(cooldown, "Mauer im Feld", "Jemand kam auf die tolle Idee eine Mauer zu bauen... Man weiß nicht wer.")
    {
        this.orientation = Orientation.Horizontal;

    }

    public KleineMauer(int expiresAfter, int cooldown, Orientation orientation) : base(expiresAfter, cooldown, "Mauer im Feld", "Jemand kam auf die tolle Idee eine Mauer zu bauen... Man weiß nicht wer.")
    {
        this.orientation = orientation;

    }
    public KleineMauer(int expiresAfter, int cooldown) : base(expiresAfter, cooldown, "Mauer im Feld", "Jemand kam auf die tolle Idee eine Mauer zu bauen... Man weiß nicht wer.")
    {
        this.orientation = Orientation.Horizontal;

    }
    public override void OnBoardSizeUpdate(BoardRandomEvent e)
    {
        base.OnBoardSizeUpdate(e);
        if (active)
        {
            if (e.ExtensionSide == BoardRandomEvent.ExtensionDirection.North)
            {
                //nothing to do
            }
            else if (e.ExtensionSide == BoardRandomEvent.ExtensionDirection.South)
            {
                impactPosition.y += e.NumberOfRows;
            }
            else if (e.ExtensionSide == BoardRandomEvent.ExtensionDirection.East)
            {
                impactPosition.x += e.NumberOfRows;
            }
            else if (e.ExtensionSide == BoardRandomEvent.ExtensionDirection.West)
            {
                //nothing to do
            }
            else if (e.ExtensionSide == BoardRandomEvent.ExtensionDirection.CenterHorizontal)
            {
                ChessBoardManager b = ChessBoardManager.Instance;
                int boardCenter = b.YSize / 2;

                if (impactPosition.y > boardCenter)
                {
                    impactPosition.y += e.NumberOfRows;
                }
            }
            else if (e.ExtensionSide == BoardRandomEvent.ExtensionDirection.CenterVertical)
            {
                ChessBoardManager b = ChessBoardManager.Instance;
                int boardCenter = b.XSize / 2;

                if (impactPosition.x > boardCenter)
                {
                    impactPosition.x += e.NumberOfRows;
                }
            }
        }
    }


    public override void OnTrigger()
    {
        OnTriggerEvents = new List<RandomEventComponent>();
        OnExpireEvents = new List<RandomEventComponent>();

        impactPosition = RandomEventSystem.RandomFreePosition();

        this.active = true;

        //Events für Trigger

        if(orientation == Orientation.Vertical)
        {
            //Tiles Disablen
            TileRandomEvent top = new TileRandomEvent(impactPosition.x, impactPosition.y + 1, false);
            TileRandomEvent bottom = new TileRandomEvent(impactPosition.x, impactPosition.y - 1, false);
            TileRandomEvent center = new TileRandomEvent(impactPosition.x, impactPosition.y, false);

            OnTriggerEvents.Add(top);
            OnTriggerEvents.Add(bottom);
            OnTriggerEvents.Add(center);

            //Events für Expire
            top = new TileRandomEvent(impactPosition.x, impactPosition.y + 1, true);
            bottom = new TileRandomEvent(impactPosition.x, impactPosition.y - 1, true);
            center = new TileRandomEvent(impactPosition.x, impactPosition.y, true);


            OnExpireEvents.Add(top);
            OnExpireEvents.Add(bottom);
            OnExpireEvents.Add(center);
        }
        else
        {
            //Tiles Disablen
            TileRandomEvent left = new TileRandomEvent(impactPosition.x - 1, impactPosition.y, false);
            TileRandomEvent right = new TileRandomEvent(impactPosition.x + 1, impactPosition.y, false);
            TileRandomEvent center = new TileRandomEvent(impactPosition.x, impactPosition.y, false);

            OnTriggerEvents.Add(left);
            OnTriggerEvents.Add(right);
            OnTriggerEvents.Add(center);

            left = new TileRandomEvent(impactPosition.x - 1, impactPosition.y, true);
            right = new TileRandomEvent(impactPosition.x + 1, impactPosition.y, true);
            center = new TileRandomEvent(impactPosition.x, impactPosition.y, true);


            OnExpireEvents.Add(left);
            OnExpireEvents.Add(right);
            OnExpireEvents.Add(center);
        }
        

        //Trigger the Event
        base.OnTrigger();
    }
}
