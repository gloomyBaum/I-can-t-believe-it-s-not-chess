using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ChessPieceRandomEvent : RandomEventComponent
{
    protected ChessPiece chessPiece;

    public ChessPieceRandomEvent()
    {
    }

    protected ChessPieceRandomEvent(ChessPiece chessPiece)
    {
        this.chessPiece = chessPiece;
    }

    public ChessPiece ChessPiece { get => chessPiece; protected set => chessPiece = value; }

}
