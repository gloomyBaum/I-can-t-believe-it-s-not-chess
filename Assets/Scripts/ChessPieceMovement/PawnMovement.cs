using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PawnMovement : ChessPieceMovement
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
            if (IsAttackable)
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
            if (IsAttackable)
            {
                list.Add(TargetTile);
                CTarget.IsInDanger = true;
            }

            //Standard vorne
            X = CurrentX;
            Y = CurrentY + 1;
            TargetTile = Board.GetTile(X, Y);
            IsPassable = CheckIfTilePassable(TargetTile);
            if (IsPassable)
            {
                list.Add(TargetTile);

                X = CurrentX;
                Y = CurrentY + 2;
                TargetTile = Board.GetTile(X, Y);
                IsPassable = CheckIfTilePassable(TargetTile);
                if (C.Unmoved && IsPassable)
                {
                    list.Add(TargetTile);
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
                list.Add(TargetTile);
                CTarget.IsInDanger = true;
            }

            //Diagonal Right
            X = CurrentX + 1;
            Y = CurrentY - 1;
            CTarget = PieceManager.GetChessPiece(X, Y);
            TargetTile = Board.GetTile(X, Y);
            IsAttackable = CanAttack(C, CTarget);
            if (IsAttackable)
            {
                list.Add(TargetTile);
                CTarget.IsInDanger = true;
            }

            //Standard vorne
            X = CurrentX;
            Y = CurrentY - 1;
            TargetTile = Board.GetTile(X, Y);
            IsPassable = CheckIfTilePassable(TargetTile);
            if (IsPassable)
            {
                list.Add(TargetTile);

                X = CurrentX;
                Y = CurrentY - 2;
                TargetTile = Board.GetTile(X, Y);
                IsPassable = CheckIfTilePassable(TargetTile);
                if (C.Unmoved && IsPassable)
                {
                    list.Add(TargetTile);
                }
            }
        }
        return list;
    }

    public override Dictionary<Tile, ChessPieceMovement.SpecialMoves> SpecialMoveableTiles()
    {
        Dictionary<Tile, ChessPieceMovement.SpecialMoves> dict = new Dictionary<Tile, ChessPieceMovement.SpecialMoves>();
        //selected chessPiece
        C = gameObject.GetComponent<ChessPiece>();
        CurrentX = C.CurrentX;
        CurrentY = C.CurrentY;

        //Check for Enpassant and Promotion
        if (C.ChessPieceColor == ChessPiece.Color.White)
        {
            //EnPassant
            //white moves:
            //Left enemy
            X = CurrentX - 1;
            Y = CurrentY;
            CTarget = PieceManager.GetChessPiece(X, Y);
            TargetTile = Board.GetTile(X, Y + 1);
            IsPassable = CheckIfTilePassable(TargetTile);
            if(CTarget && CTarget.EnPassantVulnerable && IsPassable)
            {
                dict.Add(TargetTile, ChessPieceMovement.SpecialMoves.EnPassant);
            }
            //Right enemy
            X = CurrentX + 1;
            Y = CurrentY;
            CTarget = PieceManager.GetChessPiece(X, Y);
            TargetTile = Board.GetTile(X, Y + 1);
            IsPassable = CheckIfTilePassable(TargetTile);
            if (CTarget && CTarget.EnPassantVulnerable && IsPassable)
            {
                dict.Add(TargetTile, ChessPieceMovement.SpecialMoves.EnPassant);
            }

            //Promotion
            //straight
            X = CurrentX;
            Y = CurrentY + 1;
            CTarget = PieceManager.GetChessPiece(X, Y);
            TargetTile = Board.GetTile(X, Y);
            IsPassable = CheckIfTilePassable(TargetTile);
            IsAttackable = CanAttack(C, CTarget);
            if (TargetTile && TargetTile.CurrentY == Board.YSize - 1 && IsPassable)
            {
                dict.Add(TargetTile, ChessPieceMovement.SpecialMoves.Promotion);
            }
            //Diagonal Left
            X = CurrentX - 1;
            Y = CurrentY + 1;
            CTarget = PieceManager.GetChessPiece(X, Y);
            TargetTile = Board.GetTile(X, Y);
            IsAttackable = CanAttack(C, CTarget);
            if (TargetTile && TargetTile.CurrentY == Board.YSize - 1 && IsAttackable)
            {
                dict.Add(TargetTile, ChessPieceMovement.SpecialMoves.Promotion);
            }

            //Diagonal Right
            X = CurrentX + 1;
            Y = CurrentY + 1;
            CTarget = PieceManager.GetChessPiece(X, Y);
            TargetTile = Board.GetTile(X, Y);
            IsAttackable = CanAttack(C, CTarget);
            if (TargetTile && TargetTile.CurrentY == Board.YSize - 1 && IsAttackable)
            {
                dict.Add(TargetTile, ChessPieceMovement.SpecialMoves.Promotion);
            }
        }
        else
        {
            //EnPassant
            //black moves:
            //Left enemy
            X = CurrentX - 1;
            Y = CurrentY;
            CTarget = PieceManager.GetChessPiece(X, Y);
            TargetTile = Board.GetTile(X, Y - 1);
            IsPassable = CheckIfTilePassable(TargetTile);
            if (CTarget && CTarget.EnPassantVulnerable && IsPassable)
            {
                dict.Add(TargetTile, ChessPieceMovement.SpecialMoves.EnPassant);
            }
            //Right enemy
            X = CurrentX + 1;
            Y = CurrentY;
            CTarget = PieceManager.GetChessPiece(X, Y);
            TargetTile = Board.GetTile(X, Y - 1);
            IsPassable = CheckIfTilePassable(TargetTile);
            if (CTarget && CTarget.EnPassantVulnerable && IsPassable)
            {
                dict.Add(TargetTile, ChessPieceMovement.SpecialMoves.EnPassant);
            }

            //Promotion
            //straight
            X = CurrentX;
            Y = CurrentY - 1;
            CTarget = PieceManager.GetChessPiece(X, Y);
            TargetTile = Board.GetTile(X, Y);
            IsPassable = CheckIfTilePassable(TargetTile);
            IsAttackable = CanAttack(C, CTarget);
            if (TargetTile && TargetTile.CurrentY == 0 && IsPassable)
            {
                dict.Add(TargetTile, ChessPieceMovement.SpecialMoves.Promotion);
            }
            //Diagonal Left
            X = CurrentX - 1;
            Y = CurrentY - 1;
            CTarget = PieceManager.GetChessPiece(X, Y);
            TargetTile = Board.GetTile(X, Y);
            IsAttackable = CanAttack(C, CTarget);
            if (TargetTile && TargetTile.CurrentY == 0 && IsAttackable)
            {
                dict.Add(TargetTile, ChessPieceMovement.SpecialMoves.Promotion);
            }

            //Diagonal Right
            X = CurrentX + 1;
            Y = CurrentY - 1;
            CTarget = PieceManager.GetChessPiece(X, Y);
            TargetTile = Board.GetTile(X, Y);
            IsAttackable = CanAttack(C, CTarget);
            if (TargetTile && TargetTile.CurrentY == 0 && IsAttackable)
            {
                dict.Add(TargetTile, ChessPieceMovement.SpecialMoves.Promotion);
            }
        }

            return dict;
    }
}
