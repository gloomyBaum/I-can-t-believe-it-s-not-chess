using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteor : RandomEvent
{
    private Vector2Int impactPosition;
    private bool active;

    public Meteor(int expiresAfter) : base(expiresAfter, "Meteor!", "Ein Meteor schlägt ein!")
    {
        //this.Name = "Meteor!";
        this.active = false;
    }
    public Meteor(int expiresAfter, int cooldown) : base(expiresAfter, cooldown, "Meteor!", "Ein Meteor schlägt ein!")
    {
        //this.Name = "Meteor!";
        this.active = false;
    }

    public override void OnBoardSizeUpdate(BoardRandomEvent e)
    {
        base.OnBoardSizeUpdate(e);
        if (active) { 
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

        impactPosition = getValidImpactPosition();
        

        this.Description = "Ein Meteor schlägt auf Feld " + impactPosition.ToString() + " ein!";
        this.active = true;

        //Events für Trigger
        //Tiles Disablen
        TileRandomEvent top = new TileRandomEvent(impactPosition.x, impactPosition.y + 1, false);
        TileRandomEvent bottom = new TileRandomEvent(impactPosition.x, impactPosition.y - 1, false);
        TileRandomEvent left = new TileRandomEvent(impactPosition.x - 1, impactPosition.y, false);
        TileRandomEvent right = new TileRandomEvent(impactPosition.x + 1, impactPosition.y, false);
        TileRandomEvent center = new TileRandomEvent(impactPosition.x, impactPosition.y, false);

        OnTriggerEvents.Add(top);
        OnTriggerEvents.Add(bottom);
        OnTriggerEvents.Add(left);
        OnTriggerEvents.Add(right);
        OnTriggerEvents.Add(center);
        
        //ChessPieces removen
        ChessPieceManager cpm = ChessPieceManager.Instance;
        List<ChessPiece> destroyedChessPieces = new List<ChessPiece>();
        ChessPiece ctmp = cpm.GetChessPiece(impactPosition.x, impactPosition.y + 1);
        destroyedChessPieces.Add(ctmp);
        ctmp = cpm.GetChessPiece(impactPosition.x, impactPosition.y - 1);
        destroyedChessPieces.Add(ctmp);
        ctmp = cpm.GetChessPiece(impactPosition.x + 1, impactPosition.y);
        destroyedChessPieces.Add(ctmp);
        ctmp = cpm.GetChessPiece(impactPosition.x - 1, impactPosition.y);
        destroyedChessPieces.Add(ctmp);
        ctmp = cpm.GetChessPiece(impactPosition.x, impactPosition.y);
        destroyedChessPieces.Add(ctmp);

        foreach(ChessPiece c in destroyedChessPieces)
        {
            if(c != null)
            {
                OnTriggerEvents.Add(new RemoveChessPieceEvent(c));
            }
        }


        //Events für Expire
        top = new TileRandomEvent(impactPosition.x, impactPosition.y + 1, true);
        bottom = new TileRandomEvent(impactPosition.x, impactPosition.y - 1, true);
        left = new TileRandomEvent(impactPosition.x - 1, impactPosition.y, true);
        right = new TileRandomEvent(impactPosition.x + 1, impactPosition.y, true);
        center = new TileRandomEvent(impactPosition.x, impactPosition.y, true);


        OnExpireEvents.Add(top);
        OnExpireEvents.Add(bottom);
        OnExpireEvents.Add(left);
        OnExpireEvents.Add(right);
        OnExpireEvents.Add(center);

        //Trigger the Event
        base.OnTrigger();
    }

    //diese Methode kann unterumständen ziemlich lahm sein. Sollte aber selten vorkommen
    private static Vector2Int getValidImpactPosition()
    {
        Vector2Int pos = RandomEventSystem.RandomFreePosition();
        bool foundValidPos = Meteor.allTilesValid(pos);
        
        while (!foundValidPos)
        {
            pos = RandomEventSystem.RandomFreePosition();
            foundValidPos = allTilesValid(pos);
        }

        return pos;
    }


    private static bool allTilesValid(Vector2Int center)
    {
        ChessPiece[] cps = new ChessPiece[5];

        cps[0] = ChessPieceManager.Instance.GetChessPiece(center.x, center.y);
        cps[1] = ChessPieceManager.Instance.GetChessPiece(center.x, center.y + 1);
        cps[2] = ChessPieceManager.Instance.GetChessPiece(center.x, center.y - 1);
        cps[3] = ChessPieceManager.Instance.GetChessPiece(center.x - 1, center.y);
        cps[4] = ChessPieceManager.Instance.GetChessPiece(center.x + 1, center.y);

        bool foundValidPos = false;
        ChessPiece witness = null;

        foreach (ChessPiece c in cps)
        {
            if(c != null)
            {
                if (c.ChessPieceType == ChessPiece.Type.King || c.ChessPieceType == ChessPiece.Type.KingUpgrade)
                {
                    foundValidPos = false;
                    witness = c;
                    break;
                }
                else
                {
                    foundValidPos = true;
                }
            }
            else
            {
                foundValidPos = true;
            }
            
        }

        return foundValidPos;
    }
}
