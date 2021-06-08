using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//can move +1 tile
public class KingUpgradeMovement : ChessPieceMovement
{
    public override List<Tile> MoveableTiles()
    {
        List<Tile> list = new List<Tile>();

        //selected chessPiece
        C = gameObject.GetComponent<ChessPiece>();
        CurrentX = C.CurrentX;
        CurrentY = C.CurrentY;

        //top left
        X = CurrentX - 1;
        Y = CurrentY + 1;
        CTarget = PieceManager.GetChessPiece(X, Y);
        TargetTile = Board.GetTile(X, Y);
        IsPassable = CheckIfTilePassable(TargetTile);
        IsAttackable = CanAttack(C, CTarget);
        //isSafeZone = !Board.CheckIfKingDangerZone(X, Y, C.ChessPieceColor);
        if (IsAttackable)
        {
            list.Add(TargetTile);
            CTarget.IsInDanger = true;
        }
        else if(IsPassable)
        {
            list.Add(TargetTile);
            X = CurrentX - 2;
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
        }
        //left left
        X = CurrentX - 1;
        Y = CurrentY;
        CTarget = PieceManager.GetChessPiece(X, Y);
        TargetTile = Board.GetTile(X, Y);
        IsPassable = CheckIfTilePassable(TargetTile);
        IsAttackable = CanAttack(C, CTarget);
        //isSafeZone = !Board.CheckIfKingDangerZone(X, Y, C.ChessPieceColor);
        if (IsAttackable)
        {
            list.Add(TargetTile);
            CTarget.IsInDanger = true;
        }
        else if (IsPassable)
        {
            list.Add(TargetTile);
            X = CurrentX - 2;
            Y = CurrentY;
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
        }
        //bottom left
        X = CurrentX - 1;
        Y = CurrentY - 1;
        CTarget = PieceManager.GetChessPiece(X, Y);
        TargetTile = Board.GetTile(X, Y);
        IsPassable = CheckIfTilePassable(TargetTile);
        IsAttackable = CanAttack(C, CTarget);
        //isSafeZone = !Board.CheckIfKingDangerZone(X, Y, C.ChessPieceColor);
        if (IsAttackable)
        {
            list.Add(TargetTile);
            CTarget.IsInDanger = true;
        }
        else if (IsPassable)
        {
            list.Add(TargetTile);
            X = CurrentX - 2;
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
        }
        //top right
        X = CurrentX + 1;
        Y = CurrentY + 1;
        CTarget = PieceManager.GetChessPiece(X, Y);
        TargetTile = Board.GetTile(X, Y);
        IsPassable = CheckIfTilePassable(TargetTile);
        IsAttackable = CanAttack(C, CTarget);
        //isSafeZone = !Board.CheckIfKingDangerZone(X, Y, C.ChessPieceColor);
        if (IsAttackable)
        {
            list.Add(TargetTile);
            CTarget.IsInDanger = true;
        }
        else if (IsPassable)
        {
            list.Add(TargetTile);
            X = CurrentX + 2;
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
        }
        //right right
        X = CurrentX + 1;
        Y = CurrentY;
        CTarget = PieceManager.GetChessPiece(X, Y);
        TargetTile = Board.GetTile(X, Y);
        IsPassable = CheckIfTilePassable(TargetTile);
        IsAttackable = CanAttack(C, CTarget);
        //isSafeZone = !Board.CheckIfKingDangerZone(X, Y, C.ChessPieceColor);
        if (IsAttackable)
        {
            list.Add(TargetTile);
            CTarget.IsInDanger = true;
        }
        else if (IsPassable)
        {
            list.Add(TargetTile);
            X = CurrentX + 2;
            Y = CurrentY;
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
        }
        //bottom right
        X = CurrentX + 1;
        Y = CurrentY - 1;
        CTarget = PieceManager.GetChessPiece(X, Y);
        TargetTile = Board.GetTile(X, Y);
        IsPassable = CheckIfTilePassable(TargetTile);
        IsAttackable = CanAttack(C, CTarget);
        //isSafeZone = !Board.CheckIfKingDangerZone(X, Y, C.ChessPieceColor);
        if (IsAttackable)
        {
            list.Add(TargetTile);
            CTarget.IsInDanger = true;
        }
        else if (IsPassable)
        {
            list.Add(TargetTile);
            X = CurrentX + 2;
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
        }
        //top top
        X = CurrentX;
        Y = CurrentY + 1;
        CTarget = PieceManager.GetChessPiece(X, Y);
        TargetTile = Board.GetTile(X, Y);
        IsPassable = CheckIfTilePassable(TargetTile);
        IsAttackable = CanAttack(C, CTarget);
        //isSafeZone = !Board.CheckIfKingDangerZone(X, Y, C.ChessPieceColor);
        if (IsAttackable)
        {
            list.Add(TargetTile);
            CTarget.IsInDanger = true;
        }
        else if (IsPassable)
        {
            list.Add(TargetTile);
            X = CurrentX;
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
        }
        //bottom bottom
        X = CurrentX;
        Y = CurrentY - 1;
        CTarget = PieceManager.GetChessPiece(X, Y);
        TargetTile = Board.GetTile(X, Y);
        IsPassable = CheckIfTilePassable(TargetTile);
        IsAttackable = CanAttack(C, CTarget);
        //isSafeZone = !Board.CheckIfKingDangerZone(X, Y, C.ChessPieceColor);
        if (IsAttackable)
        {
            list.Add(TargetTile);
            CTarget.IsInDanger = true;
        }
        else if (IsPassable)
        {
            list.Add(TargetTile);
            X = CurrentX;
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
        }

        return list;
    }
}
