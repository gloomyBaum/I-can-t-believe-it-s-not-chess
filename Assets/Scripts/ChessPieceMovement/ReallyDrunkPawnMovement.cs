using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//todo
public class ReallyDrunkPawnMovement : ChessPieceMovement
{
    public override Dictionary<Tile, SpecialMoves> SpecialMoveableTiles()
    {
        Dictionary<Tile, ChessPieceMovement.SpecialMoves> dict = new Dictionary<Tile, ChessPieceMovement.SpecialMoves>();
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
            if (IsAttackable)
            {
                dict.Add(TargetTile, ChessPieceMovement.SpecialMoves.ReallyDrunk);
            }

            //Diagonal Right
            X = CurrentX + 1;
            Y = CurrentY + 1;
            CTarget = PieceManager.GetChessPiece(X, Y);
            TargetTile = Board.GetTile(X, Y);
            IsAttackable = CanAttack(C, CTarget);
            if (IsAttackable)
            {
                dict.Add(TargetTile, ChessPieceMovement.SpecialMoves.ReallyDrunk);
            }

            //Standard vorne
            X = CurrentX;
            Y = CurrentY + 1;
            TargetTile = Board.GetTile(X, Y);
            IsPassable = CheckIfTilePassable(TargetTile);
            if (IsPassable)
            {
                dict.Add(TargetTile, ChessPieceMovement.SpecialMoves.ReallyDrunk);

                X = CurrentX;
                Y = CurrentY + 2;
                TargetTile = Board.GetTile(X, Y);
                IsPassable = CheckIfTilePassable(TargetTile);
                if (C.Unmoved && IsPassable)
                {
                    dict.Add(TargetTile, ChessPieceMovement.SpecialMoves.ReallyDrunk);
                }
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
            if (IsAttackable)
            {
                dict.Add(TargetTile, ChessPieceMovement.SpecialMoves.ReallyDrunk);
            }

            //Diagonal Right
            X = CurrentX + 1;
            Y = CurrentY - 1;
            CTarget = PieceManager.GetChessPiece(X, Y);
            TargetTile = Board.GetTile(X, Y);
            IsAttackable = CanAttack(C, CTarget);
            if (IsAttackable)
            {
                dict.Add(TargetTile, ChessPieceMovement.SpecialMoves.ReallyDrunk);
            }

            //Standard vorne
            X = CurrentX;
            Y = CurrentY - 1;
            TargetTile = Board.GetTile(X, Y);
            IsPassable = CheckIfTilePassable(TargetTile);
            if (IsPassable)
            {
                dict.Add(TargetTile, ChessPieceMovement.SpecialMoves.ReallyDrunk);

                X = CurrentX;
                Y = CurrentY - 2;
                TargetTile = Board.GetTile(X, Y);
                IsPassable = CheckIfTilePassable(TargetTile);
                if (C.Unmoved && IsPassable)
                {
                    dict.Add(TargetTile, ChessPieceMovement.SpecialMoves.ReallyDrunk);
                }
            }
        }
        return dict;
    }
}
