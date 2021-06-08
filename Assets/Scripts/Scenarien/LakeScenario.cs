using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LakeScenario : Scenario
{
	public LakeScenario()
	{
		XSize = 10;
		YSize = 10;
		ChessPieceFormation = MakeChessPieceFormation();
		PossibleEvents = MakePossibleEventList();
		DisabledTiles = MakeDisabledTileList();
		MaxEventInterval = 11;
	}
	protected override List<FormationUnit> MakeChessPieceFormation()
	{
		List<FormationUnit> formation = new List<FormationUnit>();
		//white team
		formation.Add(new FormationUnit(ChessPiece.Type.Rook, ChessPiece.Color.White, 0, 0));
		formation.Add(new FormationUnit(ChessPiece.Type.Knight, ChessPiece.Color.White, 1, 0));
		formation.Add(new FormationUnit(ChessPiece.Type.Bishop, ChessPiece.Color.White, 2, 0));
		formation.Add(new FormationUnit(ChessPiece.Type.Pawn, ChessPiece.Color.White, 3, 0));
		formation.Add(new FormationUnit(ChessPiece.Type.Queen, ChessPiece.Color.White, 4, 0));
		formation.Add(new FormationUnit(ChessPiece.Type.King, ChessPiece.Color.White, 5, 0));
		formation.Add(new FormationUnit(ChessPiece.Type.Pawn, ChessPiece.Color.White, 6, 0));
		formation.Add(new FormationUnit(ChessPiece.Type.Bishop, ChessPiece.Color.White, 7, 0));
		formation.Add(new FormationUnit(ChessPiece.Type.Knight, ChessPiece.Color.White, 8, 0));
		formation.Add(new FormationUnit(ChessPiece.Type.Rook, ChessPiece.Color.White, 9, 0));
		for (int i = 0; i < XSize; i++)
		{
			formation.Add(new FormationUnit(ChessPiece.Type.Pawn, ChessPiece.Color.White, i, 1));
		}
		//black team
		formation.Add(new FormationUnit(ChessPiece.Type.Rook, ChessPiece.Color.Black, 0, XSize - 1));
		formation.Add(new FormationUnit(ChessPiece.Type.Knight, ChessPiece.Color.Black, 1, XSize - 1));
		formation.Add(new FormationUnit(ChessPiece.Type.Bishop, ChessPiece.Color.Black, 2, XSize - 1));
		formation.Add(new FormationUnit(ChessPiece.Type.Pawn, ChessPiece.Color.Black, 3, XSize - 1));
		formation.Add(new FormationUnit(ChessPiece.Type.Queen, ChessPiece.Color.Black, 4, XSize - 1));
		formation.Add(new FormationUnit(ChessPiece.Type.King, ChessPiece.Color.Black, 5, XSize - 1));
		formation.Add(new FormationUnit(ChessPiece.Type.Pawn, ChessPiece.Color.Black, 6, XSize - 1));
		formation.Add(new FormationUnit(ChessPiece.Type.Bishop, ChessPiece.Color.Black, 7, XSize - 1));
		formation.Add(new FormationUnit(ChessPiece.Type.Knight, ChessPiece.Color.Black, 8, XSize - 1));
		formation.Add(new FormationUnit(ChessPiece.Type.Rook, ChessPiece.Color.Black, 9, XSize - 1));
		for (int i = 0; i < XSize; i++)
		{
			formation.Add(new FormationUnit(ChessPiece.Type.Pawn, ChessPiece.Color.Black, i, XSize - 2));
		}

		return formation;
	}
	protected override List<Vector2Int> MakeDisabledTileList()
	{
		List<Vector2Int> list = new List<Vector2Int>();

		list.Add(new Vector2Int(5, 3));

		list.Add(new Vector2Int(3, 4));
		list.Add(new Vector2Int(4, 4));
		list.Add(new Vector2Int(5, 4));
		list.Add(new Vector2Int(6, 4));

		list.Add(new Vector2Int(6, 5));
		list.Add(new Vector2Int(5, 5));
		list.Add(new Vector2Int(4, 5));
		list.Add(new Vector2Int(3, 5));

		list.Add(new Vector2Int(4, 6));

		return list;
	}
	protected override List<RandomEvent> MakePossibleEventList()
	{
        List<RandomEvent> list = new List<RandomEvent>();

        list.Add(new ReplaceAllChessPiecesOfType(6, "Harley Davidson", "Alle Springer erhalten ein temporäres Upgrade! Ihre Beine werden für einen Zug durch Reifen ersetzt.", ChessPiece.Type.Knight, ChessPiece.Type.Harley, true));
        list.Add(new ReplaceAllChessPiecesOfType(2, "High Heels Fiasko", "Durch den unebenen Boden haben sich alle Damen den HighHeel abgebrochen. Sie sind Schlecht gelaunt und können sich 2 Züge nicht bewegen.", ChessPiece.Type.Queen, ChessPiece.Type.BrokenQueen, true));
        list.Add(new ReplaceAllChessPiecesOfType(8, "Oktoberfest", "Alle Bauern vergnügen sich auf dem Oktoberfest in der Nähe. Sie können nun einige Züge nicht mehr geradeaus laufen!", ChessPiece.Type.Pawn, ChessPiece.Type.DrunkPawn, true));
        list.Add(new ReplaceAllChessPiecesOfType(6, "Freibier", "Bauern vergnügten sich bei einem lokalen #RettetDieWale Event wo es Freibier gab. Ihre Bewegungen sind in nächster Zeit nicht ganz wie erwartet.", ChessPiece.Type.Pawn, ChessPiece.Type.ReallyDrunkPawn, true));

        //läuft nicht ab
        list.Add(new ReplaceAllChessPiecesOfType(0, "Für den König!", "Die Könige haben nun auch bemerkt das dies kein normales Schachfeld ist. Um mitzuhalten können sie nun doppelt so weit laufen.", ChessPiece.Type.King, ChessPiece.Type.KingUpgrade, false));

        list.Add(new Nothing());
        list.Add(new Meteor(5, 20));
        list.Add(new SimpleBoardSizeEvent(25, "Seidenstraße", "Der Weg nach China im Osten ist jetzt sicher.", BoardRandomEvent.ExtensionDirection.East, 2));
        list.Add(new SimpleBoardSizeEvent(25, "Christopher Columbus", "Im Westen ist jetzt Land begehbar.", BoardRandomEvent.ExtensionDirection.West, 2));
        list.Add(new Baumaßnahme(2, 25));

        list.Add(new KleineMauer(10, 20, KleineMauer.Orientation.Horizontal));
        list.Add(new KleineMauer(3, 20, KleineMauer.Orientation.Horizontal));
        list.Add(new KleineMauer(10, 20, KleineMauer.Orientation.Vertical));
        list.Add(new KleineMauer(2, 20, KleineMauer.Orientation.Vertical));

        list.Add(new Aufforstung(6, 20));
        list.Add(new SpawnChessPieceAtRandomPositionEvent(10, ChessPiece.Type.Bishop));
        list.Add(new SpawnChessPieceAtRandomPositionEvent(10, ChessPiece.Type.Rook));

        return list;
	}
}
