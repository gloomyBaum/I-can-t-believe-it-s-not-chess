using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandardGame : Scenario
{
	public StandardGame()
	{
		XSize = 8;
		YSize = 8;
		ChessPieceFormation = MakeChessPieceFormation();
		PossibleEvents = MakePossibleEventList();
		DisabledTiles = MakeDisabledTileList();
		MaxEventInterval = 11;
	}
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
		return base.MakePossibleEventList();
	}
}
