using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlainBoringNormalChess : Scenario
{
	public PlainBoringNormalChess()
	{
		XSize = 8;
		YSize = 8;
		ChessPieceFormation = MakeChessPieceFormation();
		PossibleEvents = MakePossibleEventList();
		DisabledTiles = MakeDisabledTileList();
		MaxEventInterval = 100;
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
		List<RandomEvent> list = new List<RandomEvent>();
		list.Add(new Nothing());
		return list;
	}
}