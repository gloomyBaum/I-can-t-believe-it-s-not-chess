using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForTheKing : RandomEvent
{
    private List<ChessPiece> oldKings;

    public ForTheKing() : base(9999, "Für den König!", "Die Könige haben nun auch bemerkt das dies kein normales Schachfeld ist. Um mitzuhalten können sie nun doppelt so weit laufen.")
    {
        oldKings = new List<ChessPiece>();
    }

    public override void OnExpire()
    {
       //should not expire
    }

    public override void OnTrigger()
    {
        foreach (ChessPiece c in ChessPieceManager.Instance.ChessPieceList)
        {
            if (c.ChessPieceType == ChessPiece.Type.King)
            {
                ChessPieceManager.Instance.ChangePieceType(c, ChessPiece.Type.KingUpgrade);
            }
        }
    }
}