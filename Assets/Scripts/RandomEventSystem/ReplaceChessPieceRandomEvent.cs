using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReplaceChessPieceRandomEvent : ChessPieceRandomEvent
{
    private ChessPiece oldChessPiece;
    private ChessPiece.Type newChessPieceType;

    public ReplaceChessPieceRandomEvent(ChessPiece oldChessPiece, ChessPiece.Type newChessPieceType): base(oldChessPiece)
    {
        this.oldChessPiece = oldChessPiece;
        this.newChessPieceType = newChessPieceType;
    }

    public ChessPiece OldChessPiece { get => oldChessPiece; set => oldChessPiece = value; }
    public ChessPiece.Type NewChessPieceType { get => newChessPieceType; set => newChessPieceType = value; }

    public override void OnBoardSizeUpdate(BoardRandomEvent e)
    {
        
    }

    public override void OnTrigger()
    {
        if(oldChessPiece != null)
        {
            ChessPieceManager.Instance.ChangePieceType(oldChessPiece, newChessPieceType);
        }
        

    }

    public override void OnTurnUpdate()
    {
        
    }
}
