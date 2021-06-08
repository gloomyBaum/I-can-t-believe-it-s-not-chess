using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KingMovement : ChessPieceMovement
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
        if (IsPassable)
        {
            list.Add(TargetTile);
        }
        else if (IsAttackable)
        {
            list.Add(TargetTile);
            CTarget.IsInDanger = true;
        }
        //left left
        X = CurrentX - 1;
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
        //bottom left
        X = CurrentX - 1;
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
        //top right
        X = CurrentX + 1;
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
        //right right
        X = CurrentX + 1;
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
        //bottom right
        X = CurrentX + 1;
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
        //top top
        X = CurrentX;
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
        //bottom bottom
        X = CurrentX;
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

    public override Dictionary<Tile, SpecialMoves> SpecialMoveableTiles()
    {
        Dictionary<Tile, SpecialMoves> dict = new Dictionary<Tile, SpecialMoves>();
        //selected chessPiece
        C = gameObject.GetComponent<ChessPiece>();
        CurrentX = C.CurrentX;
        CurrentY = C.CurrentY;
        //Roachade
        //white king
        if(C.Unmoved && C.ChessPieceColor == ChessPiece.Color.White)
        {
            ChessPiece leftRook = PieceManager.GetChessPiece(0, 0);
            ChessPiece rightRook = PieceManager.GetChessPiece(Board.XSize - 1, 0);
            if (leftRook && leftRook.Unmoved && leftRook.ChessPieceType == ChessPiece.Type.Rook)
            {
                bool check = true;
                for (int i = 1; i < CurrentX; i++)
                {
                    if(!CheckIfTilePassable(Board.GetTile(i,0)))
                    {
                        check = false;
                    }
                }
                if (check)
                    dict.Add(Board.GetTile(CurrentX - 2, 0), ChessPieceMovement.SpecialMoves.Rochade);
            }
            if(rightRook && rightRook.Unmoved && rightRook.ChessPieceType == ChessPiece.Type.Rook)
            {
                bool check = true;
                for (int i = CurrentX + 1; i < Board.XSize - 1; i++)
                {
                    if (!CheckIfTilePassable(Board.GetTile(i, 0)))
                    {
                        check = false;
                    }
                }
                if (check && Board.GetTile(CurrentX + 2, 0))
                {
                    dict.Add(Board.GetTile(CurrentX + 2, 0), ChessPieceMovement.SpecialMoves.Rochade);
                }
                    
            }
        }
        //black king
        else if (C.Unmoved && C.ChessPieceColor == ChessPiece.Color.Black)
        {
            ChessPiece leftRook = PieceManager.GetChessPiece(0, Board.YSize - 1);
            ChessPiece rightRook = PieceManager.GetChessPiece(Board.XSize - 1, Board.YSize - 1);
            if (leftRook && leftRook.Unmoved && leftRook.ChessPieceType == ChessPiece.Type.Rook)
            {
                bool check = true;
                for (int i = 1; i < CurrentX; i++)
                {
                    if (!CheckIfTilePassable(Board.GetTile(i, Board.YSize - 1)))
                    {
                        check = false;
                    }
                }
                if (check)
                    dict.Add(Board.GetTile(CurrentX - 2, Board.YSize - 1), ChessPieceMovement.SpecialMoves.Rochade);
            }
            if (rightRook && rightRook.Unmoved && rightRook.ChessPieceType == ChessPiece.Type.Rook)
            {
                bool check = true;
                for (int i = CurrentX + 1; i < Board.XSize - 1; i++)
                {
                    if (!CheckIfTilePassable(Board.GetTile(i, Board.YSize - 1)))
                    {
                        check = false;
                    }
                }
                if (check && Board.GetTile(CurrentX + 2, Board.YSize - 1))
                    dict.Add(Board.GetTile(CurrentX + 2, Board.YSize - 1), ChessPieceMovement.SpecialMoves.Rochade);
            }
        }


        return dict;
    }
}
