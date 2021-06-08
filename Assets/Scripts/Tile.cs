using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//to prevent mouseclicks going trough pause menu
using UnityEngine.EventSystems;

public class Tile : MonoBehaviour
{
    #region variables
    public int CurrentX;
    public int CurrentY;
    public State TileState;
    public Color color;

    public Sprite OriginalSprite;
    public Sprite CurrentSprite;
    public Sprite DisabledSprite;
    public Sprite MouseOverSprite;
    public Sprite MouseOverHighlightSprite;
    public Sprite HighlightSprite;
    #endregion
    public enum State
    {
        Free,
        Occupied,
        Obstacle,
        Disabled,
    }
    public enum Color
    {
        White,
        Black
    }
    private void Awake()
    {
        CurrentSprite = OriginalSprite;
    }
    private void OnMouseOver()
    {
        if(!EventSystem.current.IsPointerOverGameObject())
        {
            if (CurrentSprite == HighlightSprite)
                gameObject.GetComponent<SpriteRenderer>().sprite = MouseOverHighlightSprite;
            else if (TileState == State.Disabled)
                return;
            else
                gameObject.GetComponent<SpriteRenderer>().sprite = MouseOverSprite;
        }
        
    }
    private void OnMouseExit()
    {
        if (!EventSystem.current.IsPointerOverGameObject())
            gameObject.GetComponent<SpriteRenderer>().sprite = CurrentSprite;
    }
    private void OnMouseDown()
    {
        if (!EventSystem.current.IsPointerOverGameObject())
            SessionManager.Instance.HandleMouseClick(this);
    }
    //to prevent stuck highlight after event canvas
    public void CancelMouseOverHighlight()
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = CurrentSprite;
    }
    public void Highlight(Sprite sprite)
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = sprite;
        CurrentSprite = sprite;
    }
    public void CancelHighlight()
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = OriginalSprite;
        CurrentSprite = OriginalSprite;
    }
    public void ChangeColorTo(Color color)
    {
        if(TileState != State.Disabled)
        {
            if (color == Color.Black)
            {
                OriginalSprite = Resources.Load<Sprite>(@"Sprites/White Tile");
                if (TileState == State.Free || TileState == State.Occupied)
                    CurrentSprite = OriginalSprite;
                MouseOverSprite = Resources.Load<Sprite>(@"Sprites/White Tile Selected");
                GetComponent<SpriteRenderer>().sprite = OriginalSprite;
                GetComponent<SpriteRenderer>().color = new UnityEngine.Color(1,1,1, 75f/255f);

            }
            else
            {
                OriginalSprite = Resources.Load<Sprite>(@"Sprites/White Tile");
                if (TileState == State.Free || TileState == State.Occupied)
                    CurrentSprite = OriginalSprite;
                MouseOverSprite = Resources.Load<Sprite>(@"Sprites/White Tile Selected");
                GetComponent<SpriteRenderer>().sprite = OriginalSprite;
                GetComponent<SpriteRenderer>().color = new UnityEngine.Color(1, 1, 1, 166f/255f);
            }
        }        
    }

    public void Disable()
    {
        TileState = State.Disabled;
        CurrentSprite = DisabledSprite;
        GetComponent<SpriteRenderer>().sprite = CurrentSprite;
    }
    public void Enable()
    {
        //check if chesspiece on wall
        if(ChessPieceManager.Instance.GetChessPiece(CurrentX, CurrentY))
        {
            TileState = State.Occupied;
        }
        else
        {
            TileState = State.Free;
        }        
        CurrentSprite = OriginalSprite;
        GetComponent<SpriteRenderer>().sprite = CurrentSprite;
    }
}
