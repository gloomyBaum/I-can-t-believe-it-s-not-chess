using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChessPieceMovement : MonoBehaviour
{
    #region variables
    protected ChessBoardManager Board;
    protected ChessPieceManager PieceManager;

    protected ChessPiece C;
    protected ChessPiece CTarget;
    protected Tile TargetTile;

    protected int X, Y;
    protected int CurrentX;
    protected int CurrentY;
    protected bool IsAttackable;
    protected bool IsPassable;

    //for PotentialAttackableTiles
    protected bool IsFriendOnTile;
    #endregion
    public enum SpecialMoves
    {
        EnPassant,
        Promotion,
        Rochade,
		ReallyDrunk
    }

    private void Awake()
    {
        Board = ChessBoardManager.Instance;
        PieceManager = ChessPieceManager.Instance;
    }
    public virtual List<Tile> MoveableTiles()
    {
        List<Tile> list = new List<Tile>();
        return list;
    }
    public virtual Dictionary<Tile, ChessPieceMovement.SpecialMoves> SpecialMoveableTiles()
    {
        Dictionary<Tile, ChessPieceMovement.SpecialMoves> dict = new Dictionary<Tile, ChessPieceMovement.SpecialMoves>();
        return dict;
    }
    protected bool CanAttack(ChessPiece cOrigin, ChessPiece cTarget)
    {
        if (cOrigin != null && cTarget != null && cOrigin.ChessPieceColor != cTarget.ChessPieceColor && Board.GetTile(CTarget.CurrentX, CTarget.CurrentY).TileState != Tile.State.Disabled)
        {
            return true;
        }
        return false;
    }
    protected bool CheckIfTilePassable(Tile tile)
    {
        if (tile != null && tile.TileState == Tile.State.Free)
        {
            return true;
        }
        return false;
    }
    protected bool CheckIfFriendly(ChessPiece cOrigin, ChessPiece cTarget)
    {
        if (cOrigin != null && cTarget != null && cOrigin.ChessPieceColor == cTarget.ChessPieceColor)
        {
            return true;
        }
        return false;
    }
}
