using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using Random = UnityEngine.Random;

public class BoardManager : MonoBehaviour
{
    private Tilemap m_TileMap;

    public int Width;
    public int Height;
    public Tile[] GroundTiles;
    public Tile[] WallTiles;
    public class CellData
    {
        public bool Passable;
    }

    private CellData[,] m_BoardData;
    private void Start()
    {
        m_TileMap = transform.GetComponentInChildren<Tilemap>();
        m_BoardData = new CellData[Width, Height];
        
        for (int y = 0; y < Width; ++y)
        {
            for (int x = 0; x < Height; ++x)
            {
                Tile tile;

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
    }
}
