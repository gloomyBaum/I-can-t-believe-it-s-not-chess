using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QueenMovement : ChessPieceMovement
{
    public override List<Tile> MoveableTiles()
    {
        List<Tile> list = new List<Tile>();

        //selected chessPiece
        C = gameObject.GetComponent<ChessPiece>();
        CurrentX = C.CurrentX;
        CurrentY = C.CurrentY;

        //Bishop Movement
        //Top Left
        X = CurrentX - 1; //start inspecting from the figure
        Y = CurrentY + 1;

        while (true)
        {
            CTarget = PieceManager.GetChessPiece(X, Y);
            TargetTile = Board.GetTile(X, Y);
            IsPassable = CheckIfTilePassable(TargetTile);
            IsAttackable = CanAttack(C, CTarget);
            if (IsPassable)
            {
                list.Add(TargetTile);
                X--;
                Y++;
            }
            else if (IsAttackable)
            {
                list.Add(TargetTile);
                CTarget.IsInDanger = true;
                break;
            }
            else
            {
                break;
            }
        }

        //Top Right
        X = CurrentX + 1; //start inspecting from the figure
        Y = CurrentY + 1;

        while (true)
        {
            CTarget = PieceManager.GetChessPiece(X, Y);
            TargetTile = Board.GetTile(X, Y);
            IsPassable = CheckIfTilePassable(TargetTile);
            IsAttackable = CanAttack(C, CTarget);
            if (IsPassable)
            {
                list.Add(TargetTile);
                X++;
                Y++;
            }
            else if (IsAttackable)
            {
                list.Add(TargetTile);
                CTarget.IsInDanger = true;
                break;
            }
            else
            {
                break;
            }
        }

        //Bottom Left
        X = CurrentX - 1; //start inspecting from the figure
        Y = CurrentY - 1;

        while (true)
        {
            CTarget = PieceManager.GetChessPiece(X, Y);
            TargetTile = Board.GetTile(X, Y);
            IsPassable = CheckIfTilePassable(TargetTile);
            IsAttackable = CanAttack(C, CTarget);
            if (IsPassable)
            {
                list.Add(TargetTile);
                X--;
                Y--;
            }
            else if (IsAttackable)
            {
                list.Add(TargetTile);
                CTarget.IsInDanger = true;
                break;
            }
            else
            {
                break;
            }
        }

        //Bottom Right
        X = CurrentX + 1; //start inspecting from the figure
        Y = CurrentY - 1;

        while (true)
        {
            CTarget = PieceManager.GetChessPiece(X, Y);
            TargetTile = Board.GetTile(X, Y);
            IsPassable = CheckIfTilePassable(TargetTile);
            IsAttackable = CanAttack(C, CTarget);
            if (IsPassable)
            {
                list.Add(TargetTile);
                X++;
                Y--;
            }
            else if (IsAttackable)
            {
                list.Add(TargetTile);
                CTarget.IsInDanger = true;
                break;
            }
            else
            {
                break;
            }
        }

        //Rook Movement
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
