using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnChessPieceAtRandomPositionEvent : RandomEvent
{
    private ChessPiece.Type chessPieceType;
    

    public SpawnChessPieceAtRandomPositionEvent(int cooldown, ChessPiece.Type chessPieceType) : base(cooldown)
    {
        this.chessPieceType = chessPieceType;

        string typeString = ChessPiece.baseTypeToString(chessPieceType);
        this.Name = typeString + " aufgetaucht!";
        if (ChessPiece.baseTypeToString(chessPieceType).Equals("Dame"))
        {
            this.Description = "Eine neue " + typeString + " ist aufgetaucht.";
        }else
        {
            this.Description = "Ein neuer " + typeString + " ist aufgetaucht.";

        }
        
    }

    public override void OnTrigger()
    {
        Vector2Int spawnPosition = RandomEventSystem.RandomFreePosition();
        SpawnChessPieceRandomEvent re = new SpawnChessPieceRandomEvent(chessPieceType, spawnPosition.x, spawnPosition.y);

        this.OnTriggerEvents.Add(re);
        base.OnTrigger();

        string typeString = ChessPiece.baseTypeToString(chessPieceType);
        string colorString = ChessPiece.colorToString(re.ChessPiece.ChessPieceColor);

        if (ChessPiece.baseTypeToString(chessPieceType).Equals("Dame"))
        {
            this.Description = "Eine " + colorString + "e " + typeString + " ist auf Feld " + spawnPosition + " aufgetaucht.";
        }
        else
        {
            this.Description = "Ein " + colorString + "er "+ typeString + " ist auf Feld " + spawnPosition + " aufgetaucht.";

        }

        
    }
}
