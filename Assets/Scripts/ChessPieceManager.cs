using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChessPieceManager : MonoBehaviour
{
    #region variables
    public static ChessPieceManager Instance;
    private ChessBoardManager Board;

    public List<ChessPiece> ChessPieceList;

    private const float LAYER_HEIGHT = 0.00f;
    private const float OVERALL_SCALE = 0.9f;
    #endregion
    private void Awake()
    {
        if (Instance == null)
            Instance = this;        
    }
    public void Setup(Scenario currentScenario)
    {
        ChessPieceList = new List<ChessPiece>();
        Board = ChessBoardManager.Instance;
        //Set up all Pieces
        foreach (FormationUnit unit in currentScenario.ChessPieceFormation)
        {
            SpawnPiece(unit.X, unit.Y, unit.Type, unit.Color);
        }
    }
    public void SpawnPiece(int x, int y, ChessPiece.Type type, ChessPiece.Color color)
    {
        if (type == ChessPiece.Type.Empty)
        {
            return;
        }
        if(GetChessPiece(x,y) != null)
        {
            throw new InvalidOperationException("Tile " + x + ", " + y + " is not empty. Cannot Spawn ChessPiece!");
        }
        GameObject go = Instantiate(Resources.Load("Prefabs/" + type.ToString() + color.ToString())) as GameObject;

        //transform Magic
        go.transform.parent = GameObject.Find("ChessBoard").transform;
        go.transform.position = new Vector3(x, LAYER_HEIGHT, y);
        go.transform.rotation = Quaternion.Euler(90, 0, 0);
        go.transform.localScale = new Vector3(OVERALL_SCALE, OVERALL_SCALE, OVERALL_SCALE);

        //update position
        go.GetComponent<ChessPiece>().CurrentX = x;
        go.GetComponent<ChessPiece>().CurrentY = y;

        //set Layer to ignore raycast
        go.layer = LayerMask.NameToLayer("Ignore Raycast");

        //update tile state       
        Board.GetTile(x, y).TileState = Tile.State.Occupied;

        //add to list
        ChessPieceList.Add(go.GetComponent<ChessPiece>());

        go.name = type.ToString() + color.ToString();
    }
    public ChessPiece GetChessPiece(int x, int y)
    {
        foreach(ChessPiece piece in ChessPieceList)
        {
            if (piece.CurrentX == x && piece.CurrentY == y)
                return piece;
        }
        return null;
    }
    public void MoveChessPieceTo(ChessPiece piece, Tile targetTile)
    {
        //move gameobject
        Tile oldTile = Board.GetTile(piece.CurrentX, piece.CurrentY);
        piece.gameObject.transform.position = new Vector3(targetTile.CurrentX, 0, targetTile.CurrentY);

        //update position
        piece.CurrentX = targetTile.CurrentX;
        piece.CurrentY = targetTile.CurrentY;
        //sound Figur
        FindObjectOfType<AudioManager1>().Play("FigurMoved");
        //update Tile state
        targetTile.TileState = Tile.State.Occupied;
        oldTile.TileState = Tile.State.Free;
    }
    public void HighlightChessPiece(ChessPiece piece)
    {
        piece.GetComponent<SpriteRenderer>().sprite = piece.HighlightSprite;
        // Sound drag
        FindObjectOfType<AudioManager1>().Play("FigurDragged");
    }
    public void CancelHighlightChessPiece(ChessPiece piece)
    {
        piece.GetComponent<SpriteRenderer>().sprite = piece.OriginalSprite;
        //sound dragged
        FindObjectOfType<AudioManager1>().Play("FigurDragged");
    }
    public void UpdateAllPossibleMoves()
    {
        foreach (ChessPiece piece in ChessPieceList)
        {
            piece.UpdateMoves();
        }
    }
    public bool ValidateMove(ChessPiece piece, Tile targetTile)
    {
        if(piece.PossibleMoves.Contains(targetTile) || piece.PossibleSpecialMoves.ContainsKey(targetTile))
        {
            return IsKingSafeAfterMove(piece, targetTile);
        }
        return false;
    }
    private bool IsKingSafeAfterMove(ChessPiece piece, Tile targetTile)
    {
        Tile originTile = ChessBoardManager.Instance.GetTile(piece.CurrentX, piece.CurrentY);
        bool kingDanger = true;
        //if enemy on tile, temp delete
        ChessPiece enemy = GetChessPiece(targetTile.CurrentX, targetTile.CurrentY);
        if (enemy != null && enemy.ChessPieceColor != piece.ChessPieceColor)
        {
            ChessPieceList.Remove(enemy);

            //save piece position
            int oldX = piece.CurrentX;
            int oldY = piece.CurrentY;
            //change piece position
            piece.CurrentX = targetTile.CurrentX;
            piece.CurrentY = targetTile.CurrentY;
            //save actual tilestate
            Tile.State saveStateOriginTile = originTile.TileState;
            //update tile state
            originTile.TileState = Tile.State.Free;
            //check if king still in danger
            GameObject.FindGameObjectWithTag("KingWhite").GetComponent<ChessPiece>().IsInDanger = false;
            GameObject.FindGameObjectWithTag("KingBlack").GetComponent<ChessPiece>().IsInDanger = false;
            UpdateAllPossibleMoves();
            //save result
            if (piece.ChessPieceColor == ChessPiece.Color.White)
            {
                kingDanger = GameObject.FindGameObjectWithTag("KingWhite").GetComponent<ChessPiece>().IsInDanger;
            }
            else
            {
                kingDanger = GameObject.FindGameObjectWithTag("KingBlack").GetComponent<ChessPiece>().IsInDanger;
            }
            //return to original positions               
            piece.CurrentX = oldX;
            piece.CurrentY = oldY;
            //reset tile state to old state
            originTile.TileState = saveStateOriginTile;
            //add deleted piece
            ChessPieceList.Add(enemy);

            UpdateAllPossibleMoves();
            return !kingDanger;
        }
        else
        {
            //save piece position
            int oldX = piece.CurrentX;
            int oldY = piece.CurrentY;
            //change piece position
            piece.CurrentX = targetTile.CurrentX;
            piece.CurrentY = targetTile.CurrentY;
            //save actual tilestate
            Tile.State saveStateOriginTile = originTile.TileState;
            Tile.State saveStateTargetTile = targetTile.TileState;
            //update tile state
            originTile.TileState = Tile.State.Free;
            targetTile.TileState = Tile.State.Occupied;
            //check if king still in danger
            GameObject.FindGameObjectWithTag("KingWhite").GetComponent<ChessPiece>().IsInDanger = false;
            GameObject.FindGameObjectWithTag("KingBlack").GetComponent<ChessPiece>().IsInDanger = false;
            UpdateAllPossibleMoves();
            //save result
            if (piece.ChessPieceColor == ChessPiece.Color.White)
            {
                kingDanger = GameObject.FindGameObjectWithTag("KingWhite").GetComponent<ChessPiece>().IsInDanger;
            }
            else
            {
                kingDanger = GameObject.FindGameObjectWithTag("KingBlack").GetComponent<ChessPiece>().IsInDanger;
            }
            //return to original positions               
            piece.CurrentX = oldX;
            piece.CurrentY = oldY;
            //update tile state
            originTile.TileState = saveStateOriginTile;
            targetTile.TileState = saveStateTargetTile;
            UpdateAllPossibleMoves();
            return !kingDanger;
        }
    }
    public void RemoveChessPiece(ChessPiece piece)
    {
        if(piece)
        {
            ChessPieceList.Remove(piece);
            Destroy(piece.gameObject);
            // Sound Destroyed Figur
            FindObjectOfType<AudioManager1>().Play("FigurDestroyed");
        }      
    }
    public void ChangePieceType(ChessPiece piece, ChessPiece.Type type)
    {
        //Delete old piece and spawn a new Piece with type
        //SpawnPiece(piece.CurrentX, piece.CurrentY, type, piece.ChessPieceColor);
        //RemoveChessPiece(piece);
        //change name
        piece.gameObject.name = type.ToString() + piece.ChessPieceColor.ToString();
        //change sprites
        Sprite newOriginalSprite = Resources.Load<Sprite>("Sprites/" + type.ToString() + " " + piece.ChessPieceColor.ToString());
        Sprite newHighlightSprite = Resources.Load<Sprite>("Sprites/HighlightedSprites/" + type.ToString() + " " + piece.ChessPieceColor.ToString() + " Highlight");
        piece.gameObject.GetComponent<SpriteRenderer>().sprite = newOriginalSprite;
        piece.OriginalSprite = newOriginalSprite;
        piece.HighlightSprite = newHighlightSprite;
        //change type
        piece.ChessPieceType = type;
        //change moveset
        Destroy(piece.gameObject.GetComponent<ChessPieceMovement>());
        piece.gameObject.AddComponent(Type.GetType(type.ToString()+"Movement"));
        //piece.UpdateMoves();
    }

    public void SetEnPassantVulnerable(ChessPiece piece, Tile tile)
    {
        //reset enpassant
        foreach(ChessPiece mPiece in ChessPieceList)
        {
            mPiece.EnPassantVulnerable = false;
        }
        //check to set
        if (piece.ChessPieceType == ChessPiece.Type.Pawn && piece.Unmoved && (piece.CurrentY + 2 == tile.CurrentY || piece.CurrentY - 2 == tile.CurrentY))
        {
            piece.EnPassantVulnerable = true;
        }
        else
        {
            piece.EnPassantVulnerable = false;
        }
    }
    public string GetGermanName(string s)
    {
        string chessPieceName = "";
        switch (s)
        {
            case "PawnWhite":
                chessPieceName = "Weißer Bauer";
                break;
            case "PawnBlack":
                chessPieceName = "Schwarzer Bauer";
                break;
            case "RookWhite":
                chessPieceName = "Weißer Turm";
                break;
            case "RookBlack":
                chessPieceName = "Schwarzer Turm";
                break;
            case "KnightWhite":
                chessPieceName = "Weißer Ritter";
                break;
            case "KnightBlack":
                chessPieceName = "Schwarzer Ritter";
                break;
            case "BishopWhite":
                chessPieceName = "Weißer Bischof";
                break;
            case "BishopBlack":
                chessPieceName = "Schwarzer Bischof";
                break;
            case "QueenWhite":
                chessPieceName = "Weiße Königin";
                break;
            case "QueenBlack":
                chessPieceName = "Schwarze Königin";
                break;
            case "BrokenQueenWhite":
                chessPieceName = "Weiße Königin (mit kaputten Schuhen)";
                break;
            case "BrokenQueenBlack":
                chessPieceName = "Schwarze Königin (mit kaputten Schuhen)";
                break;
            case "DrunkPawnWhite":
                chessPieceName = "Betrunkener weißer Bauer";
                break;
            case "DrunkPawnBlack":
                chessPieceName = "Betrunkener schwarzer Bauer";
                break;
            case "ReallyDrunkPawnWhite":
                chessPieceName = "Richtig betrunkener weißer Bauer";
                break;
            case "ReallyDrunkPawnBlack":
                chessPieceName = "Richtig betrunkener schwarzer Bauer";
                break;
            case "KingUpgradeWhite":
                chessPieceName = "Verbesserter weißer König";
                break;
            case "KingUpgradeBlack":
                chessPieceName = "Verbesserter schwarzer König";
                break;
            case "HarleyWhite":
                chessPieceName = "Weiße Harley";
                break;
            case "HarleyBlack":
                chessPieceName = "schwarze Harley";
                break;
            default:
                Console.WriteLine("Default case");
                break;
        }
        return chessPieceName;
    }

}
