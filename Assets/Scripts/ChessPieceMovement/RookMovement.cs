using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RookMovement : ChessPieceMovement
{
    public override List<Tile> MoveableTiles()
    {
        List<Tile> list = new List<Tile>();

        //selected chessPiece
        C = gameObject.GetComponent<ChessPiece>();
        CurrentX = C.CurrentX;
        CurrentY = C.CurrentY;

        X = CurrentX;
        Y = CurrentY;
        //left
        while (true)
        {
            X--;
            CTarget = PieceManager.GetChessPiece(X, Y);
            TargetTile = Board.GetTile(X, Y);
            IsPassable = CheckIfTilePassable(TargetTile);
            IsAttackable = CanAttack(C, CTarget);
            if (IsAttackable)
            {
                list.Add(TargetTile);
                CTarget.IsInDanger = true;
                break;
            }
            else if (IsPassable)
            {
                list.Add(TargetTile);
            }
            else
            {
                break;
            }
        }
        X = CurrentX;
        Y = CurrentY;
        //right
        while (true)
        {
            X++;
            CTarget = PieceManager.GetChessPiece(X, Y);
            TargetTile = Board.GetTile(X, Y);
            IsPassable = CheckIfTilePassable(TargetTile);
            IsAttackable = CanAttack(C, CTarget);
            if (IsAttackable)
            {
                list.Add(TargetTile);
                CTarget.IsInDanger = true;
                break;
            }
            else if (IsPassable)
            {
                list.Add(TargetTile);
            }
            else
            {
                break;
            }
        }
        X = CurrentX;
        Y = CurrentY;
        //top
        while (true)
        {
            Y++;
            CTarget = PieceManager.GetChessPiece(X, Y);
            TargetTile = Board.GetTile(X, Y);
            IsPassable = CheckIfTilePassable(TargetTile);
            IsAttackable = CanAttack(C, CTarget);
            if (IsAttackable)
            {
                list.Add(TargetTile);
                CTarget.IsInDanger = true;
                break;
            }
            else if (IsPassable)
            {
                list.Add(TargetTile);
            }
            else
            {
                break;
            }
        }
        X = CurrentX;
        Y = CurrentY;
        //bottom
        while (true)
        {
            Y--;
            CTarget = PieceManager.GetChessPiece(X, Y);
            TargetTile = Board.GetTile(X, Y);
            IsPassable = CheckIfTilePassable(TargetTile);
            IsAttackable = CanAttack(C, CTarget);
            if (IsAttackable)
            {
                list.Add(TargetTile);
                CTarget.IsInDanger = true;
                break;
            }
            else if (IsPassable)
            {
                list.Add(TargetTile);
            }
            else
            {
                break;
            }
        }
        return list;
    }
}
