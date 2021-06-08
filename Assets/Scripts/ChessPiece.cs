using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChessPiece : MonoBehaviour
{
    #region variables
    public Type ChessPieceType;
    public Color ChessPieceColor;
    public bool Unmoved;
    public bool EnPassantVulnerable;

    public int CurrentX;
    public int CurrentY;

    public Sprite OriginalSprite;
    public Sprite HighlightSprite;  //todo highlightSprites 

    private List<Tile> possibleMoves;
    private List<Tile> possibleAttackMoves;
    private Dictionary<Tile, ChessPieceMovement.SpecialMoves> possibleSpecialMoves;

    public bool IsInDanger;
    #endregion
    #region properties
    public List<Tile> PossibleMoves
    {
        get { return possibleMoves; }
    }
    public Dictionary<Tile, ChessPieceMovement.SpecialMoves> PossibleSpecialMoves
    {
        get { return possibleSpecialMoves; }
    }
    #endregion
    private void Awake()
    {
        possibleMoves = new List<Tile>();
        possibleAttackMoves = new List<Tile>();
    }
    public enum Type
    {
        Empty,
        Rook,
        Knight,
        Bishop,
        Queen,
        King,
        Pawn,
        //...
        BrokenQueen,
        DrunkPawn,
        ReallyDrunkPawn,
        KingUpgrade,
        Harley
    }
    public enum Color
    {
        White,
        Black
    }
    public void UpdateMoves()
    {
        possibleMoves = GetComponent<ChessPieceMovement>().MoveableTiles();
        possibleSpecialMoves = GetComponent<ChessPieceMovement>().SpecialMoveableTiles();
    }

    
    public static string baseTypeToString(Type t)
    {

        switch (t)
        {

            case Type.Rook:
                return "Turm";
            case Type.Knight:
            case Type.Harley:
                return "Springer";
            case Type.Bishop:
                return "Läufer";
            case Type.Queen:
            case Type.BrokenQueen:
                return "Dame";
            case Type.King:
            case Type.KingUpgrade:
                return "König";
            case Type.Pawn:
            case Type.DrunkPawn:
            case Type.ReallyDrunkPawn:
                return "Bauer";
            default:
                return "None";
        }
    }

    public static string colorToString(Color c)
    {
        switch (c)
        {
            case Color.Black:
                return "schwarz";
            case Color.White:
                return "weiß";
            default:
                return "none";
        }
    }
}
