using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//REALLY 
//BIG 
//TODOOOOO
public class MoveChessPieceRandomEvent : ChessPieceRandomEvent
{
    
    private int moveToX;
    private int moveToY;
    private bool active;

    /*Bewegen zu fixen Feldern: einfach x,y koordinaten angeben.
     Bewegen zum Brettrand: mit x,y koordinaten außerhalb des Brettsangeben.*/
    public MoveChessPieceRandomEvent(ChessPiece chessPiece, int moveToX, int moveToY): base(chessPiece)
    {
        this.moveToX = moveToX;
        this.moveToY = moveToY;
        this.active = false;
    }

    public int MoveToX { get => moveToX; set => moveToX = value; }
    public int MoveToY { get => moveToY; set => moveToY = value; }
    

    public override void OnBoardSizeUpdate(BoardRandomEvent e)
    {
        ChessBoardManager b = ChessBoardManager.Instance;
        int xEnd = b.XSize;
        int yEnd = b.YSize;


        if (e.ExtensionSide == BoardRandomEvent.ExtensionDirection.North)
        {
            if(moveToY > yEnd)
            {
                moveToY += e.NumberOfRows;
            }
        }
        else if (e.ExtensionSide == BoardRandomEvent.ExtensionDirection.South)
        {
            if(moveToY < 0)
            {
                moveToY -= e.NumberOfRows;
            }
        }
        else if (e.ExtensionSide == BoardRandomEvent.ExtensionDirection.East)
        {
            if(moveToX > xEnd)
            {
                moveToX += e.NumberOfRows;
            }
        }   
        else if (e.ExtensionSide == BoardRandomEvent.ExtensionDirection.West)
        {
            if (moveToX < 0)
            {
                moveToX -= e.NumberOfRows;
            }
        }
        else if (e.ExtensionSide == BoardRandomEvent.ExtensionDirection.CenterHorizontal)
        {
            
            int boardCenter = yEnd / 2;

            if (moveToY > boardCenter)
            {
                moveToY += e.NumberOfRows;
            }
        }
        else if (e.ExtensionSide == BoardRandomEvent.ExtensionDirection.CenterVertical)
        {
            
            int boardCenter = xEnd / 2;

            if (moveToX > boardCenter)
            {
                moveToX += e.NumberOfRows;
            }
        }
    }

    public override void OnTrigger()
    {
        this.active = true;
       
    }

    public override void OnTurnUpdate()
    {
        if (this.active)
        {
            throw new System.NotImplementedException("Kein Move implementiert.");
            //Debug.LogWarning("Ein MoveChessPieceEvent ist aktiv. Muss etwas geändert werden?");
        }
        
    }
}
