using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnChessPieceAtFixedPositionEvent : RandomEvent
{
    private ChessPiece.Type chessPieceType;
    public SpawnChessPieceAtFixedPositionEvent(int expiresAfter, ChessPiece.Type chessPieceType, int x, int y) : base(expiresAfter)
    {
        this.chessPieceType = chessPieceType;
        this.OnTriggerEvents.Add(new SpawnChessPieceRandomEvent(chessPieceType, x, y));
    }

    public SpawnChessPieceAtFixedPositionEvent(int expiresAfter, string name, string description, ChessPiece.Type chessPieceType, int x, int y) : base(expiresAfter, name, description)
    {
        this.chessPieceType = chessPieceType;
        this.OnTriggerEvents.Add(new SpawnChessPieceRandomEvent(chessPieceType, x, y));

    }

}
