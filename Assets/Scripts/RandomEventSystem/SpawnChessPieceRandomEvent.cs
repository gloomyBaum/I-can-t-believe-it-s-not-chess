using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnChessPieceRandomEvent : ChessPieceRandomEvent
{
    private int xPos;
    private int yPos;
    private ChessPiece.Type chessPieceType;

    public SpawnChessPieceRandomEvent(ChessPiece.Type chessPieceType, int xPos, int yPos): base()
    {
        this.xPos = xPos;
        this.yPos = yPos;
        this.chessPieceType = chessPieceType;

    }

    public override void OnBoardSizeUpdate(BoardRandomEvent e)
    {
        ChessBoardManager b = ChessBoardManager.Instance;
        int xEnd = b.XSize;
        int yEnd = b.YSize;


        if (e.ExtensionSide == BoardRandomEvent.ExtensionDirection.North)
        {
            if (yPos > yEnd)
            {
                yPos += e.NumberOfRows;
            }
        }
        else if (e.ExtensionSide == BoardRandomEvent.ExtensionDirection.South)
        {
            if (yPos < 0)
            {
                yPos -= e.NumberOfRows;
            }
        }
        else if (e.ExtensionSide == BoardRandomEvent.ExtensionDirection.East)
        {
            if (xPos > xEnd)
            {
                xPos += e.NumberOfRows;
            }
        }
        else if (e.ExtensionSide == BoardRandomEvent.ExtensionDirection.West)
        {
            if (xPos < 0)
            {
                xPos -= e.NumberOfRows;
            }
        }
        else if (e.ExtensionSide == BoardRandomEvent.ExtensionDirection.CenterHorizontal)
        {

            int boardCenter = yEnd / 2;

            if (yPos > boardCenter)
            {
                yPos += e.NumberOfRows;
            }
        }
        else if (e.ExtensionSide == BoardRandomEvent.ExtensionDirection.CenterVertical)
        {

            int boardCenter = xEnd / 2;

            if (xPos > boardCenter)
            {
                xPos += e.NumberOfRows;
            }
        }
    }

    public override void OnTrigger()
    {
        
        bool isWhitePlayer = SessionManager.Instance.IsWhiteTurn;
        ChessPiece.Color TeamColor;
        if (isWhitePlayer)
        {
            TeamColor = ChessPiece.Color.White;
        }
        else
        {
            TeamColor = ChessPiece.Color.Black;
        }
        try
        {
            ChessPieceManager.Instance.SpawnPiece(xPos, yPos, chessPieceType, TeamColor);
            this.chessPiece = ChessPieceManager.Instance.GetChessPiece(xPos, yPos);
        }catch (InvalidOperationException e)
        {
            Debug.LogWarning(e.Message);
        }
         
        
    }

    public override void OnTurnUpdate()
    {
        //nothing to do
    }
}
