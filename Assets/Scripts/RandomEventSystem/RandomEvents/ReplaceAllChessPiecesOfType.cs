using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReplaceAllChessPiecesOfType : RandomEvent
{
    private bool temporaryChange;
    private ChessPiece.Type oldChessPieceType;
    private ChessPiece.Type newChessPieceType;

    public bool TemporaryChange { get => temporaryChange; protected set => temporaryChange = value; }

    public ReplaceAllChessPiecesOfType(int expiresAfter, ChessPiece.Type oldChessPieceType, ChessPiece.Type newChessPieceType, bool temporaryChange) : base(expiresAfter)
    {
        this.temporaryChange = temporaryChange;
        this.oldChessPieceType = oldChessPieceType;
        this.newChessPieceType = newChessPieceType;

    }
    public ReplaceAllChessPiecesOfType(int expiresAfter, int cooldown, ChessPiece.Type oldChessPieceType, ChessPiece.Type newChessPieceType, bool temporaryChange) : base(expiresAfter,cooldown)
    {
        this.temporaryChange = temporaryChange;
        this.oldChessPieceType = oldChessPieceType;
        this.newChessPieceType = newChessPieceType;

    }

    public ReplaceAllChessPiecesOfType(int expiresAfter, int cooldown, string name, string description, ChessPiece.Type oldChessPieceType, ChessPiece.Type newChessPieceType, bool temporaryChange) : base(expiresAfter, cooldown, name, description)
    {
        this.temporaryChange = temporaryChange;
        this.oldChessPieceType = oldChessPieceType;
        this.newChessPieceType = newChessPieceType;
    }
    public ReplaceAllChessPiecesOfType(int expiresAfter, string name, string description, ChessPiece.Type oldChessPieceType, ChessPiece.Type newChessPieceType, bool temporaryChange) : base(expiresAfter, name, description)
    {
        this.temporaryChange = temporaryChange;
        this.oldChessPieceType = oldChessPieceType;
        this.newChessPieceType = newChessPieceType;
    }


    public override void OnExpire()
    {
        if (temporaryChange)
        {
            base.OnExpire();
        }
        
    }

    public override void OnTrigger()
    {
        foreach (ChessPiece c in ChessPieceManager.Instance.ChessPieceList)
        {
            if(c.ChessPieceType == oldChessPieceType)
            {
                OnTriggerEvents.Add(new ReplaceChessPieceRandomEvent(c, newChessPieceType));
                OnExpireEvents.Add(new ReplaceChessPieceRandomEvent(c, oldChessPieceType));
            }
            
        }

        base.OnTrigger();
    }

}
