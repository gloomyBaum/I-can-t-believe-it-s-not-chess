using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugScenario : LakeScenario
{
	/*public DebugScenario()
	{
		XSize = 8;
		YSize = 8;
		ChessPieceFormation = MakeChessPieceFormation();
		PossibleEvents = MakePossibleEventList();
		DisabledTiles = MakeDisabledTileList();
		MaxEventInterval = 11;
	}*/
	protected override List<FormationUnit> MakeChessPieceFormation()
	{
		return base.MakeChessPieceFormation();
	}

	protected override List<Vector2Int> MakeDisabledTileList()
	{
		return base.MakeDisabledTileList();
	}
	protected override List<RandomEvent> MakePossibleEventList()
	{
		List<RandomEvent> list = new List<RandomEvent>();

        //hier event einfügen zum debuggen

        //Lake Game Events for testing
        list.Add(new ReplaceAllChessPiecesOfType(6, "Harley Davidson", "Alle Springer erhalten ein temporäres Upgrade! Ihre Beine werden für einen Zug durch Reifen ersetzt.", ChessPiece.Type.Knight, ChessPiece.Type.Harley, true));
        list.Add(new ReplaceAllChessPiecesOfType(2, "High Heels Fiasko", "Durch den unebenen Boden haben sich alle Damen den HighHeel abgebrochen. Sie sind Schlecht gelaunt und können sich 2 Züge nicht bewegen.", ChessPiece.Type.Queen, ChessPiece.Type.BrokenQueen, true));
        list.Add(new ReplaceAllChessPiecesOfType(8, "Oktoberfest", "Alle Bauern vergnügen sich auf dem Oktoberfest in der Nähe. Sie können nun einige Züge nicht mehr geradeaus laufen!", ChessPiece.Type.Pawn, ChessPiece.Type.DrunkPawn, true));
        list.Add(new ReplaceAllChessPiecesOfType(6, "Freibier", "Bauern vergnügten sich bei einem lokalen #RettetDieWale Event wo es Freibier gab. Ihre Bewegungen sind in nächster Zeit nicht ganz wie erwartet.", ChessPiece.Type.Pawn, ChessPiece.Type.ReallyDrunkPawn, true));

        //läuft nicht ab
        list.Add(new ReplaceAllChessPiecesOfType(0, "Für den König!", "Die Könige haben nun auch bemerkt das dies kein normales Schachfeld ist. Um mitzuhalten können sie nun doppelt so weit laufen.", ChessPiece.Type.King, ChessPiece.Type.KingUpgrade, false));

        list.Add(new Nothing());
        list.Add(new Meteor(5, 20));
        list.Add(new SimpleBoardSizeEvent(50, "Seidenstraße", "Der Weg nach China im Osten ist jetzt sicher.", BoardRandomEvent.ExtensionDirection.East, 3));
        list.Add(new SimpleBoardSizeEvent(50, "Christopher Columbus", "Im Westen ist jetzt Land begehbar.", BoardRandomEvent.ExtensionDirection.West, 3));
        list.Add(new Baumaßnahme(2, 50));

        list.Add(new KleineMauer(10, 20, KleineMauer.Orientation.Horizontal));
        list.Add(new KleineMauer(3, 20, KleineMauer.Orientation.Horizontal));
        list.Add(new KleineMauer(10, 20, KleineMauer.Orientation.Vertical));
        list.Add(new KleineMauer(2, 20, KleineMauer.Orientation.Vertical));

        list.Add(new Aufforstung(6, 20));

        list.Add(new SpawnChessPieceAtRandomPositionEvent(5, ChessPiece.Type.Pawn));
        list.Add(new SpawnChessPieceAtRandomPositionEvent(10, ChessPiece.Type.Knight));
        list.Add(new SpawnChessPieceAtRandomPositionEvent(10, ChessPiece.Type.Bishop));
        list.Add(new SpawnChessPieceAtRandomPositionEvent(10, ChessPiece.Type.Rook));
        list.Add(new SpawnChessPieceAtRandomPositionEvent(50, ChessPiece.Type.Queen));

        //Meteor test
        //list.Add(new Meteor(2));
        //list.Add(new Meteor(5));
        //list.Add(new Meteor(10));
        //list.Add(new Meteor(2, 10));

        //Aufforstung test
        //list.Add(new Aufforstung(5));

        /*
        //Tile Disabling test
        list.Add(new KleineMauer(10, KleineMauer.Orientation.Horizontal));
        list.Add(new KleineMauer(5, KleineMauer.Orientation.Horizontal));
        list.Add(new KleineMauer(10, KleineMauer.Orientation.Vertical));
        list.Add(new KleineMauer(5, KleineMauer.Orientation.Vertical));
        */

        //SpawnChessPiece Test
        //list.Add(new SpawnChessPieceAtRandomPositionEvent(3, "Bauer Aufgetaucht", "Ein neuer Bauer kämpft für deine Seite", ChessPiece.Type.Pawn));


        return list;
	}
}
