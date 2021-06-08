using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//besserer Name: BoardSizeRandomEvent
public class BoardRandomEvent : RandomEventComponent
{

    
    public enum ExtensionDirection { North, South, East, West, CenterVertical, CenterHorizontal};

    private ExtensionDirection extensionSide;
    private int numberOfRows;

    public BoardRandomEvent(ExtensionDirection extensionSide, int numberOfRows)
    {
        this.extensionSide = extensionSide;
        this.numberOfRows = numberOfRows;
    }

    public ExtensionDirection ExtensionSide { get => extensionSide; private set => extensionSide = value; }
    public int NumberOfRows { get => numberOfRows; private set => numberOfRows = value; }

    public override void OnTrigger()
    {

        //enlarge the board

        
        ChessBoardManager board = ChessBoardManager.Instance;
        int xEnd = board.XSize;
        int yEnd = board.YSize;

        if (extensionSide == BoardRandomEvent.ExtensionDirection.North)
        {
            board.AddRows(numberOfRows, yEnd);

        }
        else if (extensionSide == BoardRandomEvent.ExtensionDirection.South)
        {
            board.AddRows(numberOfRows, 0);

        }
        else if (extensionSide == BoardRandomEvent.ExtensionDirection.East)
        {
            board.AddColumns(numberOfRows, xEnd);

        }
        else if (extensionSide == BoardRandomEvent.ExtensionDirection.West)
        {
            board.AddColumns(numberOfRows, 0);
            
        }
        else if (extensionSide == BoardRandomEvent.ExtensionDirection.CenterHorizontal)
        {
            int boardCenter = yEnd / 2;

            board.AddRows(numberOfRows, boardCenter);

        }
        else if (extensionSide == BoardRandomEvent.ExtensionDirection.CenterVertical)
        {
            int boardCenter = xEnd / 2;
            board.AddColumns(numberOfRows, boardCenter);

        }
        

        //tell the Game that BoardSize has been changed
        RandomEventSystem.BoardSizeUpdate(this);

    }

    public override void OnTurnUpdate()
    {
        //nothing to do
    }

    public override void OnBoardSizeUpdate(BoardRandomEvent e)
    {
        //nothing to do
    }
}
