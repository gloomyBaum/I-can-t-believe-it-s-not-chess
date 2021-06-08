using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveChessPieceEvent : ChessPieceRandomEvent
{
    public RemoveChessPieceEvent(ChessPiece chessPiece) : base(chessPiece)
    {
    }

    public override void OnBoardSizeUpdate(BoardRandomEvent e)
    {
        
    }

    public override void OnTrigger()
    {

        //lösche das betreffende ChessPiece vom Feld
        if(ChessPiece != null)
        {
            try
            {
                ChessPieceManager.Instance.RemoveChessPiece(ChessPiece);
            }catch(InvalidOperationException e)
            {
                Debug.LogError(e.Message);
            }
            

        }
    }

    public override void OnTurnUpdate()
    {
        //nothing to do
    }
}
