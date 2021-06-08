using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aufforstung : RandomEvent
{
    private Vector2Int center;
    private System.Random rng;
    

    public Aufforstung(int expiresAfter) : base(expiresAfter)
    {
        Name = "Aufforstung!";
        Description = "Einer Eurer Bauern hat es sich zu Aufgabe gemacht der lokalen Abholzung entgegenzuwirken. Er pflanzt jetzt regelmäßig neue Bäume.";
        
    }

    public Aufforstung(int expiresAfter, int cooldown) : base(expiresAfter, cooldown)
    {
        Name = "Aufforstung!";
        Description = "Einer Eurer Bauern hat es sich zu Aufgabe gemacht der lokalen Abholzung entgegenzuwirken. Er pflanzt jetzt regelmäßig neue Bäume.";

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
        //base.OnTrigger();
        rng = RandomEventSystem.Instance.RandomIndexGenerator;

        center = new Vector2Int();
        center.x = rng.Next(0, ChessBoardManager.Instance.XSize - 1);
        center.y = rng.Next(0, ChessBoardManager.Instance.YSize - 1);

        spawnNewTree();
    }

    public override void OnTurnUpdate()
    {
        //base.OnTurnUpdate();
        spawnNewTree();
    }

    private void spawnNewTree()
    {
        Vector2Int newTreePos = RandomEventSystem.RandomFreePosition();

        SimpleTileEvent newTreeEvent = new SimpleTileEvent(ExpiresAfter, newTreePos.x, newTreePos.y);
        newTreeEvent.OnTrigger();
        RandomEventSystem.QueueEvent(newTreeEvent);

    }

    private Vector2Int getRandomTile()
    {
        Vector2Int pos = new Vector2Int();

        pos.x = rng.Next(center.x - ExpiresAfter, center.x + ExpiresAfter);
        pos.y = rng.Next(center.y - ExpiresAfter, center.y + ExpiresAfter);

        if(pos.x < 0 || pos.x >= ChessBoardManager.Instance.XSize || pos.y < 0 || pos.y >= ChessBoardManager.Instance.YSize)
        {

            return getRandomTile();
        }
        else
        {
            Tile t = ChessBoardManager.Instance.GetTile(pos.x, pos.y);
            if (t.enabled && ChessPieceManager.Instance.GetChessPiece(t.CurrentX, t.CurrentY) == null)
            {
                return pos;
            }
            else
            {
                return getRandomTile();
            }
        }
        
    }
}
