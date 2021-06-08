using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnightMovement : ChessPieceMovement
{
    public override List<Tile> MoveableTiles()
    {
        List<Tile> list = new List<Tile>();

        //selected chessPiece
        C = gameObject.GetComponent<ChessPiece>();
        CurrentX = C.CurrentX;
        CurrentY = C.CurrentY;

        //top left 1
        X = CurrentX - 1;
        Y = CurrentY + 2;
        CTarget = PieceManager.GetChessPiece(X, Y);
        TargetTile = Board.GetTile(X, Y);
        IsPassable = CheckIfTilePassable(TargetTile);
        IsAttackable = CanAttack(C, CTarget);
        if (IsPassable)
        {
            list.Add(TargetTile);
        }
        else if (IsAttackable)
        {
            list.Add(TargetTile);
            CTarget.IsInDanger = true;
        }
        //top left 2
        X = CurrentX - 2;
        Y = CurrentY + 1;
        CTarget = PieceManager.GetChessPiece(X, Y);
        TargetTile = Board.GetTile(X, Y);
        IsPassable = CheckIfTilePassable(TargetTile);
        IsAttackable = CanAttack(C, CTarget);
        if (IsPassable)
        {
            list.Add(TargetTile);
        }
        else if (IsAttackable)
        {
            list.Add(TargetTile);
            CTarget.IsInDanger = true;
        }
        //bot left 1
        X = CurrentX - 1;
        Y = CurrentY - 2;
        CTarget = PieceManager.GetChessPiece(X, Y);
        TargetTile = Board.GetTile(X, Y);
        IsPassable = CheckIfTilePassable(TargetTile);
        IsAttackable = CanAttack(C, CTarget);
        if (IsPassable)
        {
            list.Add(TargetTile);
        }
        else if (IsAttackable)
        {
            list.Add(TargetTile);
            CTarget.IsInDanger = true;
        }
        //bot left 2
        X = CurrentX - 2;
        Y = CurrentY - 1;
        CTarget = PieceManager.GetChessPiece(X, Y);
        TargetTile = Board.GetTile(X, Y);
        IsPassable = CheckIfTilePassable(TargetTile);
        IsAttackable = CanAttack(C, CTarget);
        if (IsPassable)
        {
            list.Add(TargetTile);
        }
        else if (IsAttackable)
        {
            list.Add(TargetTile);
            CTarget.IsInDanger = true;
        }
        //top right 1
        X = CurrentX + 1;
        Y = CurrentY + 2;
        CTarget = PieceManager.GetChessPiece(X, Y);
        TargetTile = Board.GetTile(X, Y);
        IsPassable = CheckIfTilePassable(TargetTile);
        IsAttackable = CanAttack(C, CTarget);
        if (IsPassable)
        {
            list.Add(TargetTile);
        }
        else if (IsAttackable)
        {
            list.Add(TargetTile);
            CTarget.IsInDanger = true;
        }
        //top right 2
        X = CurrentX + 2;
        Y = CurrentY + 1;
        CTarget = PieceManager.GetChessPiece(X, Y);
        TargetTile = Board.GetTile(X, Y);
        IsPassable = CheckIfTilePassable(TargetTile);
        IsAttackable = CanAttack(C, CTarget);
        if (IsPassable)
        {
            list.Add(TargetTile);
        }
        else if (IsAttackable)
        {
            list.Add(TargetTile);
            CTarget.IsInDanger = true;
        }
        //bot right 1
        X = CurrentX + 1;
        Y = CurrentY - 2;
        CTarget = PieceManager.GetChessPiece(X, Y);
        TargetTile = Board.GetTile(X, Y);
        IsPassable = CheckIfTilePassable(TargetTile);
        IsAttackable = CanAttack(C, CTarget);
        if (IsPassable)
        {
            list.Add(TargetTile);
        }
        else if (IsAttackable)
        {
            list.Add(TargetTile);
            CTarget.IsInDanger = true;
        }
        //bot right 2
        X = CurrentX + 2;
        Y = CurrentY - 1;
        CTarget = PieceManager.GetChessPiece(X, Y);
        TargetTile = Board.GetTile(X, Y);
        IsPassable = CheckIfTilePassable(TargetTile);
        IsAttackable = CanAttack(C, CTarget);
        if (IsPassable)
        {
            list.Add(TargetTile);
        }
        else if (IsAttackable)
        {
            list.Add(TargetTile);
            CTarget.IsInDanger = true;
        }


        return list;
    }
}

