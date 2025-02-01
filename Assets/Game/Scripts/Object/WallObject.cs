using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class WallObject : CellObject
{
    public List<Tile> ObstackleTileList;

    private Tile OriginalTile;
    [SerializeField] private int m_HealthPoint;
    
    public override void Init(Vector2Int cell)
    {
        base.Init(cell);
        m_HealthPoint = ObstackleTileList.Count;
        OriginalTile = GameManager.Instance.BoardManager.GetCellTile(cell);
        
        SetWallSprite();

    }

    public override bool PlayerWantsToEnter()
    {
        m_HealthPoint = Math.Max(m_HealthPoint - 1, 0);
       
        if (m_HealthPoint > 0)
        {
            SetWallSprite();
            return false;
        }
        GameManager.Instance.BoardManager.SetCellTile(m_Cell,OriginalTile);
        PoolingManager.Despawn(gameObject);
        
        return true;
    }

    void SetWallSprite()
    {
        //Update late
        int spriteIndex = ObstackleTileList.Count - m_HealthPoint;
        Tile obstackleTile = ObstackleTileList[spriteIndex];
        GameManager.Instance.BoardManager.SetCellTile(m_Cell,obstackleTile);
    }
}
