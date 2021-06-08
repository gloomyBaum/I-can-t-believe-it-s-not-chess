using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Scenario
{
	#region variables
	private int _xSize;
	private int _ySize;
	private List<FormationUnit> _chessPieceFormation;
	private List<RandomEvent> _possibleEvents;
	private List<Vector2Int> _disabledTiles;
	private int _maxEventInterval;
	#endregion

	#region properties
	public int XSize
	{
		get { return _xSize; }
		protected set { _xSize = value; }
	}
	public int YSize
	{
		get { return _ySize; }
		protected set { _ySize = value; }
	}
	public List<FormationUnit> ChessPieceFormation
	{
		get { return _chessPieceFormation; }
		protected set { _chessPieceFormation = value; }
	}
	public List<RandomEvent> PossibleEvents
	{
		get { return _possibleEvents; }
		protected set { _possibleEvents = value; }
	}
	public List<Vector2Int> DisabledTiles
	{
		get { return _disabledTiles; }
		protected set { _disabledTiles = value; }
	}
	public int MaxEventInterval
	{
		get { return _maxEventInterval; }
		protected set { _maxEventInterval = value; }
	}

	#endregion

	protected virtual List<FormationUnit> MakeChessPieceFormation()
	{
		List<FormationUnit> formation = new List<FormationUnit>();
		//white team
		formation.Add(new FormationUnit(ChessPiece.Type.Rook, ChessPiece.Color.White, 0, 0));
		formation.Add(new FormationUnit(ChessPiece.Type.Knight, ChessPiece.Color.White, 1, 0));
		formation.Add(new FormationUnit(ChessPiece.Type.Bishop, ChessPiece.Color.White, 2, 0));
		formation.Add(new FormationUnit(ChessPiece.Type.Queen, ChessPiece.Color.White, 3, 0));
		formation.Add(new FormationUnit(ChessPiece.Type.King, ChessPiece.Color.White, 4, 0));
		formation.Add(new FormationUnit(ChessPiece.Type.Bishop, ChessPiece.Color.White, 5, 0));
		formation.Add(new FormationUnit(ChessPiece.Type.Knight, ChessPiece.Color.White, 6, 0));
		formation.Add(new FormationUnit(ChessPiece.Type.Rook, ChessPiece.Color.White, 7, 0));
		for (int i = 0; i < XSize; i++)
		{
			formation.Add(new FormationUnit(ChessPiece.Type.Pawn, ChessPiece.Color.White, i, 1));
		}
		//black team
		formation.Add(new FormationUnit(ChessPiece.Type.Rook, ChessPiece.Color.Black, 0, XSize - 1));
		formation.Add(new FormationUnit(ChessPiece.Type.Knight, ChessPiece.Color.Black, 1, XSize - 1));
		formation.Add(new FormationUnit(ChessPiece.Type.Bishop, ChessPiece.Color.Black, 2, XSize - 1));
		formation.Add(new FormationUnit(ChessPiece.Type.Queen, ChessPiece.Color.Black, 3, XSize - 1));
		formation.Add(new FormationUnit(ChessPiece.Type.King, ChessPiece.Color.Black, 4, XSize - 1));
		formation.Add(new FormationUnit(ChessPiece.Type.Bishop, ChessPiece.Color.Black, 5, XSize - 1));
		formation.Add(new FormationUnit(ChessPiece.Type.Knight, ChessPiece.Color.Black, 6, XSize - 1));
		formation.Add(new FormationUnit(ChessPiece.Type.Rook, ChessPiece.Color.Black, 7, XSize - 1));
		for (int i = 0; i < XSize; i++)
		{
			formation.Add(new FormationUnit(ChessPiece.Type.Pawn, ChessPiece.Color.Black, i, XSize - 2));
		}

		return formation;
	}
	protected virtual List<Vector2Int> MakeDisabledTileList()
	{
		List<Vector2Int> list = new List<Vector2Int>();
		return list;
	}

	protected virtual List<RandomEvent> MakePossibleEventList()
	{
		List<RandomEvent> list = new List<RandomEvent>();

        list.Add(new ReplaceAllChessPiecesOfType(6, "Harley Davidson", "Alle Springer erhalten ein temporäres Upgrade! Ihre Beine werden für einen Zug durch Reifen ersetzt.", ChessPiece.Type.Knight, ChessPiece.Type.Harley, true));
        list.Add(new ReplaceAllChessPiecesOfType(2, "High Heels Fiasko", "Durch den unebenen Boden haben sich alle Damen den HighHeel abgebrochen. Sie sind Schlecht gelaunt und können sich 2 Züge nicht bewegen.", ChessPiece.Type.Queen, ChessPiece.Type.BrokenQueen, true));
        list.Add(new ReplaceAllChessPiecesOfType(8, "Oktoberfest", "Alle Bauern vergnügen sich auf dem Oktoberfest in der Nähe. Sie können nun einige Züge nicht mehr geradeaus laufen!", ChessPiece.Type.Pawn, ChessPiece.Type.DrunkPawn, true));
        list.Add(new ReplaceAllChessPiecesOfType(6, "Freibier", "Bauern vergnügten sich bei einem lokalen #RettetDieWale Event wo es Freibier gab. Ihre Bewegungen sind in nächster Zeit nicht ganz wie erwartet.", ChessPiece.Type.Pawn, ChessPiece.Type.ReallyDrunkPawn, true));

        //läuft nicht ab
        list.Add(new ReplaceAllChessPiecesOfType(0, "Für den König!", "Die Könige haben nun auch bemerkt das dies kein normales Schachfeld ist. Um mitzuhalten können sie nun doppelt so weit laufen.", ChessPiece.Type.King, ChessPiece.Type.KingUpgrade, false));

        list.Add(new Nothing());
        list.Add(new Meteor(5));
        list.Add(new SimpleBoardSizeEvent(50, "Seidenstraße", "Der Weg nach China im Osten ist jetzt sicher.", BoardRandomEvent.ExtensionDirection.East, 2));
        list.Add(new SimpleBoardSizeEvent(50, "Christopher Columbus", "Im Westen ist jetzt Land begehbar.", BoardRandomEvent.ExtensionDirection.West, 2));
        list.Add(new Baumaßnahme(2, 50));


        list.Add(new KleineMauer(10, 20, KleineMauer.Orientation.Horizontal));
        list.Add(new KleineMauer(5, 20, KleineMauer.Orientation.Horizontal));
        list.Add(new KleineMauer(10, 20, KleineMauer.Orientation.Vertical));
        list.Add(new KleineMauer(5, 20, KleineMauer.Orientation.Vertical));

        list.Add(new Aufforstung(6, 20));

        list.Add(new SpawnChessPieceAtRandomPositionEvent(5, ChessPiece.Type.Pawn));
        list.Add(new SpawnChessPieceAtRandomPositionEvent(10, ChessPiece.Type.Knight));
        list.Add(new SpawnChessPieceAtRandomPositionEvent(10, ChessPiece.Type.Bishop));
        list.Add(new SpawnChessPieceAtRandomPositionEvent(10, ChessPiece.Type.Rook));
        list.Add(new SpawnChessPieceAtRandomPositionEvent(50, ChessPiece.Type.Queen));

        return list;
	}
}

//Helper Class
public class FormationUnit
{
	public ChessPiece.Type Type;
	public ChessPiece.Color Color;
	public int X;
	public int Y;
	public FormationUnit(ChessPiece.Type type, ChessPiece.Color color, int x, int y)
	{
		Type = type;
		Color = color;
		X = x;
		Y = y;
	}
}

