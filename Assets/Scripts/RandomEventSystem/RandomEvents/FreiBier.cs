using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreiBier : RandomEvent
{
    private List<ChessPiece> oldPawns;

    public FreiBier() : base(6, "Freibier", "Bauern vergnügten sich bei einem lokalen #RettetDieWale Event wo es Freibier gab. Ihre Bewegungen sind in nächster Zeit nicht ganz wie erwartet.")
    {
        oldPawns = new List<ChessPiece>();
    }

    public override void OnExpire()
    {
        foreach (ChessPiece c in ChessPieceManager.Instance.ChessPieceList)
        {
            if (c.ChessPieceType == ChessPiece.Type.ReallyDrunkPawn)
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
                ChessPieceManager.Instance.ChangePieceType(c, ChessPiece.Type.ReallyDrunkPawn);
            }
        }
    }
}
