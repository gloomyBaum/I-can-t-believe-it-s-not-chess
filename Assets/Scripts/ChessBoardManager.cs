using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChessBoardManager : MonoBehaviour
{
    #region variables
    public static ChessBoardManager Instance;
    private ChessPieceManager pieceManager;
    public List<Tile> TileList;
    private List<Tile> HighlightedList;

    private int _xSize;
    private int _ySize;

    private const float LAYER_HEIGHT = -0.1f;
    #endregion

    #region properties
    public int XSize
    {
        get { return _xSize; }
    }

    public int YSize
    {
        get { return _ySize; }
    }
    #endregion

    private void Awake()
    {
        if (Instance == null)
            Instance = this;        
    }

    public void Setup(Scenario currentScenario)
    {
        
        TileList = new List<Tile>();
        HighlightedList = new List<Tile>();
        pieceManager = ChessPieceManager.Instance;
        //update Properties
        _xSize = currentScenario.XSize;
        _ySize = currentScenario.YSize;
        //Build Board
        for (int i = 0; i < _xSize; i++)
        {
            for (int j = 0; j < _ySize; j++)
            {
                if (i % 2 == 0)
                {
                    if (j % 2 == 0)
                    {
                        SpawnTile(i, j, Tile.Color.Black);
                    }
                    else
                    {
                        SpawnTile(i, j, Tile.Color.White);
                    }
                }
                else
                {
                    if (j % 2 == 0)
                    {
                        SpawnTile(i, j, Tile.Color.White);
                    }
                    else
                    {
                        SpawnTile(i, j, Tile.Color.Black);
                    }
                }

            }
        }
        //Disable given Tiles
        foreach(Tile tile in TileList)
        {
            if(currentScenario.DisabledTiles.Contains(new Vector2Int(tile.CurrentX, tile.CurrentY)))
            {
                tile.Disable();
            }
        }
    }
    private void SpawnTile(int x, int y, Tile.Color color)
    {
        GameObject tile;
        if (color == Tile.Color.Black)
        {
            tile = Instantiate(Resources.Load("Prefabs/Tile Black")) as GameObject;
        }
        else
        {
            tile = Instantiate(Resources.Load("Prefabs/Tile White")) as GameObject;
        }
        //set position and update position
        tile.transform.position = new Vector3(x, LAYER_HEIGHT, y);
        tile.GetComponent<Tile>().CurrentX = x;
        tile.GetComponent<Tile>().CurrentY = y;

        //make shiny
        tile.name = "[" + x + "," + y + "]";
        tile.transform.parent = GameObject.Find("ChessBoard").transform;

        //update state
        tile.GetComponent<Tile>().TileState = Tile.State.Free;

        //add to List
        TileList.Add(tile.GetComponent<Tile>());
    }
    public Tile GetTile(int x, int y)
    {
        foreach (Tile tile in TileList)
        {
            if (tile.GetComponent<Tile>().CurrentX == x && tile.GetComponent<Tile>().CurrentY == y)
                return tile;
        }
        return null;
    }

    #region TileHighlights
    public void HighlightAttackedTiles(ChessPiece piece)
    {
        foreach (Tile tile in piece.PossibleMoves)
        {
            if(pieceManager.ValidateMove(piece, tile))
            {
                tile.Highlight(tile.HighlightSprite);
                HighlightedList.Add(tile);
            }           
        }
        foreach (Tile tile in piece.PossibleSpecialMoves.Keys)
        {
            if (pieceManager.ValidateMove(piece, tile))
            {
                tile.Highlight(tile.HighlightSprite);
                HighlightedList.Add(tile);
            }
        }
    }
    public void CancelHighlightAttackedTiles()
    {
        foreach (Tile tile in HighlightedList)
        {
            tile.CancelHighlight();
        }
        HighlightedList.Clear();
    }
    #endregion

    #region eventstuff
    public void AddColumns(int number, int position)
    {
        for (int i = 0; i < number; i++)
        {
            AddColumn(position);
        }
        
    }
    public void AddColumn(int position)
    {
        foreach (Tile tile in TileList)
        {
            if (tile.CurrentX >= position)
            {
                ChessPiece currentPiece = pieceManager.GetChessPiece(tile.CurrentX, tile.CurrentY);
                if (currentPiece)
                {
                    currentPiece.transform.position = new Vector3(currentPiece.CurrentX + 1, 0, currentPiece.CurrentY);
                }
                tile.CurrentX += 1;
                tile.gameObject.transform.position = new Vector3(tile.CurrentX, LAYER_HEIGHT, tile.CurrentY);
            }
        }
        //Update Positions of ChessPieces
        foreach (ChessPiece piece in pieceManager.ChessPieceList)
        {
            piece.CurrentX = (int)piece.gameObject.transform.position.x;
            piece.CurrentY = (int)piece.gameObject.transform.position.z;
        }
        for (int i = 0; i < YSize; i++)
        {
            SpawnTile(position, i, Tile.Color.Black);
        }
        _xSize++;
        //Board neu streichen
        foreach (Tile tile in TileList)
        {
            if (tile.CurrentX % 2 == 0)
            {
                if (tile.CurrentY % 2 == 0)
                {
                    tile.ChangeColorTo(Tile.Color.Black);
                }
                else
                {
                    tile.ChangeColorTo(Tile.Color.White);
                }
            }
            else
            {
                if (tile.CurrentY % 2 == 0)
                {
                    tile.ChangeColorTo(Tile.Color.White);
                }
                else
                {
                    tile.ChangeColorTo(Tile.Color.Black);
                }
            }
        }
    }
    public void AddRows(int number, int position)
    {
        for (int i = 0; i < number; i++)
        {
            AddRow(position);
        }
        
    }
    public void AddRow(int position)
    {
        foreach (Tile tile in TileList)
        {
            if (tile.CurrentY >= position)
            {
                ChessPiece currentPiece = pieceManager.GetChessPiece(tile.CurrentX, tile.CurrentY);
                if (currentPiece)
                {
                    currentPiece.transform.position = new Vector3(currentPiece.CurrentX, 0, currentPiece.CurrentY + 1);
                }
                tile.CurrentY += 1;
                tile.gameObject.transform.position = new Vector3(tile.CurrentX, LAYER_HEIGHT, tile.CurrentY);
            }
        }
        //Update Positions of ChessPieces
        foreach (ChessPiece piece in pieceManager.ChessPieceList)
        {
            piece.CurrentX = (int)piece.gameObject.transform.position.x;
            piece.CurrentY = (int)piece.gameObject.transform.position.z;
        }
        for (int i = 0; i < XSize; i++)
        {
            SpawnTile(i, position, Tile.Color.Black);
        }
        _ySize++;
        //Board neu streichen
        foreach (Tile tile in TileList)
        {
            if (tile.CurrentX % 2 == 0)
            {
                if (tile.CurrentY % 2 == 0)
                {
                    tile.ChangeColorTo(Tile.Color.Black);
                }
                else
                {
                    tile.ChangeColorTo(Tile.Color.White);
                }
            }
            else
            {
                if (tile.CurrentY % 2 == 0)
                {
                    tile.ChangeColorTo(Tile.Color.White);
                }
                else
                {
                    tile.ChangeColorTo(Tile.Color.Black);
                }
            }
        }
    }
    #endregion

    }
