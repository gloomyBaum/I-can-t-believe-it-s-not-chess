using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SessionManager : MonoBehaviour
{
    #region variables
    public static SessionManager Instance;
    private ChessBoardManager board;
    private ChessPieceManager pieceManager;
    private RandomEventSystem eventSystem;
    
    private ChessPiece selectedChessPiece;
    private bool _isWhiteTurn;

    private Scenario _currentScenario;
    private int _turnCounter;

    //for UI
    private string lastPieceDestroyed = " \n ";
    #endregion

    #region properties
    public Scenario CurrentScenario
    {
        get { return _currentScenario; }
    }
    public int TurnCounter
    {
        get { return _turnCounter; }
    }
    public bool IsWhiteTurn
    {
        get { return _isWhiteTurn; }
    }
    #endregion

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }
    private void Start()
    {        
        StartNewGame();
    }

    public void StartNewGame()
    {
        _turnCounter = 1;
        _isWhiteTurn = true;

        _currentScenario = GameManager.Instance.currentScenario;

        this.board = ChessBoardManager.Instance;
        this.pieceManager = ChessPieceManager.Instance;
        this.eventSystem = RandomEventSystem.Instance;

        board.Setup(_currentScenario);
        pieceManager.Setup(_currentScenario);
        eventSystem.OnSessionStart();

        pieceManager.UpdateAllPossibleMoves();

    }
    public void HandleMouseClick(Tile clickedTile)
    {
        //handle chesspiece selection
        if(CheckForFriendlyPiece(clickedTile))
        {
            SelectPiece(clickedTile);
        }
        //validate move
        else if(selectedChessPiece && pieceManager.ValidateMove(selectedChessPiece, clickedTile))
        {
            //Handle special move
            if(selectedChessPiece.PossibleSpecialMoves.ContainsKey(clickedTile))
            {
                HandleSpecialMove(selectedChessPiece, clickedTile, selectedChessPiece.PossibleSpecialMoves[clickedTile]);
            }
            else if(clickedTile.TileState == Tile.State.Free)
            {
                MoveToTile(selectedChessPiece, clickedTile);
            }
            else if (CheckForEnemyPiece(clickedTile))
            {
                AttackTile(selectedChessPiece, clickedTile);
                MoveToTile(selectedChessPiece, clickedTile);
            }
        }      
        else
        {
            DeSelectPiece();
        }
    }
    #region some check helper
    private bool CheckIfInPossibleSpecialMoves(Tile tile)
    {
        //todo
        return false;
    }
    private bool CheckForFriendlyPiece(Tile tile)
    {
        return (tile.TileState == Tile.State.Occupied && pieceManager.GetChessPiece(tile.CurrentX, tile.CurrentY).ChessPieceColor == ChessPiece.Color.White == _isWhiteTurn || (tile.TileState == Tile.State.Disabled && pieceManager.GetChessPiece(tile.CurrentX, tile.CurrentY) != null && pieceManager.GetChessPiece(tile.CurrentX, tile.CurrentY).ChessPieceColor == ChessPiece.Color.White == _isWhiteTurn));
    }
    private bool CheckForEnemyPiece(Tile tile)
    {
        return (tile.TileState == Tile.State.Occupied && pieceManager.GetChessPiece(tile.CurrentX, tile.CurrentY).ChessPieceColor == ChessPiece.Color.White != _isWhiteTurn);
    }
    #endregion
    private void SelectPiece(Tile tile)
    {
        //Delete old Highlights
        if (selectedChessPiece)
            board.CancelHighlightAttackedTiles();

        
        //select
        selectedChessPiece = pieceManager.GetChessPiece(tile.CurrentX, tile.CurrentY);
        //hier update?
        selectedChessPiece.UpdateMoves();

        //highlight selected piece
        pieceManager.HighlightChessPiece(selectedChessPiece);
        //Highlight all PossibleMoves for the selected piece
        board.HighlightAttackedTiles(selectedChessPiece);
    }
    private void DeSelectPiece()
    {
        board.CancelHighlightAttackedTiles();
        selectedChessPiece = null;
    }
    private void MoveToTile(ChessPiece piece, Tile tile)
    {        
        board.CancelHighlightAttackedTiles();
        if(board.GetTile(piece.CurrentX, piece.CurrentY).TileState != Tile.State.Disabled)
            board.GetTile(piece.CurrentX, piece.CurrentY).TileState = Tile.State.Free;
        //cancel highlight on piece
        pieceManager.CancelHighlightChessPiece(selectedChessPiece);

        //EnPassant
        pieceManager.SetEnPassantVulnerable(piece, tile);

        //Showcase Details in UI
        UpdateUIZuege(piece, tile);

        pieceManager.MoveChessPieceTo(piece, tile);

        //Play Sound
        FindObjectOfType<AudioManager1>().Play("FigurMoved");

        piece.Unmoved = false;
        piece = null;        

        //call event system
        eventSystem.UpdateAllEvents();

        //update List of all Moves
        pieceManager.UpdateAllPossibleMoves();

        //todo
        //mark checked king

        //check for end game
        if(CheckForEndGame())
        {
			EndGame();
        }

        _isWhiteTurn = !_isWhiteTurn;

        //increment turn
        _turnCounter++;
    }
    private void AttackTile(ChessPiece piece, Tile tile)
    {
        ChessPiece targetChessPiece = pieceManager.GetChessPiece(tile.CurrentX, tile.CurrentY);
        //in case really drunk pawn falls on king
        if(targetChessPiece.ChessPieceType == ChessPiece.Type.King || targetChessPiece.ChessPieceType == ChessPiece.Type.KingUpgrade)
        {
            EndGame();
        } 
        //ANzeige geschlagener Figur
        lastPieceDestroyed = pieceManager.GetGermanName(targetChessPiece.gameObject.name) + " wurde zerstört";
        // Ende anzeige
        pieceManager.RemoveChessPiece(targetChessPiece);
    }
    private void HandleSpecialMove(ChessPiece piece, Tile tile, ChessPieceMovement.SpecialMoves special)
    {
        //Handle EnPassant
        if(special == ChessPieceMovement.SpecialMoves.EnPassant)
        {
            if(piece.ChessPieceColor == ChessPiece.Color.White)
            {
                AttackTile(piece, board.GetTile(tile.CurrentX, tile.CurrentY - 1));
                MoveToTile(piece, tile);
                board.GetTile(tile.CurrentX, tile.CurrentY - 1).TileState = Tile.State.Free;
            }
            else
            {
                AttackTile(piece, board.GetTile(tile.CurrentX, tile.CurrentY + 1));
                MoveToTile(piece, tile);
                board.GetTile(tile.CurrentX, tile.CurrentY + 1).TileState = Tile.State.Free;
            }
        }
        //Handle Promotion
        else if(special == ChessPieceMovement.SpecialMoves.Promotion)
        {
            if(CheckForEnemyPiece(tile))
            {
                AttackTile(piece, tile);
                pieceManager.ChangePieceType(piece, ChessPiece.Type.Queen);               
                MoveToTile(piece, tile);
            }
            else
            {
                pieceManager.ChangePieceType(piece, ChessPiece.Type.Queen);
                MoveToTile(piece, tile);                
            }
        }
        else if(special == ChessPieceMovement.SpecialMoves.Rochade)
        {
            if(IsWhiteTurn)
            {
                //große Rochade
                if (tile.CurrentX == piece.CurrentX - 2)
                {
                    ChessPiece leftRook = pieceManager.GetChessPiece(0, 0);
                    if(leftRook)
                    {
                        pieceManager.MoveChessPieceTo(leftRook, board.GetTile(tile.CurrentX + 1, 0));
                        MoveToTile(piece, tile);
                    }
                }
                //kleine Rochade
                else
                {
                    ChessPiece rightRook = pieceManager.GetChessPiece(board.XSize - 1, 0);
                    if (rightRook)
                    {
                        pieceManager.MoveChessPieceTo(rightRook, board.GetTile(tile.CurrentX - 1, 0));
                        MoveToTile(piece, tile);
                    }
                }
            }
            else if (!IsWhiteTurn)
            {
                //große Rochade
                if (tile.CurrentX == piece.CurrentX - 2)
                {
                    ChessPiece leftRook = pieceManager.GetChessPiece(0, board.YSize - 1);
                    if (leftRook)
                    {
                        pieceManager.MoveChessPieceTo(leftRook, board.GetTile(tile.CurrentX + 1, board.YSize - 1));
                        MoveToTile(piece, tile);
                    }
                }
                //kleine Rochade
                else
                {
                    ChessPiece rightRook = pieceManager.GetChessPiece(board.XSize - 1, board.YSize - 1);
                    if (rightRook)
                    {
                        pieceManager.MoveChessPieceTo(rightRook, board.GetTile(tile.CurrentX - 1, board.YSize - 1));
                        MoveToTile(piece, tile);
                    }
                }
            }
        }
        else if (special == ChessPieceMovement.SpecialMoves.ReallyDrunk)
        {
            //make random number 0-8 to randomly choose surrounding tiles of piece, starting from top left to mid
            System.Random rnd = eventSystem.RandomIndexGenerator;
            int tmp = rnd.Next(0, 8);
            Tile drunkTargetTile;

            switch (tmp)
            {
                case 0:
                    drunkTargetTile = board.GetTile(piece.CurrentX - 1, piece.CurrentY + 1);
                    break;
                case 1:
                    drunkTargetTile = board.GetTile(piece.CurrentX, piece.CurrentY + 1);
                    break;
                case 2:
                    drunkTargetTile = board.GetTile(piece.CurrentX + 1, piece.CurrentY + 1);
                    break;
                case 3:
                    drunkTargetTile = board.GetTile(piece.CurrentX + 1, piece.CurrentY);
                    break;
                case 4:
                    drunkTargetTile = board.GetTile(piece.CurrentX + 1, piece.CurrentY - 1);
                    break;
                case 5:
                    drunkTargetTile = board.GetTile(piece.CurrentX, piece.CurrentY - 1);
                    break;
                case 6:
                    drunkTargetTile = board.GetTile(piece.CurrentX - 1, piece.CurrentY - 1);
                    break;
                case 7:
                    drunkTargetTile = board.GetTile(piece.CurrentX - 1, piece.CurrentY);
                    break;
                default:
                    drunkTargetTile = board.GetTile(piece.CurrentX, piece.CurrentY);
                    break;
            }

            if (drunkTargetTile)
            {
                if (drunkTargetTile.TileState == Tile.State.Occupied)
                {
                    AttackTile(piece, drunkTargetTile);
                    MoveToTile(piece, drunkTargetTile);
                }
                else if (drunkTargetTile.TileState == Tile.State.Free)
                {
                    MoveToTile(piece, drunkTargetTile);
                }
            }
            else
            {
                MoveToTile(piece, tile);
            }
        }

    }

    //update information to show on UI
    private void UpdateUIZuege(ChessPiece piece, Tile tile)
    {
        string name = pieceManager.GetGermanName(piece.gameObject.name);
        string oldPosition = (piece.CurrentX + 1) + ", " + (piece.CurrentY + 1);
        string newPosition = (tile.CurrentX + 1) + ", " + (tile.CurrentY + 1);

        //Anzeige Zuege
        if (!IsWhiteTurn)
        {
            GameObject.Find("ZuegeAnzeige").GetComponent<UI_ZuegeAnzeige>().SetText( name + "\n"+  " von" + "\n" + oldPosition + "\n"+ " nach " + "\n" + newPosition + "\n" + lastPieceDestroyed+ "\n"+ "\n" + "Weißer Spieler ist am Zug!");
        }
        else
        {
            GameObject.Find("ZuegeAnzeige").GetComponent<UI_ZuegeAnzeige>().SetText(name + "\n" + " von" + "\n" + oldPosition + "\n" + " nach " + "\n" + newPosition + "\n" + lastPieceDestroyed + "\n" +"\n" + "Schwarzer Spieler ist an der Reihe!");
        }
        lastPieceDestroyed = "\n ";
    }
    private bool CheckForEndGame()
    {
        bool end = true;
        if (!IsWhiteTurn)
        {
            //make deep copy to evade invalidOPexception
            List<ChessPiece> tmp = new List<ChessPiece>();
            foreach (ChessPiece piece in pieceManager.ChessPieceList)
            {
                if (piece.ChessPieceColor == ChessPiece.Color.White)
                {
                    tmp.Add(piece);
                }
            }
            foreach (ChessPiece piece in tmp)
            {
                    foreach (Tile tile in piece.PossibleMoves)
                    {
                        if (pieceManager.ValidateMove(piece, tile))
                        {
                            end = false;
                        }
                    }
                
            }
            return end;
        }
        else if (IsWhiteTurn)
        {
            //make deep copy to evade invalidOPexception
            List<ChessPiece> tmp = new List<ChessPiece>();
            foreach (ChessPiece piece in pieceManager.ChessPieceList)
            {
                if (piece.ChessPieceColor == ChessPiece.Color.Black)
                {
                    tmp.Add(piece);
                }
            }
            foreach (ChessPiece piece in tmp)
            {
                foreach (Tile tile in piece.PossibleMoves)
                {
                    if (pieceManager.ValidateMove(piece, tile))
                    {
                        end = false;
                    }
                }

            }
            return end;
        }
        return false;
    }

    private void EndGame()
    {
        if (IsWhiteTurn)
        {
            Debug.Log("Black won");
            FindObjectOfType<AudioManager1>().Play("VictorySound");
            GameObject.Find("WinCanvas").GetComponent<WinView>().ShowBlackWin();
        }
        else
        {
            Debug.Log("White won");
            FindObjectOfType<AudioManager1>().Play("VictorySound");
            GameObject.Find("WinCanvas").GetComponent<WinView>().ShowWhiteWin();
        }
    }

}
