using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileRandomEvent : RandomEventComponent
{
    //private Tile tile;
    private int x;
    private int y;
    private bool enabled;

    public TileRandomEvent(int x, int y, bool enabled):base()
    {
        this.x = x;
        this.y = y;
        this.enabled = enabled;
    }

    //public Tile Tile { get => tile; set => tile = value; }
    public int X { get => x; set => x = value; }
    public int Y { get => y; set => y = value; }
    public bool Enabled { get => enabled; set => enabled = value; }

    public override void OnBoardSizeUpdate(BoardRandomEvent e)
    {
        if (e.ExtensionSide == BoardRandomEvent.ExtensionDirection.North)
        {
            //nothing to do
        }
        else if (e.ExtensionSide == BoardRandomEvent.ExtensionDirection.South)
        {
            y += e.NumberOfRows;
        }
        else if (e.ExtensionSide == BoardRandomEvent.ExtensionDirection.East)
        {
            x += e.NumberOfRows;
        }
        else if (e.ExtensionSide == BoardRandomEvent.ExtensionDirection.West)
        {
            //nothing to do
        }
        else if (e.ExtensionSide == BoardRandomEvent.ExtensionDirection.CenterHorizontal)
        {
            ChessBoardManager b = ChessBoardManager.Instance;
            int boardCenter = b.YSize / 2;

            if (y > boardCenter)
            {
                y += e.NumberOfRows;
            }
        }
        else if (e.ExtensionSide == BoardRandomEvent.ExtensionDirection.CenterVertical)
        {
            ChessBoardManager b = ChessBoardManager.Instance;
            int boardCenter = b.XSize / 2;

            if (x > boardCenter)
            {
                x += e.NumberOfRows;
            }
        }
    }

    public override void OnTrigger()
    {
        //TileComponent t = TileManager.Instance().GetTile(x, y);
        ChessBoardManager t = ChessBoardManager.Instance;
        if (enabled)
        {
            Tile tile = t.GetTile(x, y);
            if(tile)
                tile.Enable();
        }
        else
        {
            Tile tile = t.GetTile(x, y);
            if (tile)
                tile.Disable();
        }
    }

    public override void OnTurnUpdate()
    {
        //nothing to do
    }
}
