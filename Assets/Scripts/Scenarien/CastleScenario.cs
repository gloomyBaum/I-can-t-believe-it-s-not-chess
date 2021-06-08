using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CastleScenario : Scenario
{
	public CastleScenario()
	{
		XSize = 8;
		YSize = 12;
		ChessPieceFormation = MakeChessPieceFormation();
		PossibleEvents = MakePossibleEventList();
		DisabledTiles = MakeDisabledTileList();
		MaxEventInterval = 11;
	}
	protected override List<FormationUnit> MakeChessPieceFormation()
	{
		List<FormationUnit> formation = new List<FormationUnit>();
		//white team
		formation.Add(new FormationUnit(ChessPiece.Type.Queen, ChessPiece.Color.White, 3, 0));
		formation.Add(new FormationUnit(ChessPiece.Type.King, ChessPiece.Color.White, 4, 0));
		formation.Add(new FormationUnit(ChessPiece.Type.Bishop, ChessPiece.Color.White, 2, 2));
		formation.Add(new FormationUnit(ChessPiece.Type.Bishop, ChessPiece.Color.White, 5, 2));
		formation.Add(new FormationUnit(ChessPiece.Type.Knight, ChessPiece.Color.White, 3, 3));
		formation.Add(new FormationUnit(ChessPiece.Type.Knight, ChessPiece.Color.White, 4, 3));
		formation.Add(new FormationUnit(ChessPiece.Type.Rook, ChessPiece.Color.White, 0, 3));
		formation.Add(new FormationUnit(ChessPiece.Type.Rook, ChessPiece.Color.White, 7, 3));
		for (int i = 0; i < XSize; i++)
		{
			formation.Add(new FormationUnit(ChessPiece.Type.Pawn, ChessPiece.Color.White, i, 4));
		}

		//black team
		formation.Add(new FormationUnit(ChessPiece.Type.Queen, ChessPiece.Color.Black, 3, 11));
		formation.Add(new FormationUnit(ChessPiece.Type.King, ChessPiece.Color.Black, 4, 11));
		formation.Add(new FormationUnit(ChessPiece.Type.Bishop, ChessPiece.Color.Black, 2, 9));
		formation.Add(new FormationUnit(ChessPiece.Type.Bishop, ChessPiece.Color.Black, 5, 9));
		formation.Add(new FormationUnit(ChessPiece.Type.Knight, ChessPiece.Color.Black, 3, 8));
		formation.Add(new FormationUnit(ChessPiece.Type.Knight, ChessPiece.Color.Black, 4, 8));
		formation.Add(new FormationUnit(ChessPiece.Type.Rook, ChessPiece.Color.Black, 0, 8));
		formation.Add(new FormationUnit(ChessPiece.Type.Rook, ChessPiece.Color.Black, 7, 8));
		for (int i = 0; i < XSize; i++)
		{
			formation.Add(new FormationUnit(ChessPiece.Type.Pawn, ChessPiece.Color.Black, i, 7));
		}

		return formation;
	}
	protected override List<Vector2Int> MakeDisabledTileList()
	{
		List<Vector2Int> list = new List<Vector2Int>();

		list.Add(new Vector2Int(0, 0));
		list.Add(new Vector2Int(1, 0));
		list.Add(new Vector2Int(2, 0));
		list.Add(new Vector2Int(5, 0));
		list.Add(new Vector2Int(6, 0));
		list.Add(new Vector2Int(7, 0));
		list.Add(new Vector2Int(0, 1));
		list.Add(new Vector2Int(7, 1));
		list.Add(new Vector2Int(0, 2));
		list.Add(new Vector2Int(7, 2));
		list.Add(new Vector2Int(1, 3));
		list.Add(new Vector2Int(2, 3));
		list.Add(new Vector2Int(5, 3));
		list.Add(new Vector2Int(6, 3));

		list.Add(new Vector2Int(0, 11));
		list.Add(new Vector2Int(1, 11));
		list.Add(new Vector2Int(2, 11));
		list.Add(new Vector2Int(5, 11));
		list.Add(new Vector2Int(6, 11));
		list.Add(new Vector2Int(7, 11));
		list.Add(new Vector2Int(0, 10));
		list.Add(new Vector2Int(7, 10));
		list.Add(new Vector2Int(0, 9));
		list.Add(new Vector2Int(7, 9));
		list.Add(new Vector2Int(1, 8));
		list.Add(new Vector2Int(2, 8));
		list.Add(new Vector2Int(5, 8));
		list.Add(new Vector2Int(6, 8));


		return list;
	}
	protected override List<RandomEvent> MakePossibleEventList()
	{
        List<RandomEvent> list = new List<RandomEvent>();

        list.Add(new ReplaceAllChessPiecesOfType(6, "Harley Davidson", "Alle Springer erhalten ein temporäres Upgrade! Ihre Beine werden für einen Zug durch Reifen ersetzt.", ChessPiece.Type.Knight, ChessPiece.Type.Harley, true));
        list.Add(new ReplaceAllChessPiecesOfType(2, "High Heels Fiasko", "Durch den unebenen Boden haben sich alle Damen den HighHeel abgebrochen. Sie sind Schlecht gelaunt und können sich 2 Züge nicht bewegen.", ChessPiece.Type.Queen, ChessPiece.Type.BrokenQueen, true));
        list.Add(new ReplaceAllChessPiecesOfType(8, "Oktoberfest", "Alle Bauern vergnügen sich auf dem Oktoberfest in der Nähe. Sie können nun einige Züge nicht mehr geradeaus laufen!", ChessPiece.Type.Pawn, ChessPiece.Type.DrunkPawn, true));
        list.Add(new ReplaceAllChessPiecesOfType(6, "Freibier", "Bauern vergnügten sich bei einem lokalen #RettetDieWale Event wo es Freibier gab. Ihre Bewegungen sind in nächster Zeit nicht ganz wie erwartet.", ChessPiece.Type.Pawn, ChessPiece.Type.ReallyDrunkPawn, true));
        list.Add(new ReplaceAllChessPiecesOfType(0, "Für den König!", "Die Könige haben nun auch bemerkt das dies kein normales Schachfeld ist. Um mitzuhalten können sie nun doppelt so weit laufen.", ChessPiece.Type.King, ChessPiece.Type.KingUpgrade, false));

        list.Add(new Nothing());

        list.Add(new SimpleBoardSizeEvent(50, "Seidenstraße", "Der Weg nach China im Osten ist jetzt sicher.", BoardRandomEvent.ExtensionDirection.East, 3));
        list.Add(new SimpleBoardSizeEvent(50, "Christopher Columbus", "Im Westen ist jetzt Land begehbar.", BoardRandomEvent.ExtensionDirection.West, 3));
        list.Add(new Baumaßnahme(2, 50));

        list.Add(new KleineMauer(3, 20, KleineMauer.Orientation.Horizontal));

        return list;
    }
}
