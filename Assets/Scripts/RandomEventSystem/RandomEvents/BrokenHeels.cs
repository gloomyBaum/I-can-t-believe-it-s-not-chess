using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrokenHeels : RandomEvent
{ 

    public BrokenHeels() : base(2, "High Heels Fiasko", "Durch den unebenen Boden haben sich alle Damen den HighHeel abgebrochen. Sie sind Schlecht gelaunt und können sich 2 Züge nicht bewegen.")
    {
        
    }

    public override void OnExpire()
    {
        foreach (ChessPiece c in ChessPieceManager.Instance.ChessPieceList)
        {
            if (c.ChessPieceType == ChessPiece.Type.BrokenQueen)
            {
                ChessPieceManager.Instance.ChangePieceType(c, ChessPiece.Type.Queen);
            }
        }
    }

    public override void OnTrigger()
    {
        foreach(ChessPiece c in ChessPieceManager.Instance.ChessPieceList)
        {
            if(c.ChessPieceType == ChessPiece.Type.Queen)
            {
                ChessPieceManager.Instance.ChangePieceType(c, ChessPiece.Type.BrokenQueen);
            }
        }
    }
}
