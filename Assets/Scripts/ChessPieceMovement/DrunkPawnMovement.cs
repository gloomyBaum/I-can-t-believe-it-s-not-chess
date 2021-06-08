using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//can only move diagonal
public class DrunkPawnMovement : ChessPieceMovement
{
    public override List<Tile> MoveableTiles()
    {
        List<Tile> list = new List<Tile>();

        //selected chessPiece
        C = gameObject.GetComponent<ChessPiece>();
        CurrentX = C.CurrentX;
        CurrentY = C.CurrentY;

        if (C.ChessPieceColor == ChessPiece.Color.White)
        {
            //white moves:
            //Diagonal Left
            X = CurrentX - 1;
            Y = CurrentY + 1;
            CTarget = PieceManager.GetChessPiece(X, Y);
            TargetTile = Board.GetTile(X, Y);
            IsAttackable = CanAttack(C, CTarget);
            IsPassable = CheckIfTilePassable(TargetTile);
            if (IsPassable)
            {
                list.Add(TargetTile);
            }
            else if(IsAttackable)
            {
                list.Add(TargetTile);
                CTarget.IsInDanger = true;
            }

            //Diagonal Right
            X = CurrentX + 1;
            Y = CurrentY + 1;
            CTarget = PieceManager.GetChessPiece(X, Y);
            TargetTile = Board.GetTile(X, Y);
            IsAttackable = CanAttack(C, CTarget);
            IsPassable = CheckIfTilePassable(TargetTile);
            if (IsPassable)
            {
                list.Add(TargetTile);
            }
            else if (IsAttackable)
            {
                list.Add(TargetTile);
                CTarget.IsInDanger = true;
            }
        }
        else
        {
            //black moves:
            //Diagonal Left
            X = CurrentX - 1;
            Y = CurrentY - 1;
            CTarget = PieceManager.GetChessPiece(X, Y);
            TargetTile = Board.GetTile(X, Y);
            IsAttackable = CanAttack(C, CTarget);
            IsPassable = CheckIfTilePassable(TargetTile);
            if (IsPassable)
            {
                list.Add(TargetTile);
            }
            else if (IsAttackable)
            {
                list.Add(TargetTile);
                CTarget.IsInDanger = true;
            }

            //Diagonal Right
            X = CurrentX + 1;
            Y = CurrentY - 1;
            CTarget = PieceManager.GetChessPiece(X, Y);
            TargetTile = Board.GetTile(X, Y);
            IsAttackable = CanAttack(C, CTarget);
            IsPassable = CheckIfTilePassable(TargetTile);
            if (IsPassable)
            {
                list.Add(TargetTile);
            }
            else if (IsAttackable)
            {
                list.Add(TargetTile);
                CTarget.IsInDanger = true;
            }
        }
        return list;
    }
}
