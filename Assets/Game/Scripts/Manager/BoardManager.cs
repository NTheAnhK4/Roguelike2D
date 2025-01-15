using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using Random = UnityEngine.Random;

public class BoardManager : MonoBehaviour
{
    [SerializeField] private Tilemap m_TileMap;
    [SerializeField] private Grid m_Grid;

    public int Width;
    public int Height;
    public Tile[] GroundTiles;
    public Tile[] WallTiles;
    public PlayerController Player;
    public class CellData
    {
        public bool Passable;
    }

    private CellData[,] m_BoardData;

    public void Init()
    {
        m_TileMap = transform.GetComponentInChildren<Tilemap>();
        m_Grid = transform.GetComponent<Grid>();
        m_BoardData = new CellData[Width, Height];

       
        for (int y = 0; y < Width; ++y)
        {
            
            for (int x = 0; x < Height; ++x)
            {
                
                Tile tile;
                m_BoardData[x, y] = new CellData();
                if (x == 0 || x == Height - 1 || y == 0 || y == Width - 1)
                {
                    tile = WallTiles[Random.Range(0, WallTiles.Length)];
                    m_BoardData[x, y].Passable = false;
                }
                else
                {
                    tile = GroundTiles[Random.Range(0, GroundTiles.Length)];
                    m_BoardData[x, y].Passable = true;
                }
                
                m_TileMap.SetTile(new Vector3Int(x, y,0),tile);
            }
        }
        Player.Spawn(this,new Vector2Int(1,1));
    }

   

    

    public Vector3 CellToWorld(Vector2Int cellIndex)
    {
        return m_Grid.GetCellCenterWorld((Vector3Int)cellIndex);
    }

    public CellData GetCellData(Vector2Int cellIndex)
    {
        if (cellIndex.x < 0 || cellIndex.x >= Width || cellIndex.y < 0 || cellIndex.y >= Height) return null;
        return m_BoardData[cellIndex.x, cellIndex.y];
    }
}
