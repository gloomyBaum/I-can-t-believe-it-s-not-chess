using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oktoberfest : RandomEvent
{
    public Oktoberfest() : base(8, "Oktoberfest", "Alle Bauern vergnügen sich auf dem Oktoberfest in der Nähe. Sie können nun einige Züge nicht mehr geradeaus laufen!")
    {
    }

    public override void OnExpire()
    {
        foreach (ChessPiece c in ChessPieceManager.Instance.ChessPieceList)
        {
            if (c.ChessPieceType == ChessPiece.Type.DrunkPawn)
            {
                ChessPieceManager.Instance.ChangePieceType(c, ChessPiece.Type.Pawn);
            }
        }
    }

    public override void OnTrigger()
    {
        foreach (ChessPiece c in ChessPieceManager.Instance.ChessPieceList)
        {
            if (c.ChessPieceType == ChessPiece.Type.Pawn)
            {
                ChessPieceManager.Instance.ChangePieceType(c, ChessPiece.Type.DrunkPawn);
            }
        }
    }
}
