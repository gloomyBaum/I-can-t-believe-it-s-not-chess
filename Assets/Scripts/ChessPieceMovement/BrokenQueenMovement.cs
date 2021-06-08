using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//cant move at all
public class BrokenQueenMovement : ChessPieceMovement
{
    public override List<Tile> MoveableTiles()
    {
        List<Tile> list = new List<Tile>();
        return list;
    }
}
