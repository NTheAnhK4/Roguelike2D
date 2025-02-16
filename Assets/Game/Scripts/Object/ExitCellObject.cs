using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TerrainUtils;
using UnityEngine.Tilemaps;

public class ExitCellObject : CellObject
{
    public Tile EndTile;
    public override void Init(Vector2Int cell)
    {
        base.Init(cell);
        GameManager.Instance.BoardManager.SetCellTile(cell,EndTile);
    }

    public override void PlayerEntered()
    {
        GameManager.Instance.NewLevel();
    }
}
