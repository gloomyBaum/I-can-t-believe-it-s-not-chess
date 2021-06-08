using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HarleyDavidson : RandomEvent
{
    public HarleyDavidson() : base(6, "Harley Davidson", "Alle Springer erhalten ein temporäres Upgrade! Ihre Beine werden für 6 Züge durch Reifen ersetzt.")
    {
    }

    public override void OnExpire()
    {
        foreach (ChessPiece c in ChessPieceManager.Instance.ChessPieceList)
        {
            if (c.ChessPieceType == ChessPiece.Type.Harley)
            {
                ChessPieceManager.Instance.ChangePieceType(c, ChessPiece.Type.Knight);
            }
        }
    }

    public override void OnTrigger()
    {
        foreach (ChessPiece c in ChessPieceManager.Instance.ChessPieceList)
        {
            if (c.ChessPieceType == ChessPiece.Type.Knight)
            {
                ChessPieceManager.Instance.ChangePieceType(c, ChessPiece.Type.Harley);
            }
        }
    }
}
